using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Team
    public List<GameObject> Teams;
    public List<GameObject> PlayerList;
    public List<GameObject> BallList;

    public List<GameObject> PlayerSkins;
    public List<GameObject> BallSkin;
    public List<GameObject> FieldBlock;
    public List<GameObject> World;

    //Settings
    public int fieldblocks;
    public string gamemode;
    public int player_count;
    public int team_count;
    public int win_score = 10;
    public int world_select;

    public GameObject UIManager;
    public delegate void GameManagerEvent(int id);
    public static event GameManagerEvent addscore;
    public delegate void PlayEvent(bool start);
    public static event PlayEvent Startgame;
    
    
	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnEnable()
    {
        GameField.Score +=Score_Ball;
        PlayerSelection.Ready += PlayerReady;
    }

    private void OnDisable()
    {
        GameField.Score -= Score_Ball;
        PlayerSelection.Ready -= PlayerReady;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GameStart()
    {
        //2 TEAMS
        //Create Blocks
        //Spawn Player
        //Timer
        //Spawn Ball
        InitializePlayer();
        Startgame(true);
        ResetRound();

        
    }

    void InitializeArea()
    {
        //TODO
        //Create Blocks by Angle
        //
        if (fieldblocks == 2)
        {
            Instantiate(FieldBlock[0],transform.position,transform.rotation);
        }
        else if (fieldblocks == 4)
        {
            Instantiate(FieldBlock[1], transform.position, transform.rotation);
        }
    }

    void InitializePlayer()
    {
        if (player_count == 2)
        {
            /*if (team_count > Teams.Count) { team_count = Teams.Count; }
            for (int i = 0; i < team_count; ++i)
            {
                float tmp_id = Teams[i].GetComponent<Team>().team_id;
                for (int a = 0; a < PlayerList.Count;++a)
                {
                    int tmp_pl_id = PlayerList[a].GetComponent<player>().team_id;
                    if (tmp_id == tmp_pl_id)
                    {
                        Teams[i].GetComponent<Team>().AddPlayer(PlayerList[a]);
                    }
                }
            }*/
            for (int i = 0; i < 2; ++i)
            {
                for (int a = 0; a < 2; ++a)
                {
                    string tmp1 = PlayerList[i].GetComponentInChildren<player>().Controller;
                    string tmp2 = UIManager.GetComponent<UIManager>().Playerselection[a].GetComponent<PlayerSelection>().Controller;
                    if ( tmp1 == tmp2)
                    {
                        Transform spawn = UIManager.GetComponent<UIManager>().Playerselection[a].GetComponent<PlayerSelection>().spawn;
                        Instantiate(PlayerList[i], spawn.position, spawn.rotation);
                    }
                }
            }
   
        }
        else if (player_count == 4)
        {

        }
    }

    void InitializeWorld()
    {

    }

    void InitializeAI()
    {

    }

    void Score_Ball(int lasthit, int team_area)
    {
        //Debug.Log("Game Field ID: " + id);
        //Debug.Log("Last Hit ID: " + lasthit);
        Debug.Log("Add Score to Team " + lasthit);
        if (lasthit == 99)
        {
            Debug.Log("All Players failed.");
        }
        else if (lasthit >= 0 && lasthit != team_area)
        {
            Teams[lasthit].GetComponent<Team>().score += 1;
        }
        CheckScore();
        ResetRound();

    }

    void AddPlayer(int skin, int teamid)
    {
        PlayerList.Add(PlayerSkins[skin]);
        PlayerList[PlayerList.Count-1].GetComponentInChildren<player>().team_id = teamid;
    }

    void CheckScore()
    {
        for (int i = 0; i < Teams.Count; ++i)
        {
            Team tmp_team = Teams[i].GetComponent<Team>();
            if (tmp_team.score >= win_score)
            {
                Debug.Log("Team " + tmp_team.teamname + " Wins");              
            }
        }
    }

    void PlayerReady(bool ready, int team, string controller, int skin)
    {
        Debug.Log("Player Ready");
        if (CheckPlayer(controller))
        {
            AddPlayer(skin, team);
            PlayerList[PlayerList.Count - 1].GetComponentInChildren<player>().Controller = controller;
        }        
    }

    bool CheckPlayer(string controller)
    {
        for (int i = 0; i < PlayerList.Count; ++i)
        {
            if (controller == PlayerList[i].GetComponentInChildren<player>().Controller)
            {
                Debug.Log("Check Player negative");
                return false;
            }
        }
        Debug.Log("Check Player positive");
        return true;
    }

    private void ResetRound()
    {
        Instantiate(BallSkin[0],transform.position,transform.rotation);
    }
}
