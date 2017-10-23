using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    // publics
    public float upwardTrust;
    public float movementThrust;

    // privates
    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        HandleMovement();
    }

    /**
     * handle movement
     */
    private void HandleMovement() {


        // rise
        if (Input.GetKey(KeyCode.Space)) {
            _rigidbody.AddForce(transform.up * upwardTrust * Time.deltaTime);
        }


        //
        if (Input.GetKey(KeyCode.W)) {
            _rigidbody.AddForce(new Vector3(0, 0, -1) * movementThrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            _rigidbody.AddForce(new Vector3(0, 0, 1) * movementThrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            _rigidbody.AddForce(new Vector3(-1, 0, 0) * movementThrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            _rigidbody.AddForce(new Vector3(1, 0, 0) * movementThrust * Time.deltaTime);
        }

    }

}