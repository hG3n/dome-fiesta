using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public GameObject area;
    public GameObject left;
    public GameObject right;
    public string name;
    public float speed;
    public float jump;
    public Vector3 movement;
    public int direction;
    public Rigidbody rigid;
    public bool ground;
    public float maxspeed;
    public string Controller;
    public int team_id;
    public bool play;

    
    // Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}

    private void OnEnable()
    {
        ControlUnit.ButtonInput += GetInput;
    }
    private void OnDisable()
    {
        ControlUnit.ButtonInput -= GetInput;
    }

    public void GetInput(string source, string button)
    {
        if (Controller == source)
        {
            if (button == "jump")
            {
                Jump();
            }
            else if (button == "horizont")
            {
                SelectHorizontal(1);
            }
            else if (button == "horizontneg")
            {
                SelectHorizontal(-1);
            }
            else if (button == "horizontzero")
            {
                SelectHorizontal(0);
            }
        }
    }

    public void SelectHorizontal(int value)
    {
        if (value < 0)
        {
            direction = -1;
        }
        else if (value > 0)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
        {
            if (play)
            {
                Movement();
            }

            //movement = Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            //rigid.AddRelativeForce(movement);


           // Vector3 target = new Vector3(area.transform.position.x, transform.position.y, area.transform.position.z);
            transform.LookAt(area.transform.position);
            if (rigid.velocity.magnitude > maxspeed)
            {
                rigid.velocity =Vector3.ClampMagnitude(rigid.velocity,maxspeed);
            }
	    }

    void Jump()
    {
        if (ground)
        {
            rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    void Movement()
    {
        movement = right.transform.position * direction * speed * Time.deltaTime;
        rigid.AddForce(movement);
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
