using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundUIAC : MonoBehaviour
{

    [SerializeField] Sprite number1;
    [SerializeField] Sprite number2;
    [SerializeField] Sprite number3;
    [SerializeField] Sprite round;
    [SerializeField] SpriteRenderer numberRenderer;
    [SerializeField] SpriteRenderer start;
    [SerializeField]GameObject detector;
    
    int which;
    // Start is called before the first frame update
    void Start()
    {

        which = RoundCounter.instance.round;
        putImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void putImage()
    {
        detector.SetActive(false);

        if (which == 1)
        {
            numberRenderer.sprite = number1;
        }
        else if (which == 2)
        {
            numberRenderer.sprite = number2;
        }
        else if (which == 3)
        {
            numberRenderer.sprite = number3;
        }
        else
        {
            Debug.Log("ddddd");
        }

        
        this.gameObject.GetComponent<SpriteRenderer>().sprite = round;
        StartCoroutine(wait());
    }


    IEnumerator wait()
    {
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().RoundStart(which);

        yield return new WaitForSecondsRealtime(3.0f);
        
        if (which == 1)
        {
            numberRenderer.sprite = null;
        }
        else if (which == 2)
        {
            numberRenderer.sprite = null;
        }
        else
        {
            numberRenderer.sprite = null;
        }

        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;

        yield return new WaitForSecondsRealtime(1.0f);

        start.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1.0f);

        start.gameObject.SetActive(false);


        //Game start for current round
        detector.SetActive(true);
    }


    
}
