using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Team

    public List<GameObject> PlayerList;
    public List<GameObject> ActivePlayer;
    public List<GameObject> BallList;
    public GameObject GameFieldBlock;

    public List<GameObject> PlayerSkins;
    public List<GameObject> BallSkin;
    public List<GameObject> FieldBlock;
    public List<GameObject> World;
    public List<string> GameMode;
    public List<string> TeamSetting;
    public List<int> Teamscore;
    public List<Transform> Spawns;
    public List<string> Controller;

    //Settings
    public int fieldblocks;
    public int playercount;
    public int gamemode = 0;
    public int teamcomp = 0;
    public int win_score = 10;
    public int world_select;
    public int max_ball_count = 1;

    public GameObject UIManager;
    public delegate void GameManagerEvent(int id);
    public static event GameManagerEvent addscore;
    public delegate void PlayEvent(bool start);
    public static event PlayEvent Startgame;
    public static event PlayEvent FinishGame;
    
    
	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnEnable()
    {
        GameField.Score +=Score_Ball;
        PlayerSelection.Ready += PlayerReady;
        Ball.deathball += DestroyBall;
    }

    private void OnDisable()
    {
        GameField.Score -= Score_Ball;
        PlayerSelection.Ready -= PlayerReady;
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
        Countdown();
        InitializePlayer();
        Startgame(true);
        NextRound();

        
    }

    void InitializeArea()
    {
        Debug.Log("Initialze Area");
        Instantiate(FieldBlock[fieldblocks],transform.position,transform.rotation);
    }

    void InitializePlayer()
    {
        Debug.Log("Initialize Player");
        for (int i = 0; i<PlayerList.Count;++i)
        {
            try
            {
                GameObject tmp = Instantiate(PlayerList[i], Spawns[i]);
                tmp.GetComponentInChildren<player>().Controller = Controller[i];
            }
            catch(System.Exception n)
            {
                Debug.Log(n);
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
        UIManager.GetComponent<UIManager>().UpdateScore(Teamscore);
        CheckScore();
        NextRound();
    }

    public void ClearSetting()
    {
        Debug.Log("Clear Settings");
        PlayerList.Clear();
        Controller.Clear();
        BallList.Clear();
        for (int i =0; i<Teamscore.Count;++i)
        {
            Teamscore[i] = 0;
        }
        Destroy(GameFieldBlock);
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
        PlayerList.Clear();
        Controller.Clear();
        Startgame(true);
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

    public void SetGameMode(int mode)
    {
        gamemode = mode;
    }

    public void SetTeamMode(int mode)
    {
        teamcomp = mode;
        //MODE 1V1
        if (mode == 0)
        {
            playercount = 2;
        }
        //MODE 2v2
        else if (mode==1)
        {
            playercount = 4;
        }
    }

    void WinRound(int value)
    {
        Startgame(false);
        UIManager.GetComponent<UIManager>().Win(value);
        FinishGame(true);
    }

    void ClearGame()
    {
        PlayerList.Clear();
        BallList.Clear();
        Destroy(GameFieldBlock);
    }

    void PlayerReady(bool ready, string controller, int skin)
    {
        Debug.Log("Player Ready");
        if (CheckPlayer(controller))
        {
            PlayerList.Add(PlayerSkins[skin]);
            Controller.Add(controller);
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

    IEnumerator Countdown()
    {
        for (int i = 3; i >= -1; --i)
        {
            UIManager.GetComponent<UIManager>().SetCoundown(playercount,i);
            yield return new WaitForSeconds(1.0f);
        }
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

    public void DestroyBall(GameObject ball)
    {
        BallList.Remove(ball);
        Destroy(ball.gameObject);
    }
}
