using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles=new GameObject[3];
    [SerializeField] Transform[] shooterPositions=new Transform[3];
    [Tooltip("Height for detect hand height in descending order")]
    [SerializeField] float[] dividingHeight=new float[2];
    [SerializeField] int playerMaxHealth=10;
    [SerializeField] float cooldown = 1f;
    [SerializeField] int cooldownCount = 3;
    [SerializeField] GameObject gestureDetector;
    //[SerializeField] Animator anim;
    //[SerializeField] AudioClip onHitSFX;
    [SerializeField] TextMesh typeText;
    [SerializeField] GameObject changeImage;
    [SerializeField] AudioClip[] playerHitSFXs = new AudioClip[3];
    [SerializeField] AudioClip playerDeathSFX;
    //public bool isReadyToFire; // the bool value used by projectiles. Players will not be able to fire if it is false
    public bool isRightPlayer; // true if the player is on the right.
    
    private Vector3 handPosition=new Vector3(0,0,0);

    private int activatedShooterIndex = 0;
    private int type = 0;
    private int playerCurrentHealth;
    private int lastRandomHitIndex=0;

    private bool isDetectorActive = false;
    private bool isSwapping = false; //true if player is swapping weapon
    private bool handReadyToFireAgain = false;

    private float readyToFireTime;
    private int readyToFireIndex;
    private float[] fireTimeArray;
    private GameManager gameManager;
    private Animator anim;
    private void Awake()
    {
        
    }
    void Start()
    {
        
        SwapWeapon();
        //Setting all fire Time to
        fireTimeArray = new float[cooldownCount];
        for(int i=0;i<cooldownCount; i++)
        {
            fireTimeArray[i] = Time.time;
        }
        readyToFireIndex = 0;
        if(dividingHeight.Length!=shooterPositions.Length-1)
        {
            Debug.Log("DividingHeight does not match shooters' num");
        }
        try
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    private void OnEnable()
    {
        playerCurrentHealth = playerMaxHealth;
    }
    void Update()
    {
        
        if(IsHandActive())
        {
            if (!isDetectorActive)
            {
                isDetectorActive = true;
                gestureDetector.SetActive(true);
            }
        }
        else
        {
            if (isDetectorActive)
            {
                isDetectorActive = false;
                gestureDetector.SetActive(false);
            }
        }

    }
    public void Fire()//Fire Different type of projectiles
    {
        if (!IsHandActive())
        {
            Debug.Log("Fire jj with no Hand");//Probably won't happen.
            return;
        }
        if (handReadyToFireAgain&&Time.time>readyToFireTime)
        {
            HandPositionUpdate();
            //Project have different types.
            activatedShooterIndex = ActivateShooter();
            GameObject _projectile = Instantiate(projectiles[type], shooterPositions[activatedShooterIndex].position, Quaternion.identity);
            handReadyToFireAgain = false;
            fireTimeArray[readyToFireIndex] = Time.time + cooldown;
            readyToFireIndex += 1;
            if(readyToFireIndex==cooldownCount)
            {
                readyToFireIndex = 0;
            }
            readyToFireTime = fireTimeArray[readyToFireIndex];
            //Debug.Log(readyToFireTime.ToString() + "&"+fireTimeQueue[0].ToString()+"&"+fireTimeQueue[1].ToString());
            anim = GetComponentInChildren<Animator>();
            anim.SetTrigger("IsAttacking");
            Debug.Log("Fire" + (isRightPlayer ? "Left":"Right"));
        }

    }
    //Swap weapon type
    public void SwapWeapon()
    {
        type++;
        if (type == projectiles.Length)
        {
            type = 0;
        }
        string typeName;
        switch(type)
        {
            case 0:
                typeName = "Wood";
                break;
            case 1:
                typeName = "Fire";
                break;
            case 2:
                typeName = "Water";
                break;
            default:
                typeName = "Unknown";
                break;
        }

        //Change the type spirite
        typeText.text = typeName;
        changeImage.GetComponent<ChangeTypeImage>().changeImageType(type);

        if (isRightPlayer)
        {
            Debug.Log("Right Player Swap Weapon");
        }
        else
        {
            Debug.Log("Left Player Swap Weapon");
        }
    }
    //Called when the player is ready to fire with his hand return to a neutral state.
    public void ReturnToNeutual()
    {
        handReadyToFireAgain = true;
    }
    public void DamagePlayer(int damage)
    {
        //@Can add detection if game is over then no more damage will be dealt 
        playerCurrentHealth -= damage;
        if (isRightPlayer)
        {
            //animation for receving damage for right player
            anim = GetComponentInChildren<Animator>();
            anim.SetTrigger("Damaged");
        }
        else
        {
            anim = GetComponentInChildren<Animator>();
            anim.SetTrigger("Damaged");
        }
        if(playerCurrentHealth<=0)
        {
            PlaySoundEffect(playerDeathSFX);
            if(isRightPlayer)
            {
                gameManager.PlayerLeftWin();
            }
            else
            {
                gameManager.PlayerRightWin();
            }
        }
        else
        {
            //play random onHit audio;
            int _randomIndex = UnityEngine.Random.Range(0, playerHitSFXs.Length);
            if(_randomIndex==lastRandomHitIndex)
            {
                _randomIndex -= 1;
                if(_randomIndex==-1)
                {
                    _randomIndex = playerHitSFXs.Length - 1;
                }
            }
            PlaySoundEffect(playerHitSFXs[_randomIndex]);
            lastRandomHitIndex = _randomIndex;
        }
    }
    public int PlayerHealth()
    {
        return playerCurrentHealth; 
    }

    public int getMaxHealth()
    {
        return playerMaxHealth;
    }

    public float CoolDownPercentage(int index)
    {
        float _readyTime;
        _readyTime = fireTimeArray[index];
        if (_readyTime< Time.time)
        {
            return 1f;
        }
        else
        {
            return 1f-(_readyTime - Time.time) / cooldown;
        }
    }
    private bool IsHandActive()
    {
        //if it is left hand
        if (!isRightPlayer)
        {
            return (GameObject.Find("/HandModels/RigidRoundHand_L") != null);

        }
        //if it is right hand
        else
        {
            return (GameObject.Find("/HandModels/RigidRoundHand_R") != null) ;
        }
    }
    //Update index for active shooter position
    private int ActivateShooter()
    {
        for(int i=0;i<dividingHeight.Length;i++)
        {
            if(handPosition.y>dividingHeight[i])
            {
                return i;
            }
        }
        return dividingHeight.Length;
    }
    private void HandPositionUpdate()
    {
        if (!isRightPlayer)
        {
            handPosition = GameObject.Find("/HandModels/RigidRoundHand_L/palm").transform.position;
        }
        else
        { 
            handPosition = GameObject.Find("/HandModels/RigidRoundHand_R/palm").transform.position;
            //Debug.Log(handPosition);
        }
    }


    private AudioSource PlaySoundEffect(AudioClip sfx)
    {
        AudioSource _as;
        _as = gameObject.AddComponent<AudioSource>();
        _as.clip = sfx;
        _as.Play();
        _as.loop = false;
        StartCoroutine(DestroyAfterPlay(_as));
        return _as;
    }
    IEnumerator DestroyAfterPlay(AudioSource _as)
    {
        yield return new WaitForSeconds(_as.clip.length);
        Destroy(_as);
    }


}
