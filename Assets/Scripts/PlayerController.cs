using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public float upwardTrust;
    public float movementThrust;


    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start() {

        _rigidbody = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {

        HandleMovement();

    }

    /**
     * handle movement
     */
    private void HandleMovement() {

        if (Input.GetKey(KeyCode.Space)) {
            _rigidbody.AddForce(transform.up * upwardTrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W)) {
            _rigidbody.AddForce(new Vector3(0, 0, -1) * movementThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S)) {
            _rigidbody.AddForce(new Vector3(0, 0, 1) * movementThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A)) {
            _rigidbody.AddForce(new Vector3(1, 0, 0) * movementThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)) {
            _rigidbody.AddForce(new Vector3(-1, 0, 0) * movementThrust * Time.deltaTime);
        }

    }

}