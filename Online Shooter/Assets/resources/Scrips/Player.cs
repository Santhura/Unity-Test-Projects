using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 10;
    public float jumpSpeed = 5;
    public float gravity = 20;

  
    CharacterController controller;
    private Vector3 _moveDirection = Vector3.zero;

    private Camera camera;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        transform.rotation = Quaternion.Euler(0, camera.currentRotation.y, 0);
        if (controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= speed;
            if (Input.GetButtonDown("Jump"))
                _moveDirection.y = jumpSpeed;
        }
        _moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(_moveDirection * Time.deltaTime);
       
    }
}