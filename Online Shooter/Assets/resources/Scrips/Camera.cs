using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    private float _clampAngle = 80;
    public float lookSensitivity = 5;
    public float rotationSpeed = 100;
    public Vector3 rotation;
    public Vector3 currentRotation;
    public Vector3 rotationVelocity;
    public float lookSmoothDamp = .1f;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CameraMovement();
	}
    void CameraMovement()
    {
        rotation.x -= Input.GetAxis("Mouse Y") * lookSensitivity;
        rotation.y += Input.GetAxis("Mouse X") * lookSensitivity;

        rotation.x = Mathf.Clamp(rotation.x, -_clampAngle, _clampAngle);
        currentRotation.x = Mathf.SmoothDamp(currentRotation.x, rotation.x, ref rotationVelocity.x, lookSmoothDamp);
        currentRotation.y = Mathf.SmoothDamp(currentRotation.y, rotation.y, ref rotationVelocity.y, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
