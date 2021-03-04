using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    [SerializeField] float stayingTime=0.15f;
    private Animator anim;
    private float clipTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        AnimatorClipInfo[] explosionClip = anim.GetCurrentAnimatorClipInfo(0);
        clipTime= explosionClip[0].clip.length;
        //Debug.Log(clipTime + "s for Explosion");
        StartCoroutine("DestroyAfterAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyAfterAnim()
    {
        yield return new WaitForSeconds(clipTime + stayingTime);
        Destroy(gameObject);
    }
}
