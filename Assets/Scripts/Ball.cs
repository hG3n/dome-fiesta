using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public delegate void BallEvent(GameObject ball);
    public static event BallEvent deathball;

    public delegate void Sound_Event(AudioSource source, string value);
    public static event Sound_Event Score_Sound;

    public Rigidbody rigid;
    public GameObject death;
    public float speed;
    public Vector3 velocity;
    public float force;
    public float bounce_force;





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
        }
        if (other.tag == "Death")
        {
            //Death();
        }

    }

    public void Death()
    {
        Instantiate(death,transform.position,transform.rotation);
        Score_Sound(GetComponent<AudioSource>(), "score");
        deathball(this.gameObject.transform.parent.gameObject);
    }

}
