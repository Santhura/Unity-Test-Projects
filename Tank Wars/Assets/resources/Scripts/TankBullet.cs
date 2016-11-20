using UnityEngine;
using System.Collections;

public class TankBullet : MonoBehaviour {

    public GameObject explosionPrefab;
    public float bulletSpeed = 20;      // bullet speed 
    public float upForce = 10;          // the force that the bullet will travel up in the angle of the gun
    private float rotateBullet = 120;   // How fast the bullet has to rotate when it's dropping
    private TankScript tankScript;      // Get public variables when needed.


    void Start() {
        tankScript = GameObject.FindWithTag("Player").GetComponent<TankScript>();
    }

    void Update() {
        // if the bullet is falling rotate the bullet toward the ground
        if (GetComponent<Rigidbody2D>().velocity.y <= 0f) {
            if (tankScript.ScaleX == 1)
                transform.Rotate(new Vector3(0, 0, transform.rotation.z - Time.deltaTime * rotateBullet));
            else if (tankScript.ScaleX == -1)
                transform.Rotate(new Vector3(0, 0, transform.rotation.z + Time.deltaTime * rotateBullet));

        }
    }

    void OnCollisionEnter2D(Collision2D col) {


        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}