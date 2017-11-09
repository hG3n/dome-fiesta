using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {

    public GameObject area;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 target = new Vector3(area.transform.position.x,transform.position.y,area.transform.position.z);
        transform.LookAt(target);
	}
}
