using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCenter : MonoBehaviour {

    public float amount = 50f;
    public float force = 10f;
    public GameObject ball;
    public Rigidbody rigid;
    public Vector3 top;
    public Vector3 gravity;
    public float h;
    public float v;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        top = new Vector3(0, transform.localScale.y, 0);
    }


    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal") * amount * Time.deltaTime;
        v = Input.GetAxis("Vertical") * amount * Time.deltaTime;
        gravity = transform.TransformDirection(ball.transform.position - top);
        gravity.y = 0;
        float step = force * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards()
        //Vector3 dir = Quaternion.AngleAxis();
        
        //rigid.AddTorque(new Vector3(0,v,h),ForceMode.Impulse);
        //rigid.AddTorque(v,ForceMode.Impulse);
        //rigid.AddTorque(gravity,ForceMode.Acceleration);

    }
}
