using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] Sprite activatedButton;
    [SerializeField] Sprite leftButton;
    [SerializeField] Sprite rightButton;
    [SerializeField] AudioClip buttonHitSFX;
    private SpriteRenderer buttonImage;
    private bool isActivated = false;

    private bool isLeft = false;
    private bool isRight=false;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)

    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySoundEffect(buttonHitSFX);
        if(collision.gameObject.CompareTag("LeftSpell"))
        {
            isLeft = true;
            Destroy(collision.gameObject);
            buttonImage.sprite = leftButton;
        }
        if(collision.gameObject.CompareTag("RightSpell"))
        {
            isRight = true;
            Destroy(collision.gameObject);
            buttonImage.sprite = rightButton;
        }
        if(isLeft&&isRight)
        {
            buttonImage.sprite = activatedButton;
            if (!isActivated)
            {
                StartingSceneManager _s = GameObject.FindWithTag("GameManager").GetComponent<StartingSceneManager>();
                _s.StartButtonHit();
                isActivated = true;

            }
        }
    }
    private void PlaySoundEffect(AudioClip sfx)
    {
        AudioSource _as;
        _as = gameObject.AddComponent<AudioSource>();
        _as.clip = sfx;
        _as.Play();
        _as.loop = false;
        StartCoroutine(DestroyAfterPlay(_as));
    }
    IEnumerator DestroyAfterPlay(AudioSource _as)
    {
        yield return new WaitForSeconds(_as.clip.length);
        Destroy(_as);
    }
}
