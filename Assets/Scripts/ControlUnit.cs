using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUnit : MonoBehaviour {

    public delegate void ControlEvent(string source, string button);
    public static event ControlEvent ButtonInput;

    public string Source;
    public bool horizontal;
    public bool horizontal_zero;
    public bool vertical;
    public bool jump;
    public bool back;
    /*  keyboard1
     *  keyboard2
     *  con1
     *  con2
     *  con3
     *  con4
     */

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        VerticalInput();
        HorizontalInput();
        Jump();
        Back();
	}

    void VerticalInput()
    {
        //Joystick 1
        if (Source == "con1")
        {
            if (Input.GetAxis("Vertical1") != 0)
            {
                if (Input.GetAxis("Vertical1") > 0 && vertical)
                {

                    ButtonInput("con1","vert");
                    Debug.Log("pressed Vertical 1");
                }
                else if (Input.GetAxis("Vertical1") < 0 && vertical)
                {
                    ButtonInput("con1", "vertneg");
                    Debug.Log("pressed Vertical -1");
                }
                vertical = false;
            }
            else
            {
                vertical = true;
            }
        }
        //Joystick 2
        else if (Source=="con2")
        {
            if (Input.GetAxis("Vertical2") != 0)
            {
                if (Input.GetAxis("Vertical2") > 0 && vertical)
                {
                    ButtonInput("con2", "vert");
                    Debug.Log("pressed Vertical 2");
                }
                else if (Input.GetAxis("Vertical2") < 0 && vertical)
                {
                    ButtonInput("con2", "vertneg");
                    Debug.Log("pressed Vertical -2");
                }
                vertical = false;
            }
            else
            {
                vertical = true;
            }
        }
        //Joystick 3
        else if (Source == "con3")
        {
            if (Input.GetAxis("Vertical3") != 0)
            {
                if (Input.GetAxis("Vertical3") > 0 && vertical)
                {
                    ButtonInput("con3", "vert");
                    Debug.Log("pressed Vertical 3");
                }
                else if (Input.GetAxis("Vertical3") < 0 && vertical)
                {
                    ButtonInput("con3", "vertneg");
                    Debug.Log("pressed Vertical -3");
                }
                vertical = false;
            }
            else
            {
                vertical = true;
            }
        }
        //Joystick 4
        else if (Source == "con4")
        {
            if (Input.GetAxis("Vertical4") != 0)
            {
                if (Input.GetAxis("Vertical4") > 0 && vertical)
                {
                    ButtonInput("con4", "vert");
                    Debug.Log("pressed Vertical 4");
                }
                else if (Input.GetAxis("Vertical4") < 0 && vertical)
                {
                    ButtonInput("con4", "vertneg");
                    Debug.Log("pressed Vertical -4");
                }
                vertical = false;
            }
            else
            {
                vertical = true;
            }
        }
        
    }

    void HorizontalInput()
    {
        //Joystick 1
        if (Source == "con1")
        {
            if (Input.GetAxis("Horizontal1") != 0)
            {
                if (Input.GetAxis("Horizontal1") > 0 && horizontal)
                {
                    ButtonInput("con1", "horizont");
                    Debug.Log("pressed Horizontal 1");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal1") < 0 && horizontal)
                {
                    ButtonInput("con1", "horizontneg");
                    Debug.Log("pressed Horizontal -1");
                    horizontal_zero = false;
                }

                horizontal = false;
            }
            else if (Input.GetAxis("Horizontal1") == 0 && !horizontal_zero)
            {
                horizontal_zero = true;
                ButtonInput("con1", "horizontzero");
                Debug.Log("pressed Horizontal 0");
            }
            else
            {
                horizontal = true;
            }
        }
        //Joystick 2
        else if (Source == "con2")
        {
            if (Input.GetAxis("Horizontal2") != 0)
            {
                if (Input.GetAxis("Horizontal2") > 0 && horizontal)
                {
                    ButtonInput("con2", "horizont");
                    Debug.Log("pressed Horizontal 2");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal2") < 0 && horizontal)
                {
                    ButtonInput("con2", "horizontneg");
                    Debug.Log("pressed Horizontal -2");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal2") == 0 && !horizontal_zero)
                {
                    horizontal_zero = true;
                    ButtonInput("con2", "horizonalzero");
                    Debug.Log("pressed Horizontal 0");
                }
                horizontal = false;
            }
            else
            {
                horizontal = true;
            }
        }
        //Joystick 3
        else if (Source == "con3")
        {
            if (Input.GetAxis("Horizontal3") != 0)
            {
                if (Input.GetAxis("Horizontal3") > 0 && horizontal)
                {
                    ButtonInput("con3", "horizont");
                    Debug.Log("pressed Horizontal 3");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal3") < 0 && horizontal)
                {
                    ButtonInput("con3", "horizontneg");
                    Debug.Log("pressed Horizontal -3");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal3") == 0 && !horizontal_zero)
                {
                    horizontal_zero = true;
                    ButtonInput("con3", "horizonalzero");
                    Debug.Log("pressed Horizontal 0");
                }
                horizontal = false;
            }
            else
            {
                horizontal = true;
            }
        }
        //Joystick 4
        else if (Source == "con4")
        {
            if (Input.GetAxis("Horizontal4") != 0)
            {
                if (Input.GetAxis("Horizontal4") > 0 && horizontal)
                {
                    ButtonInput("con4", "horizont");
                    Debug.Log("pressed Horizontal 4");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal4") < 0 && horizontal)
                {
                    ButtonInput("con4", "horizontneg");
                    Debug.Log("pressed Horizontal -4");
                    horizontal_zero = false;
                }
                else if (Input.GetAxis("Horizontal4") == 0 && !horizontal_zero)
                {
                    horizontal_zero = true;
                    ButtonInput("con4", "horizonalzero");
                    Debug.Log("pressed Horizontal 0");
                }
                horizontal = false;
            }
            else
            {
                horizontal = true;
            }
        }

    }

    void Jump()
    {
        //Joystick 1 Button A
        if (Source == "con1")
        {
            if (Input.GetKey("joystick 1 button 0") && jump)
            {
                ButtonInput("con1", "jump");
                Debug.Log("pressed 1 Button A");
                jump = false;
            }
            else
            {
                jump = true;
            }
        }
        //Joystick 2 Button A
        else if (Source == "con2")
        {
            if (Input.GetKey("joystick 2 button 0") && jump)
            {
                ButtonInput("con2", "jump");
                Debug.Log("pressed 2 Button A");
                jump = false;
            }
            else
            {
                jump = true;
            }
        }
        //Joystick 3 Button A
        else if (Source == "con3")
        {
            if (Input.GetKey("joystick 3 button 0") && jump)
            {
                ButtonInput("con3", "jump");
                Debug.Log("pressed 3 Button A");
                jump = false;
            }
            else
            {
                jump = true;
            }
        }
        //Joystick 4 Button A
        else if (Source == "con4")
        {
            if (Input.GetKey("joystick 4 button 0") && jump)
            {
                ButtonInput("con4", "jump");
                Debug.Log("pressed 4 Button A");
                jump = false;
            }
            else
            {
                jump = true;
            }
        }



    }

    void Back()
    {
        //Joystick 1 Button B
        if (Source == "con1")
        {
            if (Input.GetKey("joystick 1 button 1") && back)
            {
                ButtonInput("con1", "back");
                Debug.Log("pressed 1 Button B");
                back = false;
            }
            else
            {
                back = true;
            }
        }
        //Joystick 2 Button B
        else if (Source == "con2")
        {
            if (Input.GetKey("joystick 2 button 1") && back)
            {
                ButtonInput("con2", "back");
                Debug.Log("pressed 2 Button B");
                back = false;
            }
            else
            {
                back = true;
            }
        }
        //Joystick 3 Button B
        else if (Source == "con3")
        {
            if (Input.GetKey("joystick 3 button 1") && back)
            {
                ButtonInput("con3", "back");
                Debug.Log("pressed 3 Button B");
                back = false;
            }
            else
            {
                back = true;
            }
        }
        //Joystick 4 Button B
        else if (Source == "con4")
        {
            if (Input.GetKey("joystick 4 button 1") && back)
            {
                ButtonInput("con4", "back");
                Debug.Log("pressed 4 Button B");
                back = false;
            }
            else
            {
                back = true;
            }
        }

    }
}
