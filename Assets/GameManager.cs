using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Team
    public List<GameObject> teams;

    //Settings

    public string gamemode;
    public int player_count;
    public int win_score = 10;
    
    
	// Use this for initialization
	void Start () {
		
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

        }
        else if (player_count == 4)
        {

        }
    }

    void InitializeAI()
    {

    }
}
