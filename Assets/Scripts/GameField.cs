using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public float id;
    public delegate void FieldEvent(float id);
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
            Score(id);
        }
    }
}
