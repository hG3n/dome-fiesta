using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public int last_hit;
    public delegate void FieldEvent(int lasthit);
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
            Score(last_hit);
        }
    }
}
