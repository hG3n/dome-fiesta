using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Comparers;


public class PlayerController : MonoBehaviour {

    // publics
    public float upwardTrust;

    public float movementThrust;
    public float movementThreshold;
    public float maxVelocityMagnitude;

    // privates
    private Rigidbody _rigidbody;

    /// <summary>
    /// start lifecycle hook
    /// </summary>
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// update each frame
    /// </summary>
    private void Update() {
        HandleKeyboardInput();
        HandleControllerInput();
//        DebugButtons();
    }


    /// <summary>
    /// handle keyboard input
    /// </summary>
    private void HandleKeyboardInput() {

        // rise
        if (Input.GetKey(KeyCode.Space)) {
            _rigidbody.AddForce(transform.up * upwardTrust * Time.deltaTime);
        }
        
        // move
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


    /// <summary>
    /// handle controller input
    /// </summary>
    private void HandleControllerInput() {

        // thrust up
        float r_trigger = Input.GetAxis("RightTrigger");
        r_trigger = (1.0f + r_trigger) / 2;
        if (r_trigger > 0.05) {
            _rigidbody.AddForce(transform.up * upwardTrust * Time.deltaTime * (r_trigger * 2));
        }

        // translation
        float l_horizontal = Input.GetAxis("LeftStickHorizontal");
        float l_vertical = Input.GetAxis("LeftStickVertical");
        var translation = new Vector3(l_horizontal, 0.0f, l_vertical);

        if (Mathf.Abs(l_horizontal) > movementThreshold && Mathf.Abs(l_vertical) > movementThreshold) {
            Debug.Log(translation);
            _rigidbody.AddForce(translation.normalized * movementThrust * Time.deltaTime);
        }

        float r_horizontal = Input.GetAxis("RightStickHorizontal");
        float r_vertical = Input.GetAxis("RightStickVertical");
        float l_trigger = Input.GetAxis("LeftTrigger");
    }


    /// <summary>
    /// what the function says
    /// </summary>
    void DebugButtons() {

        float l_horizontal = Input.GetAxis("LeftStickHorizontal");
        float l_vertical = Input.GetAxis("LeftStickVertical");
        float r_horizontal = Input.GetAxis("RightStickHorizontal");
        float r_vertical = Input.GetAxis("RightStickVertical");
        float l_trigger = Mathf.Abs(Input.GetAxis("LeftTrigger"));
        float r_trigger = Mathf.Abs(Input.GetAxis("RightTrigger"));

        if (l_horizontal < -0.0001 || l_horizontal > 0.0001)
            Debug.Log("left stick horizontal: " + l_horizontal.ToString());
        if (l_vertical < -0.0001 || l_vertical > 0.0001)
            Debug.Log("left stick vertical: " + l_vertical.ToString());
        if (r_horizontal < -0.0001 || r_horizontal > 0.0001)
            Debug.Log("right stick horizontal: " + l_horizontal.ToString());
        if (r_vertical < -0.0001 || r_vertical > 0.0001)
            Debug.Log("right stick vertical: " + l_vertical.ToString());

    }

}