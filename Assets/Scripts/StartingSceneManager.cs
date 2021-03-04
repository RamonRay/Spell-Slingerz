using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Leap.Unity;

public class StartingSceneManager : MonoBehaviour
{
    [SerializeField] int readyTimesRequired = 3;
    //[SerializeField] GameObject[] playerReady=new GameObject[2];
    [SerializeField] float waitTimeBeforeStart = 2f;
    [SerializeField] GameObject playerLeft;
    [SerializeField] GameObject playerRight;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject detector;
    [SerializeField] ExtendedFingerDetector left;
    [SerializeField] ExtendedFingerDetector right;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioClip readySFX;
    //[SerializeField] GameObject startIndicator;
    //private int[] playerReadyTimes = new int[2] {0,0};
    //private bool[] playerIsReady = new bool[2] { false, false };
    //private bool[] isHandNeutral=new bool[2] { false, false };
    private int startButtonHit = 0;
    private bool isInTutorial = false;
    private bool isGameStart = false;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        foreach(var _readyObject in playerReady)
        {
            _readyObject.SetActive(false);
        }
        startIndicator.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void HandReturnNeutral(int playerId)
    {
        isHandNeutral[playerId] = true;
    }
    public void Ready(int playerId)
    {
        if(!isHandNeutral[playerId])
        {
            return;
        }
        isHandNeutral[playerId] = false;
        playerReadyTimes[playerId] += 1;
        if(playerReadyTimes[playerId]==readyTimesRequired)
        {
            Debug.Log("Player" + playerId + "isReady");
            //ShowReady(playerId);
            //PrintOutReady;
        }
    }
    private void ShowReady(int playerId)
    {
        playerIsReady[playerId] = true;
        playerReady[playerId].SetActive(true);
        if(playerIsReady[0]&&playerIsReady[1])
        {
            StartCoroutine("GameStart");
        }

    }
    */

    public void StartButtonHit()
    {
        startButtonHit += 1;
        if(startButtonHit==readyTimesRequired)
        {
            StartCoroutine("GameStart");
        }
    }
    IEnumerator GameStart()
    {
        RoundCounter roundCounter;
        roundCounter = RoundCounter.instance;
        roundCounter.ResetInt();
        bgmSource.Stop();
        bgmSource.loop = false;
        bgmSource.clip = readySFX;
        bgmSource.Play();
        isGameStart = true;
        //startIndicator.SetActive(true);
        tutorial.SetActive(false);
        yield return new WaitForSeconds(waitTimeBeforeStart);
        //SceneManager.LoadScene(1);
        SceneManager.LoadSceneAsync(1);
    }
    public void Tutorial()
    {
        if(isGameStart)
        {
            return;
        }
        if(!isInTutorial)
        {
            //playerLeft.SetActive(false);
            //playerRight.SetActive(false);
            left.enabled = false;
            right.enabled = false;
            //detector.SetActive(false);
            tutorial.SetActive(true);
            isInTutorial = true;
            GameObject _projectile;
            _projectile = GameObject.FindWithTag("LeftSpell");
            /*
            while (_projectile != null)
            {
                Destroy(_projectile);
                _projectile = GameObject.FindWithTag("LeftSpell");
            }
            _projectile = GameObject.FindWithTag("RightSpell");
            while (_projectile != null)
            {
                Destroy(_projectile);
                _projectile = GameObject.FindWithTag("RightSpell");
            }
            */

        }
        else
        {
            //playerLeft.SetActive(true);
            //playerRight.SetActive(true);
            left.enabled = true;
            right.enabled = true;
            //detector.SetActive(true);
            tutorial.SetActive(false);
            isInTutorial = false;
            
        }
    }
}
