using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private readonly string horizontalAxis = "Horizontal";
    private readonly string verticalAxis = "Vertical";
    private readonly string rightStickXAxis = "LookXAxis";
    private readonly string rightStickYAxis = "LookYAxis";

    private new Rigidbody rigidbody;

    private float speed;
    private float rotationSpeed;


    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        speed = 10;
        rotationSpeed = 2;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetAxis(verticalAxis) > .1f) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if(Input.GetAxis(verticalAxis) < -.1f) {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }

        if(Input.GetAxis(rightStickXAxis) > .1f) {
            transform.Rotate(new Vector3(0, transform.rotation.x * rotationSpeed * Time.deltaTime, 0));
        }
    }
}
