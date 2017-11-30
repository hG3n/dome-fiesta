using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody rigid;
    public GameObject death;
    public float speed;
    public Vector3 velocity;
    public float force;
    public float bounce_force;
    public int last_hit;
    public int team_area_hit;




    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody>();

        float x= UnityEngine.Random.RandomRange(0,10);
        float z = UnityEngine.Random.RandomRange(0,10); ;
        Vector3 dir = new Vector3(x,0,z);
        rigid.AddForce(dir,ForceMode.Impulse);
	}

    void FixedUpdate()
    { 
        velocity = rigid.velocity;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "platform")
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up*force, ForceMode.Impulse);
        }
        if (other.tag == "Player")
        {
            Vector3 dir = transform.position - other.transform.position;
            rigid.velocity = Vector3.zero;
            rigid.AddForce(dir*bounce_force,ForceMode.Impulse);
            last_hit = other.gameObject.GetComponent<player>().team_id;
        }
        if (other.tag == "Death")
        {
            Death();
        }
        if (other.tag == "Team1")
        {
            team_area_hit = 1;
        }
        else if (other.tag=="Team2")
        {
            team_area_hit = 2;
        }
    }

    void Death()
    {
        Instantiate(death,transform.position,transform.rotation);
        Destroy(transform.parent.gameObject);
    }

}
