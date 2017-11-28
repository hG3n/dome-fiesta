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
    public Rigidbody rigid;
    public bool ground;
    public float maxspeed;
    public int playernumber = 1;
    public int team_id;
    public bool play;

    
    // Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}

    private void OnEnable()
    {
        UIManager.Click += GetInput;
    }
    private void OnDisable()
    {
        UIManager.Click -= GetInput;
    }

    void GetInput(string source)
    {
        if (source == "pause")
        {
            play = false;
        }
        else if (source == "unpause")
        {
            play = true;
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
            else
            {
                movement = right.transform.position * Input.GetAxis("Horizontal1") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.RightShift) && ground|| Input.GetKey("joystick 1 button 4") && ground)
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
            else
            {
                movement = right.transform.position * Input.GetAxis("Horizontal2") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.Space) && ground || Input.GetKey("joystick 2 button 4") && ground)
            {
                rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
        }
        if (playernumber == 3)
        {
            if (Input.GetKey(KeyCode.F))
            {
                movement = left.transform.position * -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else if (Input.GetKey(KeyCode.H))
            {
                movement = right.transform.position * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else
            {
                movement = right.transform.position * Input.GetAxis("Horizontal3") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.T) && ground || Input.GetKey("joystick 2 button 4") && ground)
            {
                rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
        }
        if (playernumber == 4)
        {
            if (Input.GetKey(KeyCode.J))
            {
                movement = left.transform.position * -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else if (Input.GetKey(KeyCode.L))
            {
                movement = right.transform.position * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else
            {
                movement = right.transform.position * Input.GetAxis("Horizontal4") * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            if (Input.GetKey(KeyCode.I) && ground || Input.GetKey("joystick 2 button 5") && ground)
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
