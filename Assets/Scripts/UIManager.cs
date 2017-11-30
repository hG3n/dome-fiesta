using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public delegate void UI_Event(string value);
    public static event UI_Event Click;

    public GameObject GameManager;
    public List<GameObject> Menu;
    public List<GameObject> Game_Menu;
    public List<GameObject> Playerselection;
    public List<GameObject> GameSetting;
    public List<GameObject> PlaySetting;
    public List<GameObject> Pause;

    public bool play;
    public bool pause;
    public int menu_select = 0;
    public int select = -1;

    public bool hori_con;
    public bool vert_con;

    //Settings
    public int win_count = 10;
    public int select_world = 0;



    // Use this for initialization
    void Start ()
    {
        MenuSelect();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!play || pause)
        {
            Input_Controller();
        }
	}



    void Input_Controller()
    {
        //Joystick1
        if (Input.GetAxis("Horizontal1") != 0)
        {
            if (Input.GetAxis("Horizontal1") > 0 && hori_con)
            {
                HorizontalInput(1);
                Debug.Log("pressed Horizontal 1");
            }
            else if (Input.GetAxis("Horizontal1") < 0 && hori_con)
            {
                HorizontalInput(-1);
                Debug.Log("pressed Horizontal -1");
            }
            hori_con = false;
        }
        else
        {
            hori_con = true;
        }
        if (Input.GetAxis("Vertical1")!=0)
        { 
            if (Input.GetAxis("Vertical1") > 0 && vert_con)
            {
                VerticalInput(1);
                Debug.Log("pressed Vertical 1");
            }
            else if (Input.GetAxis("Vertical1") < 0 && vert_con)
            {
                VerticalInput(-1);
                Debug.Log("pressed Vertical -1");
            }
            vert_con = false;
        }
        else
        {
            vert_con = true;
        }
        //Button A
        if (Input.GetKey("joystick button 0"))
        {
            ButtonSelect();
            Debug.Log("pressed Button A");
        }
        //Button Start
        if (Input.GetKey("joystick 1 button 7"))
        {
            Debug.Log("pressed Button Start");
            if (play)
            {
                PauseGame(true);
            }
            else
            {
                PauseGame(false);
            }
        }
    }



    public void Next()
    {
        select = -1;
        if (menu_select == 1 && CheckReady())
        {
            menu_select = 2;
            MenuSelect();
        }
    }

    void Back()
    {

    }

    public void ButtonSelect()
    {
        //Game Menu
        if (menu_select == 0)
        {
            //Select Game
            if (select == 0)
            {
                menu_select = 1;
                MenuSelect();
            }
            //Select Setting
            else if (select == 1)
            {
                menu_select = 4;
                MenuSelect();
            }
        }
        //Play Setting
        else if (menu_select==2)
        {
            //Start Game
            if (select==2)
            {
                Debug.Log("Start game UI MANAGER");
                StartGame();
                menu_select = 3;
                MenuSelect();
            }
        }
        //Game Setting
        else if (menu_select == 4)
        {
            //Music
            if (select == 0)
            {

            }
            //Sound
            else if (select == 1)
            {
                PauseGame(false);
            }
            //Back
            else if (select == GameSetting.Count-1)
            {
                //From Game to Pause Menu
                if (play && pause)
                {
                    menu_select = 5;
                    MenuSelect();
                }
                //To start Game Menu
                else if (!play)
                {
                    menu_select = 0;
                    MenuSelect();
                }
            }
        }
        //Pause Setting
        else if (menu_select == Menu.Count-1)
        {
            //Game Setting
            if (select == 0)
            {
                menu_select = 4;
                MenuSelect();
            }
            //Continue Game
            else if (select == 1)
            {
                PauseGame(false);
            }
        }
    }

    public void HorizontalInput(int value)
    {
        //Play Setting
        if (menu_select == 2)
        {
            if (select == 0)
            {
                if (value > 0)
                {
                    ++win_count;
                }
                else
                {
                    --win_count;
                }
                PlaySetting[0].GetComponent<Text>().text = win_count.ToString();
            }
            else if (select == 1)
            {
                if (value > 0)
                {
                    ++select_world;
                }
                else
                {
                    --select_world;
                }
                if (GameManager.GetComponent<GameManager>().World.Count <= select_world)
                {
                    select_world = 0;
                }
                else if (0 > select_world)
                {
                    select_world = GameManager.GetComponent<GameManager>().World.Count - 1;
                }
            }
        }
    }

    public void VerticalInput(int value)
    {
        //Up
        if (value == 1)
            {
                ++select;
                ColorSelect();
            }
        //Down
        else if (value == -1)
            {
                --select;
                ColorSelect();
            }     
    }
    public bool CheckReady()
    {
        //Check if ever player is ready
        int player_size = GameManager.GetComponent<GameManager>().player_count;
        for (int i = 0; i < player_size;++i)
        {
            if (!Playerselection[i].GetComponent<PlayerSelection>().ready)
            {
                Debug.Log("Ready negative");
                return false;
            }
        }
        Debug.Log("Ready positive");
        return true;
    }

    public void CheckActivate()
    {
        //Check if ever player is ready
        int player_count = 0;
        for (int i = 0; i < Playerselection.Count; ++i)
        {
            if (Playerselection[i].GetComponent<PlayerSelection>().activate)
            {
                ++player_count;
            }
        }
        GameManager.GetComponent<GameManager>().player_count = player_count;
    }

    public void StartGame()
    {
        GameManager.GetComponent<GameManager>().win_score = win_count;
        GameManager.GetComponent<GameManager>().world_select = select_world;
        GameManager.GetComponent<GameManager>().GameStart();
    }

    public void PauseGame(bool pause)
    {
        //Pause the Game
        if (pause)
        {
            Click("pause");
            for (int i = 0; i < Menu.Count; ++i)
            {
                if (Menu[i].gameObject.tag=="pause")
                {
                    Menu[i].gameObject.SetActive(true);
                }
                else
                {
                    Menu[i].gameObject.SetActive(false);
                }
            }

            //STOP FUNCTION
        }
        //Unpause the Game
        else
        {
            Click("unpause");
            for (int i = 0; i < Menu.Count; ++i)
            {
                if (Menu[i].gameObject.tag=="playscreen")
                {
                    Menu[i].gameObject.SetActive(true);
                }
                else
                {
                    Menu[i].gameObject.SetActive(false);
                }
            }

            //CONTINUE FUNCTION
        }
    }

    public void MenuSelect()
    {
        //Select UI Menu
        for (int i = 0; i < Menu.Count; ++i)
        {
            if (i == menu_select)
            {
                Menu[i].gameObject.SetActive(true);
            }
            else
            {
                Menu[i].gameObject.SetActive(false);
            }
        }
    }

    public void ColorSelect()
    {
        //Color the selected Textfield red
        //Game Menu
        if (menu_select==0)
        {
            if (select >= Game_Menu.Count)
            {
                select = 0;
            }
            else if (select < 0)
            {
                select = Game_Menu.Count - 1;
            }
            
            for (int i = 0; i < Game_Menu.Count; ++i)
            {
                if (i == select)
                {
                    Game_Menu[i].GetComponent<Text>().color = Color.red;
                }
                else
                {
                    Game_Menu[i].GetComponent<Text>().color = Color.black;
                }
            }
        }
        //Play Setting
        else if (menu_select == 2)
        {
            if (select >= PlaySetting.Count || select < 0)
            {
                select = 0;
            }

            for (int i = 0; i < PlaySetting.Count; ++i)
            {
                if (i == select)
                {
                    PlaySetting[i].GetComponent<Text>().color = Color.red;
                }
                else
                {
                    PlaySetting[i].GetComponent<Text>().color = Color.black;
                }
            }
        }
        //Game Setting
        else if (menu_select == 4)
        {
            if (select >= GameSetting.Count || select < 0)
            {
                select = 0;
            }

            for (int i = 0; i < GameSetting.Count; ++i)
            {
                if (i == select)
                {
                    GameSetting[i].GetComponent<Text>().color = Color.red;
                }
                else
                {
                    GameSetting[i].GetComponent<Text>().color = Color.black;
                }
            }
        }
        //Pause Menu
        else if (menu_select == Game_Menu.Count-1)
        {
            if (select >= Pause.Count || select < 0)
            {
                select = 0;
            }

            for (int i = 0; i < Pause.Count; ++i)
            {
                if (i == select)
                {
                    Pause[i].GetComponent<Text>().color = Color.red;
                }
                else
                {
                    Pause[i].GetComponent<Text>().color = Color.black;
                }
            }
        }
    }

    
}
