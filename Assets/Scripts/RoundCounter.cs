using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    public static RoundCounter instance;
    public int round = 0;
    public int leftWin = 0;
    public int rightWin = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Round" + round);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetInt()
    {
        round = 0;
        leftWin = 0;
        rightWin = 0;
    }


}
