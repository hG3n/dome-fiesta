using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public GameObject area;
    public GameObject left;
    public GameObject right;
    public float speed;
    public float jump;
    public Vector3 movement;
    public Rigidbody rigid;
    public bool ground;
    public float maxspeed;
    
    // Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            movement = left.transform.position * -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            rigid.AddForce(movement);
        }
        else if(Input.GetAxis("Horizontal")>0)
        {
            movement = right.transform.position * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            rigid.AddForce(movement);
        }

        //movement = Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //rigid.AddRelativeForce(movement);

        if (Input.GetKeyDown("space") && ground)
        {
            rigid.AddForce(Vector3.up*jump, ForceMode.Impulse);
        }
       // Vector3 target = new Vector3(area.transform.position.x, transform.position.y, area.transform.position.z);
        transform.LookAt(area.transform.position);
        if (rigid.velocity.magnitude > maxspeed)
        {
            rigid.velocity =Vector3.ClampMagnitude(rigid.velocity,maxspeed);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "platform")
        { ground = true; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "platform")
        { ground = false; }
    }
}
