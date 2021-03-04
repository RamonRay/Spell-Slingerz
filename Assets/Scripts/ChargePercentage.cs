using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePercentage : MonoBehaviour
{
    [SerializeField] int projectileIndex;
    private PlayerController playerController;
    private TextMesh textMesh;
    private int percentage;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        Debug.Log(playerController.gameObject.name);
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        percentage = (int)(playerController.CoolDownPercentage(projectileIndex)* 100f);
        //textMesh.text = playerController.CoolDownPercentage(projectileIndex).ToString();
        textMesh.text = percentage.ToString() + "%";
    }
}
