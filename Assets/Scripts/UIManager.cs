using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public delegate void UI_Event(string value);
    public static event UI_Event Click;


    public List<GameObject> Menu;

    public bool play;
    public int select = -1;
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!play)
        {
            Input_Controller();
        }
	}

    void Input_Controller()
    {
        //Joystick1
        if (Input.GetKeyDown("Horizontal1"))
        {
            if (Input.GetAxis("Horizontal1") > 0)
            {

            }
            else if (Input.GetAxis("Horizontal1") < 0)
            {

            }
        }
        if (Input.GetKeyDown("Vertical1"))
        {
            if (Input.GetAxis("Vertical1") > 0)
            {

            }
            else if (Input.GetAxis("Vertical1") < 0)
            {

            }
        }
        //Button A
        if (Input.GetButtonDown("0"))
        {

        }
        //Button Start
        if (Input.GetButtonDown("7"))
        {

        }
    }

    void Next()
    {
        select = -1;
    }

    void Back()
    {

    }

    /*
    void Select(int value)
    {
        select = value;

        if (select > Menu.Count)
        {
            select = 0;
        }
        if (select >= 0)
        {
            for (int i = 0; i < Menu.Count; ++i)
            {
                if (i == select)
                {
                    Menu[i].active = true;
                }
                else
                {
                    Menu[i].active = false;
                }
            }
        }
    }
    */
    
}
