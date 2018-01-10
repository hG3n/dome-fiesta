using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public int team_area;
    public delegate void FieldEvent(int team_area);
    public static event FieldEvent Score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.gameObject.GetComponent<Ball>().Death();
            Score(team_area);          
        }
    }
}
