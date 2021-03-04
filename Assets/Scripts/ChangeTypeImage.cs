using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTypeImage : MonoBehaviour
{
    [SerializeField] Sprite wood;
    [SerializeField] Sprite fire;
    [SerializeField] Sprite water;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeImageType(int i)
    {
        if (i == 0)//wood
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = wood;
        }
        else if (i == 1)//fire
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = fire;
        }
        else if (i == 2)//water
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = water;
        }

        //Play music for changing the type here
    }
}
