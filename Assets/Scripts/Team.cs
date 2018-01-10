using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour {

    public string teamname;
    public int team_id;
    public int score;
    public int round_score;
    public Text team_text;
    public Text score_text;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        team_text.text = teamname;
        score_text.text = score.ToString();
	}

    void Clear()
    {
        score = 0;
        round_score = 0;
    }

}
