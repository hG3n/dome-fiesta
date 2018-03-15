using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
public class GameManager : MonoBehaviour {

    //Team
    public List<GameObject> Player;
    public GameObject Field;
    public List<GameObject> BallList;

    public List<GameObject> PlayerSkins;
    public List<GameObject> BallSkin;
    public List<GameObject> World;
    public List<GameObject> GameFields;
    public List<string> GameMode;
    //  Normal
    //  Fury
    //  Insane
    public List<string> TeamSetting;

    public List<int> Teamscore;
    //Team 1 
    //Team 2 
    //Team 3
    //Team 4

    public List<Transform> Spawns;

    //Settings
    public int fieldblocks;
    public int playercount;

    public int gamemode = 0;
    public int ballmode = 0;
    public int ballskin = 0;
    // Team Composition
    public int teamcomp = 0;
    // 1v1
    // 2v2
    // 3ffa
    // 4 ffa
    public int win_score = 10;
    public int world_select;
    public int max_ball_count = 1;

    public OSCManager oscmanager;

    public GameObject UIManager;
    public delegate void GameManagerEvent(int id);
    public static event GameManagerEvent addscore;
    public delegate void PlayEvent(bool start);
    public static event PlayEvent Startgame;
    public static event PlayEvent FinishGame;
    public delegate void PlayerEvent();
    public static event PlayerEvent death;


    // Use this for initialization
    void Start ()
    {
		
	}

    private void OnEnable()
    {
        GameField.Score +=Score_Ball;
        Ball.deathball += DestroyBall;
    }

    private void OnDisable()
    {
        GameField.Score -= Score_Ball;
        Ball.deathball += DestroyBall;
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
        InitializeWorld();
        InitializeArea();
        InitializePlayer();
        oscmanager.GetComponent<OSCManager>().SendStartGame();
        Startgame(true);
        SpawnBall();

        
    }

    void InitializeArea()
    {
        Debug.Log("Initialze Area");
        if (teamcomp == 0)
        {
            Field = Instantiate(GameFields[0]) as GameObject;
        }
        else if (teamcomp == 1)
        {
            Field = Instantiate(GameFields[0]) as GameObject;
        }
        else if (teamcomp == 2)
        {
            Field = Instantiate(GameFields[1]) as GameObject;
        }
        else if (teamcomp == 3)
        {
            Field = Instantiate(GameFields[2] as GameObject);
        }
    }

