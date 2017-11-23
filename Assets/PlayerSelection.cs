using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {

    public GameObject GameManager;
    public List<GameObject> Select;
    public GameObject Skin;
    public Transform spawn;
    public Text team_text;
    public Text player_text;
    public int team_number = 0;
    public int select_skin;
    public int select;
    public int player;
    public bool ready = false;

    // Use this for initialization
    void Start()
    {
        player_text = this.GetComponent<Text>();
        team_text.text = "Team " + team_number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {
            player_text.color = Color.green;
        }
        else
        {
            player_text.color = Color.black;
        }
    }

    public void SelectHorizontal(int value)
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
        }
        else
        {
            SelectHorizontal(1);
        }
    }


    public void SelectTeam(int value)
    {
        if (value > 0)
        {
            if (team_number >= 3)
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
                team_number = 4;
            }
            else
            {
                --team_number;
            }
        }
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
