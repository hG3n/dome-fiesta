using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour {

    public List<GameObject> player;
    public string teamname;
    public int team_id;
    public float score;
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

    public void AddPlayer(GameObject Player)
    {
        player.Add(Player);
    }
}
