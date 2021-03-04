using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerLeft;
    [SerializeField] PlayerController playerRight;
    [SerializeField] int maxTime;
    [SerializeField] GameObject gameDetector;
    [SerializeField] float timeBeforeReturn;
    [SerializeField] GameObject[] winTitle = new GameObject[2];
    [SerializeField] GameObject[] loseTitle = new GameObject[2];
    [SerializeField] Animator leftAnima;
    [SerializeField] Animator rightAnima;
    [SerializeField] Sprite win;
    [SerializeField] Sprite lose;
    [SerializeField] SpriteRenderer renderLeft;
    [SerializeField] SpriteRenderer renderRight;
    [SerializeField] GameObject endGameBarleft;
    [SerializeField] GameObject endGameBarright;
    [SerializeField] AudioSource bgm;
    private int currentTime;
    private bool isGameGoing;
    private AudioManager audioManager;
    private RoundCounter roundCounter;
    // Start is called before the first frame update
    void Awake()
    {
        roundCounter = RoundCounter.instance;
        roundCounter.round += 1;
        GameStart();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        //gameDetector.SetActive(true);
        currentTime = maxTime;
        isGameGoing = true;
        StartCoroutine("Timer");
    }
    private void GameTimeUp()
    {
        int _leftHealth = playerLeft.PlayerHealth();
        int _rightHealth = playerRight.PlayerHealth();
        if(_leftHealth>+_rightHealth)
        {
            PlayerLeftWin();
        }
        else if(_rightHealth<_leftHealth)
        {
            PlayerRightWin();
        }
        else
        {
            GameDraw();
        }
    }
    private void GameDraw()
    {
        if (isGameGoing)
        {
            gameDetector.SetActive(false);
            isGameGoing = false;
            Debug.Log("GameDraw");
            //Show corresponding Scene
            StartCoroutine("GameEnd");
        }
    }

    public void PlayerLeftWin()
    {
        if (isGameGoing)
        {
            roundCounter.leftWin += 1;
            gameDetector.SetActive(false);
            isGameGoing = false;
            Debug.Log("LeftPlayerWin");
            if (roundCounter.leftWin==2)
            {
                renderLeft.sprite = win;
                renderRight.sprite = lose;
                endGameBarleft.SetActive(true);
                endGameBarright.SetActive(true);
            }

            rightAnima.SetTrigger("Die");
            audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.PlayerWin(0);
            //Show corresponding scene
            StartCoroutine("GameEnd");
        }
    }
    public void PlayerRightWin()
    {
        if (isGameGoing)
        {
            roundCounter.rightWin += 1;
            gameDetector.SetActive(false);
            isGameGoing = false;
            Debug.Log("RightPlayerWin");
            if (roundCounter.rightWin == 2)
            {
                renderLeft.sprite = lose;
                renderRight.sprite = win;
                endGameBarleft.SetActive(true);
                endGameBarright.SetActive(true);
            }
            leftAnima.SetTrigger("Die");
            audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.PlayerWin(1);
            //Show corresponding scene
            StartCoroutine("GameEnd");
        }
        

    }
    IEnumerator Timer()
    {
        while(currentTime>0&&isGameGoing)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1;
            //@Update time display here
        }
        if(currentTime<=0)
        {
            GameTimeUp();
        }

    }

    IEnumerator GameEnd()
    {
        bgm.Stop();
        float _gameEndTime= Time.unscaledTime;
        while(Time.unscaledTime< _gameEndTime + timeBeforeReturn)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            
            Time.timeScale = Mathf.Clamp((Time.unscaledTime- _gameEndTime) /timeBeforeReturn*(-1.6f)+1f,0.2f,1f);
            
        }
        Time.timeScale = 1f;
        if (roundCounter.round == 3||roundCounter.leftWin==2||roundCounter.rightWin==2)
        {
            SceneManager.LoadSceneAsync(0);
        }
        else
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
