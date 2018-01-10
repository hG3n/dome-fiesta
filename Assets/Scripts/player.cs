using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public delegate void Sound_Event(AudioSource source,string value);
    public static event Sound_Event Jump_Sound;

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
    public bool play;
    
    // Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}

    private void OnEnable()
    {
        ControlUnit.ButtonInput += GetInput;
        GameManager.Startgame += Gamestart;
    }
    private void OnDisable()
    {
        ControlUnit.ButtonInput -= GetInput;
        GameManager.Startgame -= Gamestart;
    }

    public void Gamestart(bool start)
    {
        play = start;
    }
     
    public void GetInput(string source, string button)
    {
        //Receives Input Data from Controller
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
        //Decides to move sidewards(1,-1) or stand still (0)
        if (value < 0)
        {
            direction = -1;
        }
        else if (value > 0)
        {
            direction = 1;
        }
        else if (value == 0)
        {
            direction = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
        {
            //Move only if game is started
            if (play)
            {
                Movement();
            }
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
            Jump_Sound(GetComponent<AudioSource>(), "jump");
            rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    void Movement()
    {
        movement = right.transform.position * direction * speed * Time.deltaTime;
        rigid.AddForce(movement);
    }

    void Animation()
    {
        //Set Animation State
        //IDLE
        //MOVE (left and right)
        //JUMP
        //WIN
        //LOOSE



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
