using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
public class player : MonoBehaviour
{

    public delegate void Sound_Event(AudioSource source, string value);
    public static event Sound_Event Jump_Sound;

    public GameObject area;
    public GameObject left;
    public GameObject right;

    public string controller_ip;
    public int teamID;
    public OSCReceiver _receiver;

    private const string _osc_control = "/control/controller/";
    private const string _osc_jump = "/control/jump/";

    public string name;
    public float speed;
    public float jump;
    public Vector3 movement;
    public float direction;
    public Rigidbody rigid;
    public bool ground;
    public float maxspeed;
    public string Controller;
    public bool play;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _receiver = gameObject.AddComponent<OSCReceiver>();
        _receiver.LocalPort = 6968;
        _receiver.Bind(_osc_control, ReceiveMovement);
        _receiver.Bind(_osc_jump, ReceiveJump);
    }

    //Network Data
    void ReceiveMovement(OSCMessage message)
    {
        Debug.Log("Received Control Value from: " + message);
        if (controller_ip == message.Values[0].StringValue)
        {
            
            direction = message.Values[1].FloatValue;
        }
    }

    void ReceiveJump(OSCMessage message)
    {
        Debug.Log("Received Jump from: " + message);
        if (controller_ip == message.Values[0].StringValue)
        {
            
            Jump();
        }
    }



    void Update()
    {
        if (play)
        {
            HorizontalInput();
            ButtonInput();
        }
    }

    private void OnEnable()
    {
        GameManager.Startgame += Gamestart;
        GameManager.death += Death;
    }
    private void OnDisable()
    {
        GameManager.Startgame -= Gamestart;
        GameManager.death -= Death;
    }

    public void Gamestart(bool start)
    {
        play = start;
    }

    void HorizontalInput()
    {
        //Joystick 1
        if (Controller == "con1")
        {
            direction = Input.GetAxis("Horizontal1");
        }
        //Joystick 2
        else if (Controller == "con2")
        {
            direction = Input.GetAxis("Horizontal2");
        }
        //Joystick 3
        else if (Controller == "con3")
        {
            direction = Input.GetAxis("Horizontal3");
        }
        //Joystick 4
        else if (Controller == "con4")
        {
            direction = Input.GetAxis("Horizontal4");
        }
    }

    void ButtonInput()
    {
        //Jump Controller 1
        if (Controller == "con1")
        {
            if (Input.GetKeyDown("joystick 1 button 0"))
            {
                Jump();
            }
        }
        //Jump Controller 2
        else if (Controller == "con2")
        {
            if (Input.GetKeyDown("joystick 2 button 0"))
            {
                Jump();
            }
        }
        //Jump Controller 3
        else if (Controller == "con3")
        {
            if (Input.GetKeyDown("joystick 3 button 0"))
            {
                Jump();
            }
        }
        //Jump Controller 4
        else if (Controller == "con4")
        {
            if (Input.GetKeyDown("joystick 4 button 0"))
            {
                Jump();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move only if game is started
        if (play)
        {
            Movement();
        }
        transform.LookAt(area.transform.position);
        if (rigid.velocity.magnitude > maxspeed)
        {
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxspeed);
        }
    }

    void Jump()
    {
        //Jump only if grounded
        if (ground)
        {
            Jump_Sound(GetComponent<AudioSource>(), "jump");
            rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    void Movement()
    {
        // Add Movement Force to radiant Direction
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

    void PauseGame()
    {

    }

    void UnPauseGame()
    {

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

    public void Death()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}