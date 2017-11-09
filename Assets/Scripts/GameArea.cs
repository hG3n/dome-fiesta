using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour {

    public float radius;
    public Vector3 position;
    private Renderer rend;


    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        //radius = rend.bounds.extents.magnitude;
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
