using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public delegate void UI_Event(string value);
    public static event UI_Event Click;

    public delegate void Sound_Event(AudioSource source, string value);
    public static event Sound_Event UI_Sound;

    public delegate void Music_Event(string value);
    public static event Music_Event UI_Music;
    public static event Music_Event UI_Music_Setting;
    public static event Music_Event UI_Sound_Setting;

    public GameObject Gamemanager;
    public List<GameObject> Menu;
    public List<GameObject> Game_Menu;
    public List<GameObject> Mode_Setting;
    public List<GameObject> Playerselection;
    public List<GameObject> GameSetting;
    public List<GameObject> PlaySetting;
    public List<GameObject> Pause;
    public List<GameObject> TeamScore;
    public List<GameObject> Countdown;
    public List<GameObject> WinScreen;
    public List<GameObject> WinText;

    public bool play;
    public bool pause;
    public int menu_select = 0;
    public int select = -1;

    public bool hori_con;
    public bool vert_con;

    //Settings
    public int win_count = 10;
    public int select_world = 0;
    public int gamemode = 0;
    public int teamcomp = 0;



    // Use this for initialization
    void Start()
    {
        MenuSelect();
    }

    // Update is called once per frame
    void Update()
    {
        if (!play || pause)
        {
            Input_Controller();
        }
    }

    private void OnEnable()
    {
        GameManager.FinishGame += GameEnd;
    }

    private void OnDisable()
    {
        GameManager.FinishGame -= GameEnd;
    }



    void GameEnd(bool value)
    {
        if (value)
        {
            Debug.Log("Game Ends");
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
        if (Input.GetAxis("Vertical1") != 0)
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
        if (Input.GetKey("joystick 1 button 0"))
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
        if (menu_select == 1)
        {
            Gamemanager.GetComponent<GameManager>().SetTeamMode(teamcomp);
            Gamemanager.GetComponent<GameManager>().SetGameMode(gamemode);
            Debug.Log("Set Team Composition and Game Mode");
        }
        else if (menu_select == 2 && CheckReady())
        {
            Debug.Log("Next to Play Setting");
            menu_select = 3;
            MenuSelect();
        }
    }

    void Back()
    {
        --menu_select;
        MenuSelect();
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
        //Mode Setting
        else if (menu_select == 1)
        {
            //Next
            if (select == 2)
            {
                Debug.Log("Next to Player Selection");
                Gamemanager.GetComponent<GameManager>().PlayerList.Clear();
                Next();
                menu_select = 2;
                MenuSelect();
            }
        }
        //Play Setting
        else if (menu_select == 3)
        {
            //Start Game
            if (select == 2)
            {
                Debug.Log("Start game UI MANAGER");
                StartGame();
                menu_select = 4;
                MenuSelect();
                UI_Music(select_world.ToString());
            }
        }
        //Game Setting
        else if (menu_select == 5)
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
            else if (select == GameSetting.Count - 1)
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
        else if (menu_select == 6)
        {
            //Play Again
            if (select == 0)
            {
                Gamemanager.GetComponent<GameManager>().Rematch();
                for (int i = 0; i < TeamScore.Count; ++i)
                {
                    TeamScore[i].GetComponent<Text>().text = 0.ToString();
                }
                StartGame();
                menu_select = 4;
                MenuSelect();
            }
            //Main Menu
            else if (select == 1)
            {
                Gamemanager.GetComponent<GameManager>().ClearSetting();
                for (int i = 0; i < TeamScore.Count; ++i)
                {
                    TeamScore[i].GetComponent<Text>().text = 0.ToString();
                }
                menu_select = 0;
                MenuSelect();
            }
        }
        //Pause Setting
        else if (menu_select == Menu.Count - 1)
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
        UI_Sound(GetComponent<AudioSource>(),"select");
    }

    public void HorizontalInput(int value)
    {
        if (menu_select == 2)
        {
            if (select == 0)
            {
                if (value > 0)
                {
                    ++teamcomp;
                    if (Gamemanager.GetComponent<GameManager>().TeamSetting.Count<teamcomp)
                    {
                        teamcomp = 0;
                    }
                }
                else
                {
                    --teamcomp;
                    if (0>teamcomp)
                    {
                        teamcomp = 0;
                    }
                }
            }
            else if (menu_select == 1)
            {
                if (value > 0)
                {
                    ++gamemode;
                    if (Gamemanager.GetComponent<GameManager>().TeamSetting.Count < gamemode)
                    {
                        gamemode = 0;
                    }
                }
                else
                {
                    --gamemode;
                    if (0 > gamemode)
                    {
                        gamemode = 0;
                    }
                }
            }
        }
        //Play Setting
        else if (menu_select == 3)
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
                if (Gamemanager.GetComponent<GameManager>().World.Count <= select_world)
                {
                    select_world = 0;
                }
                else if (0 > select_world)
                {
                    select_world = Gamemanager.GetComponent<GameManager>().World.Count - 1;
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
        int player_size = Gamemanager.GetComponent<GameManager>().playercount;
        for (int i = 0; i < player_size; ++i)
        {
            if (!Playerselection[i].GetComponent<PlayerSelection>().ready)
            {
                Debug.Log("Ready negative" + i.ToString());
                return false;
            }
        }
        Debug.Log("Ready positive");
        return true;

    }

    public void Win(int teamnummber)
    {
        for (int i = 0; i < WinText.Count; ++i)
        {
            WinText[i].gameObject.SetActive(true);
            WinText[i].GetComponent<Text>().text = "TEAM " + teamnummber.ToString() + " WINS";
        }
        menu_select = 6;
        MenuSelect();
    }

    public void StartGame()
    {
        for (int i = 0; i < WinText.Count; ++i)
        {
            WinText[i].gameObject.SetActive(false);
        }
        Gamemanager.GetComponent<GameManager>().win_score = win_count;
        Gamemanager.GetComponent<GameManager>().world_select = select_world;
        Gamemanager.GetComponent<GameManager>().GameStart();
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
        //Mode Setting
        else if (menu_select == 1)
        {
            if (select >= Mode_Setting.Count || select < 0)
            {
                select = 0;
            }

            for (int i = 0; i < Mode_Setting.Count; ++i)
            {
                if (i == select)
                {
                    Mode_Setting[i].GetComponent<Text>().color = Color.red;
                }
                else
                {
                    Mode_Setting[i].GetComponent<Text>().color = Color.black;
                }
            }
        }
        else if (menu_select == 3)
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
        else if (menu_select == 5)
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
        //Rematch Menu
        else if (menu_select == 6)
        {
            if (select >= WinScreen.Count || select < 0)
            {
                select = 0;
            }

            for (int i = 0; i < WinScreen.Count; ++i)
            {
                if (i == select)
                {
                    WinScreen[i].GetComponent<Text>().color = Color.red;
                }
                else
                {
                    WinScreen[i].GetComponent<Text>().color = Color.black;
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
    public void SetCoundown(int playercount,int value)
    {
        if (value == -1)
        {
            for (int i = 0; i < playercount; ++i)
            {
                Countdown[i].SetActive(false);
                Countdown[i].GetComponent<Text>().text = value.ToString();
            }
        }
        else
        {
            for (int i = 0; i < playercount; ++i)
            {
                Countdown[i].SetActive(true);
                Countdown[i].GetComponent<Text>().text = value.ToString();
            }
        }

    }

    public void UpdateScore(List<int> scorelist)
    {
        Debug.Log("Update Score");
        for (int i = 0; i < TeamScore.Count;++i)
        {
            Debug.Log("Score: " + scorelist[i].ToString());
            TeamScore[i].GetComponent<Text>().text = scorelist[i].ToString();
        }
    }

}
