using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public int teamID;
    public delegate void FieldEvent(int team_area);
    public static event FieldEvent Score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.gameObject.GetComponent<Ball>().Death();
            Score(other.gameObject.GetComponent<Ball>().teamid);          
        }
    }
}
