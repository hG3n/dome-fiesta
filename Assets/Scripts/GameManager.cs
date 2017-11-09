using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Team
    public List<GameObject> Teams;
    public List<GameObject> PlayerList;

    public List<GameObject> PlayerSkins;

    //Settings

    public string gamemode;
    public int player_count;
    public int team_count;
    public int win_score = 10;

    public delegate void GameManagerEvent(int id);
    public static event GameManagerEvent addscore;
    
    
	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        GameField.Score +=Score_Ball;
    }

    private void OnDisable()
    {
        GameField.Score -= Score_Ball;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void GameStart()
    {

        
    }

    void InitializeArea()
    {

    }

    void InitializePlayer()
    {
        if (player_count == 2)
        {
            if (team_count > Teams.Count) { team_count = Teams.Count; }
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
            }
        }
        else if (player_count == 4)
        {

        }
    }

    void InitializeAI()
    {

    }

    void Score_Ball(int lasthit)
    {
        //Debug.Log("Game Field ID: " + id);
        //Debug.Log("Last Hit ID: " + lasthit);
        Debug.Log("Add Score to Team " + lasthit);
        if (lasthit == 99)
        {
            Debug.Log("All Players failed.");
        }
        else if (lasthit >= 0)
        {
            Teams[lasthit].GetComponent<Team>().score += 1;
        }
        CheckScore();

    }

    void AddPlayer(int skin, int teamid)
    {
        PlayerList.Add(PlayerSkins[skin]);
        PlayerList[PlayerList.Count].GetComponent<player>().team_id = teamid;
    }

    void CheckScore()
    {
        for (int i = 0; i < Teams.Count; ++i)
        {
            Team tmp_team = Teams[i].GetComponent<Team>();
            if (tmp_team.score >= win_score)
            {
                Debug.Log("Team " + tmp_team.teamname + " Wins");
                Reset();
            }
        }
    }

    private void Reset()
    {
        
    }
}
