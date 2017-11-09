using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody rigid;
    public float speed;
    public Vector3 velocity;
    public float force;
    public float bounce_force;




    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody>();
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
    }

}
