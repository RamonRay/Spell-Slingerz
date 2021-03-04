using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDAC : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] PlayerController s;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float percentage = s.CoolDownPercentage(index);
        this.gameObject.GetComponent<Image>().fillAmount = percentage;

    }
}
