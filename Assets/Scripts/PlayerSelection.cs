using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {

    public delegate void PlayerSelect(bool ready, string controller, int skin);
    public static event PlayerSelect Ready;

    public GameObject GameManager;
    public GameObject ControlManager;
    public GameObject UIManager;
    public List<GameObject> Select;
    public GameObject ready_text;
    public GameObject Skin;
    public Transform spawn;
    public Text player_text;
    public int playerid;
    public int select_skin = 0;
    public int select;
    public string Controller;

    public bool activate = false;
    public bool ready = false;

    // Use this for initialization
    void Start()
    {

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
        CreateCharacter();
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
                SelectSkin(-1);
        }
        else if (value > 0)
        {
                SelectSkin(1);
        }
    }

    public void Next()
    {
        if (Skin != null)
        {
            ready = true;
            ready_text.SetActive(true);
            player_text.color = Color.green;
            Ready(ready, Controller, select_skin);
            UIManager.GetComponent<UIManager>().Next();
            Destroy(Skin);
            
        }
    }

    public void Back()
    {
        if (ready)
        {
            ready = false;
            ready_text.SetActive(false);
            player_text.color = Color.black;
            Ready(ready, Controller, select_skin);
        }
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
            Debug.Log("Delete Character");
            Destroy(Skin);
            CreateCharacter();
        }
    }

    public void CreateCharacter()
    {
        Debug.Log("Load new Character");
        Debug.Log("Select Number: " + select_skin.ToString());
        try {
            Skin = Instantiate(GameManager.GetComponent<GameManager>().PlayerSkins[select_skin], spawn.position, spawn.rotation);
        }
        catch (System.Exception n)
        {
            Debug.LogException(n,this);
        }
    }

}