    void InitializePlayer()
    {
        Debug.Log("Initialize Player");
        if (teamcomp == 0)
        {
            // Team Composition 1 v 1
            List<Transform> spawn = GameFields[0].GetComponent<Field>().PlayerSpawn;

            for (int i = 0; i<oscmanager.GetComponent<OSCManager>().ConnectionList.Count;++i)
            {
                int modelID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().modelID;
                int teamID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().teamID;
                string localip = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().localip;
                GameObject temp_player = Instantiate(PlayerSkins[modelID], spawn[teamID]) as GameObject;
                temp_player.GetComponent<player>().teamID = teamID;
                temp_player.GetComponent<player>().controller_ip = localip;
                Player.Add(temp_player);
            }
        }
        else if (teamcomp == 1)
        {
            // Team Composition 2 v 2
            bool firstspawn_team1 = false;
            bool firstspawn_team2 = false;
            List<Transform> spawn = GameFields[0].GetComponent<Field>().PlayerSpawn;

            for (int i = 0; i < oscmanager.GetComponent<OSCManager>().ConnectionList.Count; ++i)
            {
                int modelID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().modelID;
                int teamID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().teamID;
                string localip = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().localip;
                GameObject temp_player;
                if (teamID == 0)
                {
                    if (firstspawn_team1)
                    {
                        temp_player = Instantiate(PlayerSkins[modelID], spawn[1]) as GameObject;
                    }
                    else
                    {
                        temp_player = Instantiate(PlayerSkins[modelID], spawn[0]) as GameObject;
                        firstspawn_team1 = true;
                    }
                }
                else
                {
                    if (firstspawn_team2)
                    {
                        temp_player = Instantiate(PlayerSkins[modelID], spawn[3]) as GameObject;
                    }
                    else
                    {
                        temp_player = Instantiate(PlayerSkins[modelID], spawn[2]) as GameObject;
                        firstspawn_team1 = true;
                    }
                }

                temp_player.GetComponent<player>().teamID = teamID;
                temp_player.GetComponent<player>().controller_ip = localip;
                Player.Add(temp_player);
            }
        }
        else if (teamcomp == 2)
        {
            // Team Composition 3 free for all
            List<Transform> spawn = GameFields[1].GetComponent<Field>().PlayerSpawn;

            for (int i = 0; i < oscmanager.GetComponent<OSCManager>().ConnectionList.Count; ++i)
            {
                int modelID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().modelID;
                int teamID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().teamID;
                string localip = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().localip;
                GameObject temp_player = Instantiate(PlayerSkins[modelID], spawn[teamID]) as GameObject;
                temp_player.GetComponent<player>().teamID = teamID;
                temp_player.GetComponent<player>().controller_ip = localip;
                Player.Add(temp_player);
            }
        }
        else if (teamcomp == 3)
        {
            // Team Composition 4 free for all
            List<Transform> spawn = GameFields[2].GetComponent<Field>().PlayerSpawn;

            for (int i = 0; i < oscmanager.GetComponent<OSCManager>().ConnectionList.Count; ++i)
            {
                int modelID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().modelID;
                int teamID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().teamID;
                string localip = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().localip;
                GameObject temp_player = Instantiate(PlayerSkins[modelID], spawn[teamID]) as GameObject;
                temp_player.GetComponent<player>().teamID = teamID;
                temp_player.GetComponent<player>().controller_ip = localip;
                Player.Add(temp_player);
            }
        }

    }

    void InitializeWorld()
    {
        for (int i = 0; i<World.Count;++i)
        {
            if (i == world_select)
            {
                World[i].SetActive(true);
            }
            else
            {
                World[i].SetActive(false);
            }
        }
    }

    void Score_Ball(int team)
    {
        Debug.Log("Ball Scored");
        ++Teamscore[team];
        CheckScore();
        oscmanager.GetComponent<OSCManager>().SendUpdateScore();
        SpawnBall();
    }

    public void ClearSetting()
    {
        Debug.Log("Clear Settings");
        Player.Clear();
        BallList.Clear();
        death();
        for (int i =0; i<Teamscore.Count;++i)
        {
            Teamscore[i] = 0;
        }
        Destroy(Field);
        fieldblocks = 0;
        playercount = 0;
        gamemode = 0;
        teamcomp = 0;
        world_select = 0;
        max_ball_count = 1;
        win_score = 10;
    }

    public void Rematch()
    {
        for (int i = 0; i < Teamscore.Count; ++i)
        {
            Teamscore[i] = 0;
        }
        BallList.Clear();
        Startgame(true);
        oscmanager.GetComponent<OSCManager>().SendStartGame();
    }

    void CheckScore()
    {
        for (int i = 0; i < Teamscore.Count; ++i)
        {
            if (Teamscore[i] >= win_score)
            {
                Debug.Log("Team " + (i+1).ToString() + " Wins");
                WinRound(i);
            }
        }
    }

    void WinRound(int value)
    {
        Startgame(false);
        FinishGame(true);
        oscmanager.GetComponent<OSCManager>().SendEndGame(value);
    }

    private void NextRound()
    {
        Debug.Log("Spawn Ball");
        if (max_ball_count>BallList.Count)
        {
            GameObject ball = Instantiate(BallSkin[0], transform.position, transform.rotation) as GameObject;
            BallList.Add(ball);
        }
        
    }

    void SpawnBall()
    {
        Debug.Log("Spawn Ball");
        Instantiate(BallSkin[0], transform.position, transform.rotation);
    }

    public void DestroyBall(GameObject ball)
    {
        BallList.Remove(ball);
        Destroy(ball.gameObject);
    }
}
