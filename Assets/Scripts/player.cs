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
    public int playernumber = 1;
    
    // Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        Movement();

        //movement = Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //rigid.AddRelativeForce(movement);


       // Vector3 target = new Vector3(area.transform.position.x, transform.position.y, area.transform.position.z);
        transform.LookAt(area.transform.position);
        if (rigid.velocity.magnitude > maxspeed)
        {
            rigid.velocity =Vector3.ClampMagnitude(rigid.velocity,maxspeed);
        }
	}

    void Movement()
    {
        if (playernumber == 1)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement = left.transform.position * -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                movement = right.transform.position * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.RightShift) && ground)
            {
                rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
            } 
        }
        if (playernumber == 2)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movement = left.transform.position * -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement = right.transform.position * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.Space) && ground)
            {
                rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
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
