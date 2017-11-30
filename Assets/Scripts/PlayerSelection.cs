using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {

    public delegate void PlayerSelect(bool ready, int team, string controller, int skin);
    public static event PlayerSelect Ready;

    public GameObject GameManager;
    public GameObject ControlManager;
    public GameObject UIManager;
    public List<GameObject> Select;
    public List<GameObject> Window;
    public GameObject ready_text;
    public GameObject Skin;
    public Transform spawn;
    public Text team_text;
    public Text player_text;
    public int team_number = 0;
    public int select_skin = 0;
    public int select;
    public string Controller;

    public bool activate = false;
    public bool ready = false;

    // Use this for initialization
    void Start()
    {
        //player_text = this.GetComponent<Text>();
        team_text.text = "Team " + team_number.ToString();
    }

    private void OnEnable()
    {
        ControlUnit.ButtonInput +=GetInput;
    }

    private void OnDisable()
    {
        ControlUnit.ButtonInput -= GetInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activate)
        {
            SetController();
        }
    }

    public void SetController()
    {
        if (Input.GetKey("joystick 1 button 0"))
        {
            if (CheckControl("con1"))
            {
                Controller = "con1";
                ControlManager.GetComponent<ControlManager>().AddControl(Controller);
                activate = true;
                Initialize();
            }
        }
        else if (Input.GetKey("joystick 2 button 0"))
        {
            if (CheckControl("con2"))
            {
                Controller = "con2";
                ControlManager.GetComponent<ControlManager>().AddControl(Controller);
                activate = true;
                Initialize();
            }
        }
        else if (Input.GetKey("joystick 3 button 0"))
        {
            if (CheckControl("con3"))
            {
                Controller = "con3";
                ControlManager.GetComponent<ControlManager>().AddControl(Controller);
                activate = true;
                Initialize();
            }
        }
        else if (Input.GetKey("joystick 4 button 0"))
        {
            if (CheckControl("con4"))
            {
                Controller = "con4";
                ControlManager.GetComponent<ControlManager>().AddControl(Controller);
                activate = true;
                Initialize();
            }
        }
    }

    public bool CheckControl(string source)
    {
        List<string> list = ControlManager.GetComponent<ControlManager>().Controls;
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i] == source)
            {
                return false;
            }
        }
        return true;
    }

    public void Initialize()
    {
        for (int i = 0;i<Window.Count;++i)
        {
            if (i==0)
            {
                Window[i].SetActive(false);
            }
            else
            {
                Window[i].SetActive(true);
            }
        }
        CreateCharacter();
        UIManager.GetComponent<UIManager>().CheckActivate();
    }

    public void GetInput(string source, string button)
    {
        if (Controller == source && !ready)
        {
            if (button == "jump")
            {
                Next();
            }
            else if (button == "back")
            {
                Back();
            }
            else if (button == "vert")
            {
                SelectVertical(1);
            }
            else if (button == "vertneg")
            {
                SelectVertical(-1);
            }
            else if (button == "horizont")
            {
                SelectHorizontal(1);
            }
            else if (button == "horizontneg")
            {
                SelectHorizontal(-1);
            }
        }
    }

    public void SelectHorizontal(int value)
    {
        if (value < 0)
        {
            if (select == 0)
            {
                SelectTeam(-1);
            }
            else if (select == 1)
            {
                SelectSkin(-1);
            }
        }
        else if (value > 0)
        {
            if (select == 0)
            {
                SelectTeam(1);
            }
            else if (select == 1)
            {
                SelectSkin(1);
            }
        }
    }

    public void SelectVertical(int value)
    {
        if (value > 0)
        {
            if (select >= Select.Count-1)
            {
                select = 0;
            }
            else
            {
                ++select;
            }
        }
        else if (value < 0)
        {
            if (select <= 0)
            {
                select = Select.Count-1;
            }
            else
            {
                --select;
            }
        }
        for (int i = 0; i <Select.Count;++i)
        {
            if (i == select)
            {
                Select[i].active = true;
            }
            else
            {
                Select[i].active = false;
            }
        }
    }

    public void Next()
    {
        if (select == Select.Count - 1)
        {
            ready = true;
            ready_text.SetActive(true);
            player_text.color = Color.green;
            Ready(ready, team_number, Controller, select_skin);
            UIManager.GetComponent<UIManager>().Next();
            Destroy(Skin);
            
        }
        else
        {
            SelectVertical(1);
        }
    }

    public void Back()
    {
        if (ready)
        {
            ready = false;
            ready_text.SetActive(false);
            player_text.color = Color.black;
            Ready(ready, team_number, Controller, select_skin);
        }
    }


    public void SelectTeam(int value)
    {
        /*if (value > 0)
        {
            if (team_number >= GameManager.GetComponent<GameManager>().team_count)
            {
                team_number = 0;               
            }
            else
            {
                ++team_number;
            }           
        }
        else if (value < 0)
        {
            if (team_number <= 0)
            {
                team_number = GameManager.GetComponent<GameManager>().team_count-1;
            }
            else
            {
                --team_number;
            }
        }
        */
        team_text.text = "Team " + team_number.ToString();
    }

    public void SelectSkin(int value)
    {
        if (value > 0)
        {
            if (select_skin >= GameManager.GetComponent<GameManager>().PlayerSkins.Count)
            {
                select_skin = 0;
            }
            else
            {
                ++select_skin;
            }
        }
        else if (value < 0)
        {
            if (select_skin <= 0)
            {
                select_skin = GameManager.GetComponent<GameManager>().PlayerSkins.Count-1;
            }
            else
            {
                --select_skin;
            }
        }
        if (Skin == null)
        {
            CreateCharacter();
        }
        else
        {
            Destroy(Skin);
            CreateCharacter();
        }
    }

    public void CreateCharacter()
    {
        Debug.Log("Load new Character");
        Skin = Instantiate(GameManager.GetComponent<GameManager>().PlayerSkins[select_skin], spawn.position, spawn.rotation);
    }

}
