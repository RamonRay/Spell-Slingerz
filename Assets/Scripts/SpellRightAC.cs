using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRightAC : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public int type;//0 is wood, 1 is fire, 2 is water
    GameObject[] whereToGo = new GameObject[3];
    int where = -1;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        whereToGo[0] = GameObject.Find("/PlayerLeft/PlayerSprite/Shooter1");
        whereToGo[1] = GameObject.Find("/PlayerLeft/PlayerSprite/Shooter2");
        whereToGo[2] = GameObject.Find("/PlayerLeft/PlayerSprite/Shooter3");

        
        if (this.gameObject.transform.position.y >= 0.1f)
        {
            where = 0;
        }
        else if (this.gameObject.transform.position.y >= 0.0f && this.gameObject.transform.position.y < 0.1f)
        {
            where = 1;
        }
        else if (this.gameObject.transform.position.y < 0.0f)
        {
            where = 2;
        }

        if (where == -1)
        {
            Debug.Log("Error in the right spell");
        }

        GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayInstantiateSFX(type);

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position,whereToGo[where].transform.position) <= 0.01f)//reaches the other player
        {
            GameObject.Find("/PlayerLeft").GetComponent<PlayerController>().DamagePlayer(1);
            //GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(type);
            GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayHitSFX(type);
            //trigger player's damage animation
            //Destroy(gameObject);
            Explode();
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, whereToGo[where].transform.position, Time.deltaTime*speed);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("LeftSpell"))//right vs. left
        {
            if (type  == collision.gameObject.GetComponent<SpellLeftAC>().type)//same type
            {
                //Both players suffer damage
                //GameObject.Find("/PlayerRight").GetComponent<PlayerController>().DamagePlayer(1);
                //GameObject.Find("/PlayerLeft").GetComponent<PlayerController>().DamagePlayer(1);
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlaySelfCancelSFX(type);
                //Destroy(collision.gameObject);
                //Destroy(gameObject);
                Explode();
            }
            else if (type == 0 && collision.gameObject.GetComponent<SpellLeftAC>().type == 1)//wood vs. fire, weak
            {
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(type);
                //acceralte the opposing spell
                collision.gameObject.GetComponent<SpellLeftAC>().speed = 0.5f;
                //Destroy(gameObject);
                Explode();
            }
            else if (type == 0 && collision.gameObject.GetComponent<SpellLeftAC>().type == 2)//wood vs. water, strong
            {
                //Destroy(collision.gameObject);
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(2);
                speed = 0.6f;
                //Acceralte self
            }
            else if (type == 1 && collision.gameObject.GetComponent<SpellLeftAC>().type == 0)//fire vs wood, strong
            {
                //Destroy(collision.gameObject);
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(0);
                speed = 0.6f;
                //accelerate
            }
            else if (type == 1 && collision.gameObject.GetComponent<SpellLeftAC>().type == 2)//fire vs.water, weak
            {
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(type);
                collision.gameObject.GetComponent<SpellLeftAC>().speed = 0.5f;
                //accelrate opposing spell
                //Destroy(gameObject);
                Explode();
            }
            else if (type == 2 && collision.gameObject.GetComponent<SpellLeftAC>().type == 1)//water vs. fire, strong
            {
                //Destroy(collision.gameObject);
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(1);
                speed = 0.6f;
                //accerlate
            }
            else if (type == 2 && collision.gameObject.GetComponent<SpellLeftAC>().type == 0)//water vs. wood, weak
            {
                GameObject.Find("/AudioManager").GetComponent<AudioManager>().PlayCancelSFX(type);
                collision.gameObject.GetComponent<SpellLeftAC>().speed = 0.5f;
                //Acceralte opposing spell
                //Destroy(gameObject);
                Explode();
            }
        }
    }
    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
