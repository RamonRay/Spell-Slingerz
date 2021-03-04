using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAC : MonoBehaviour
{
    [SerializeField] PlayerController c;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeValue(c.PlayerHealth(), c.getMaxHealth());
    }

    public void changeValue(float current, float maximum)
    {
        float ratio = current / maximum;
        //Debug.Log(current +" "+maximum);
        //Debug.Log(ratio);
        this.gameObject.GetComponent<Slider>().value = ratio;
        if (this.gameObject.GetComponent<Slider>().value < 0.1f)
        {
            this.gameObject.GetComponent<Slider>().value = 0.0f;
            Destroy(gameObject);
            //this.gameObject.GetComponent<Slider>().enabled = false;
            //One player dies
        }
    }
}
