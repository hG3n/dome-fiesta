using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public GameObject area;
    public GameObject left;
    public GameObject right;
    public Rigidbody rigid;
    public float speed;
    public Vector3 movement;
    public bool detect_ball;
    public float direction;
    public List<GameObject> BallList;
    public GameObject target;
    public Vector3 target_vec;
    public float radius;
    public float move_tolerance;
    public float distance;
    public float distance_ground;
    public float distance_target;
    public Matrix4x4 mat;

    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.LookAt(area.transform.position);
        if (detect_ball)
        {
            //EvaluatePriority();
            direction = transform.localPosition.x - target.transform.localPosition.x;
            distance_target = Vector3.Distance(target_vec,transform.position);
            if (direction > 0 && distance > move_tolerance)
            {
                movement = right.transform.position * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
            else if (direction < 0 && distance > move_tolerance)
            {
                movement = left.transform.position * speed * Time.deltaTime;
                rigid.AddForce(movement);
            }
        }
    }

    void EvaluatePriority()
    {
        if (BallList.Count == 1)
        {
            target = BallList[0];
            distance = Vector3.Distance(target.transform.position,transform.position);
            Vector3 tmp = new Vector3(target.transform.position.x, 0,target.transform.position.z);
            distance_ground = Vector3.Distance(tmp,target.transform.position);
        }
        else
        {
            for (int i = 0; i < BallList.Count; ++i)
            {
                float tmp_distance = Vector3.Distance(BallList[i].transform.position, transform.position);
                Vector3 tmp = new Vector3(BallList[i].transform.position.x, 0, BallList[i].transform.position.z);
                float tmp_distance_ground = Vector3.Distance(tmp, BallList[i].transform.position);
                if (tmp_distance < distance)
                {
                    target = BallList[i];
                    distance = tmp_distance;
                    float angle = Vector3.Angle(area.transform.position-target.transform.position,area.transform.position-transform.position);
                    target_vec = radius * new Vector3(Mathf.Sin(angle), 0,Mathf.Cos(angle));
                }
                else if (tmp_distance_ground < distance_ground)
                {
                    target = BallList[i];
                    distance_ground = tmp_distance;
                    float angle = Vector3.Angle(area.transform.position - target.transform.position, area.transform.position - transform.position);
                    target_vec = radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)).normalized;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            detect_ball = true;
            //BallList.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (detect_ball)
            {
                float tmp_distance = Vector3.Distance(other.transform.position, transform.position);
                Vector3 tmp = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                float tmp_distance_ground = Vector3.Distance(tmp, other.transform.position);
                if (tmp_distance < distance)
                {
                    target = other.gameObject;
                    distance = tmp_distance;
                    Vector3 tmp_v = new Vector3(radius,0,0);
                    float angle = Vector3.Angle(tmp_v - area.transform.position, target.transform.position- area.transform.position);
                    float angle1 = Vector3.Angle(tmp_v - area.transform.position, transform.position - area.transform.position);
                    angle -= angle1;
                    target_vec = radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)).normalized;
                }
                else if (tmp_distance_ground < distance_ground)
                {
                    target = other.gameObject;
                    distance_ground = tmp_distance;
                    float angle = Vector3.Angle(target.transform.position - area.transform.position, transform.position - area.transform.position);
                    target_vec = radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)).normalized;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            foreach (GameObject pos in BallList)
            {
                if (other.gameObject == pos)
                {
                    //BallList.Remove(pos);
                }
            }
            if (BallList.Count == 0)
            {
                detect_ball = false;
            }
            detect_ball = false;
            direction = 0;
        }
    }
}
