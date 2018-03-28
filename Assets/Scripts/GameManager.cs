using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
public class GameManager : MonoBehaviour {

    //Team
    public List<GameObject> PlayerList;
    public List<GameObject> BallList;
    public List<GameObject> GameFieldList;
    public List<GameObject> PlayerSkinList;
    public List<GameObject> BallSkinList;

    public List<GameObject> WorldList;

    public List<string> GameMode;
    //  Normal
    //  Fury
    //  Insane

    public List<int> Teamscore;
    //Team 1 
    //Team 2 
    //Team 3
    //Team 4

    public List<Transform> Spawns;

    //Settings
    public int fieldblockskin;
    public int playercount;

    public int gamemode = 0;
    public int ballmode = 0;
    // small
    // normal
    // large
    // random
    public int ballskin = 0;
    public int teamcomp = 0;
    // 1v1
    // 2v2
    // 3ffa
    // 4 ffa
    public int win_score = 10;
    public int world_select = 0;
    public int max_ball_count = 1;

    public bool play = false;

    public OSCManager oscmanager;
    public FieldManager fieldmanager;

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
        Ball.Score +=Score_Ball;
        Ball.deathball += DestroyBall;
    }

    private void OnDisable()
    {
        Ball.Score -= Score_Ball;
        Ball.deathball += DestroyBall;
    }

    // Update is called once per frame
    void Update () {
		
	}

    //Ready
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
        play = true;
        if (gamemode == 0)
        {
            max_ball_count = 1;
            SpawnBall();
        }
        else if (gamemode == 1)
        {
            max_ball_count = 3;
            SpawnBall();
            SpawnBall();
        }
        else if (gamemode == 2)
        {
            max_ball_count = 5;
            SpawnBall();
            SpawnBall();
            SpawnBall();
        }
              
    }

    //Ready
    void InitializeArea()
    {
        fieldmanager.CreateGameField();
    }

    //
    void InitializePlayer()
    {
        Debug.Log("Initialize Player");
        // Team Composition 1 v 1
        List<Transform> spawn = fieldmanager.GetComponent<FieldManager>().PlayerSpawn;

        for (int i = 0; i<oscmanager.GetComponent<OSCManager>().ConnectionList.Count;++i)
        {
            int modelID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().modelID;
            int teamID = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().teamID;
            string localip = oscmanager.GetComponent<OSCManager>().ConnectionList[i].GetComponent<Client>().localip;
            GameObject temp_player = Instantiate(PlayerSkinList[modelID], spawn[i]) as GameObject;
            temp_player.transform.GetChild(0).gameObject.GetComponent<player>().teamID = teamID;
            temp_player.transform.GetChild(0).gameObject.GetComponent<player>().controller_ip = localip;
            PlayerList.Add(temp_player);
        }
       

    }

    //Ready
    void InitializeWorld()
    {
        for (int i = 0; i<WorldList.Count;++i)
        {
            if (i == world_select)
            {
                WorldList[i].SetActive(true);
            }
            else
            {
                WorldList[i].SetActive(false);
            }
        }
    }

    //Ready
    void Score_Ball(int team)
    {
        Debug.Log("Ball Scored");
        ++Teamscore[team];
        CheckScore();
        oscmanager.GetComponent<OSCManager>().SendUpdateScore();
        SpawnBall();
    }

    //Ready
    public void ClearSetting()
    {
        Debug.Log("Clear Settings");
        PlayerList.Clear();
        BallList.Clear();
        fieldmanager.ClearGameField();
        death();
        for (int i =0; i<Teamscore.Count;++i)
        {
            Teamscore[i] = 0;
        }
    }

    public void Rematch()
    {
        ClearSetting();
        GameStart();
        oscmanager.GetComponent<OSCManager>().SendStartGame();
    }

    //Ready
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

    //Ready
    void WinRound(int value)
    {
        Startgame(false);
        play = false;
        FinishGame(true);
        oscmanager.GetComponent<OSCManager>().SendEndGame(value);
    }

    private void NextRound()
    {
        Debug.Log("Spawn Ball");
        if (max_ball_count>BallList.Count)
        {
            GameObject ball = Instantiate(BallSkinList[0], transform.position, transform.rotation) as GameObject;
            BallList.Add(ball);
        }
        
    }

    //Ready
    void SpawnBall()
    {        
        if (BallList.Count <= max_ball_count && play)
        {
            Debug.Log("Spawn Ball");
            GameObject temp_Ball = Instantiate(BallSkinList[ballskin], transform.position, transform.rotation) as GameObject;
            //Changing Ball Size
            if (ballmode == 0)
            {
                temp_Ball.transform.GetChild(0).GetComponent<Ball>().ChangeSize(0.75f);
            }
            else if (ballmode == 1)
            {
                temp_Ball.transform.GetChild(0).GetComponent<Ball>().ChangeSize(1.0f);
            }
            else if (ballmode == 2)
            {
                temp_Ball.transform.GetChild(0).GetComponent<Ball>().ChangeSize(1.25f);
            }
            if (ballmode == 4)
            {
                //Choose random Size
                int random = UnityEngine.Random.RandomRange(0,3);
                if (random==0)
                {
                    temp_Ball.transform.GetChild(0).GetComponent<Ball>().ChangeSize(0.75f);
                }
                else if (random == 1)
                {
                    temp_Ball.transform.GetChild(0).GetComponent<Ball>().ChangeSize(1.0f);
                }
                else if (random == 2)
                {
                    temp_Ball.transform.GetChild(0).GetComponent<Ball>().ChangeSize(1.25f);
                }
            }
            BallList.Add(temp_Ball);
        }
    }

    //Ready
    public void DestroyBall(GameObject ball)
    {
        BallList.Remove(ball);
        Destroy(ball.gameObject);
    }
}
