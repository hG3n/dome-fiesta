using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public int last_hit;
    public int team_area;
    public delegate void FieldEvent(int lasthit,int team_area);
    public static event FieldEvent Score;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            last_hit = other.gameObject.GetComponent<Ball>().last_hit;
            other.gameObject.GetComponent<Ball>().Death();
            Score(last_hit,team_area);
            
        }
    }
}
