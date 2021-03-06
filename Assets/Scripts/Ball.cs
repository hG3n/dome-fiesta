﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public delegate void BallEvent(GameObject ball);
    public static event BallEvent deathball;

    public delegate void FieldEvent(int team_area);
    public static event FieldEvent Score;

    public delegate void Sound_Event(AudioSource source, string value);
    public static event Sound_Event Score_Sound;

    public int teamid = -1;
    public int airID = -1;
    public Rigidbody rigid;
    public GameObject death;
    public float speed;
    public Vector3 velocity;
    public float force;
    public float bounce_force;
    public Vector3 origin_size;
    public float size = 1.0f;





    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody>();
        origin_size = transform.localScale;
        float x= UnityEngine.Random.RandomRange(0,10);
        float z = UnityEngine.Random.RandomRange(0,10); ;
        Vector3 dir = new Vector3(x,0,z);
        rigid.AddForce(dir,ForceMode.Impulse);
	}

    public void ChangeSize(float value)
    {
        size = value;
        transform.localScale = origin_size * size;
    }

    void FixedUpdate()
    { 
        velocity = rigid.velocity;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Field")
        {
            airID = other.GetComponent<GameField>().teamID;
        }
        if (other.tag == "bounce")
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up*force, ForceMode.Impulse);
        }
        if (other.tag == "Player")
        {
            if (airID == other.GetComponent<player>().teamID)
            {
                teamid = other.GetComponent<player>().teamID;
            }           
            Vector3 dir = transform.position - other.transform.position;
            rigid.velocity = Vector3.zero;
            rigid.AddForce(dir*bounce_force,ForceMode.Impulse);
        }
        if (other.tag == "Death")
        {
            Death();
            Score(teamid);
        }

    }

    public void Death()
    {
        Instantiate(death,transform.position,transform.rotation);
        Score_Sound(GetComponent<AudioSource>(), "score");
        deathball(this.gameObject.transform.parent.gameObject);
    }

}
