using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour {

    private Transform playerTransform;      // position of the player
    [SerializeField]
    private float minX, minY;               // minimum x and y position for the camera that it can move to
    [SerializeField]     
    private float maxX, MaxY;               // maximum x and y position for the camera that it can move to

    private void Awake() {
        // find player transform
        if(GameObject.FindWithTag("Player") != null) {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
	// Update is called once per frame
	void Update () {
        // follow the player within the certain range.
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, minX, maxX), Mathf.Clamp(playerTransform.position.y, minY, MaxY), 
                                        transform.position.z);
	}
}
