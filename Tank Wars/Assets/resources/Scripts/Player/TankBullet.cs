using UnityEngine;
using System.Collections;

public class TankBullet : MonoBehaviour {

    public GameObject explosionPrefab;   // when the bullet hits a object, activate explosion particle.   
    public float bulletSpeed = 20;       // travel speed of the bullet
    public float upForce = 10;           // the force that the bullet will travel up in the angle of the gun
    private float _rotateBullet = 120;   // How fast the bullet has to rotate when it's dropping
    private TankScript _tankScript;      // Get public variables when needed.


    void Start() {
        if (GameObject.FindWithTag("Player") != null) {
            _tankScript = GameObject.FindWithTag("Player").GetComponent<TankScript>();
        }
        Destroy(gameObject, 2);
    }

    void FixedUpdate() {
        // if the bullet is falling rotate the bullet toward the ground
        if (GetComponent<Rigidbody2D>().velocity.y <= 0f) {
            if (_tankScript.ScaleX == 1)
                transform.Rotate(new Vector3(0, 0, transform.rotation.z - Time.deltaTime * _rotateBullet));
            else if (_tankScript.ScaleX == -1)
                transform.Rotate(new Vector3(0, 0, transform.rotation.z + Time.deltaTime * _rotateBullet));
        }
    }

    /// <summary>
    /// If the bullet hit a object let it explode and destory it.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col) {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject particle = gameObject.transform.GetChild(0).gameObject;
        particle.transform.SetParent(null);
        Destroy(particle, 1f);

        // TODO: when there are enemies, do damage on the enemies and/or destroy it.
    }
}