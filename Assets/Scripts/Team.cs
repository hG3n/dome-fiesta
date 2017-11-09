using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {

    public List<GameObject> player;
    public string teamname;
    public float score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddPlayer(GameObject Player)
    {
        player.Add(Player);
    }
}
