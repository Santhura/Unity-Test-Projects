using UnityEngine;
using System.Collections;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class TankScript : MonoBehaviour {

    //movement variables
    [Header("movement variables")]
    public string horizontalAxis;       //input for moving
    private float speed;                // tank speed for moving
    private float maxSpeed;             // The tanks max speed
    private float acceleration;         // acceleration of the tank 
    private float decelaration;         // slowing the tank down

    // gun variables
    [Header("Gun variables")]
    public GameObject gunHolder;        // holder were the gun is holding on to
    public string verticalAxis;         //input for rotating the gun
    private float angle;                // the angle of the gun 

    // bullet variables
    [Header("Bullet variables")]
    public GameObject bulletPrefab;     // the bullet that will be fired.
    public Transform gunPosition;       // the gun position of the tank
    public string jump;                 // axis to fire a bullet (space bar)
    private GameObject newBullet;       // new bullet that will be created when fired
    private const float backFire = 50;  // when a bullet is fired, the that has a little backfire

    private float scaleX;               // change the sprite
    private const float scaleYZ = 1;    // always set the y and z scale to value 1     

    /// <summary>
    /// property for the scale X
    /// get the X scale
    /// </summary>
    public float ScaleX {
        get { return scaleX; }
        // set { scaleX = value; }      // !!! maybe need this later on !!!
    }

    private new Rigidbody2D rigidbody;  // rigidbody of the player for physics
    private Animator anim;              // animator of the tank

    [Header("Particles")]
    public GameObject dustParticles;    // dust particles when driving fast

    // Use this for initialization
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 30;
        scaleX = 1;
        acceleration = 1.005f;
        decelaration = 1.5f;
        maxSpeed = 100;
        angle = 10;
    }

    // Update is called once per frame
    void Update() {
        transform.localScale = new Vector3(scaleX, scaleYZ, scaleYZ);
        Movement();
        GunControl();
    }

    void FixedUpdate() {
        Fire();
    }

    /// <summary>
    /// the tank movement on the X axis
    /// with acceleration
    /// </summary>
    private void Movement() {
        anim.SetFloat("Speed", Input.GetAxis(horizontalAxis));
        if (Input.GetAxis(horizontalAxis) > .1f) {
            dustParticles.SetActive(true);
            speed *= acceleration;
            rigidbody.velocity = new Vector2(speed * Time.deltaTime, 0);
            scaleX = 1;
        }
        else if (Input.GetAxis(horizontalAxis) < -.1f) {
            dustParticles.SetActive(true);
            speed *= acceleration;
            rigidbody.velocity = new Vector2(-speed * Time.deltaTime, 0);
            scaleX = -1;
        }
        else {
            speed -= decelaration;
            if (speed <= 30) {
                speed = 30;
                dustParticles.SetActive(false);
            }
        }

        if (speed >= maxSpeed)
            speed = maxSpeed;
    }

    /// <summary>
    /// Control the angle of the gun
    /// </summary>
    private void GunControl() {
        angle += Input.GetAxis(verticalAxis) * Time.deltaTime * 10;
        angle = Mathf.Clamp(angle, 0, 60);
        gunHolder.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// Fires a bullet to the right direction with the correct angle of the gun angle
    /// Also give the tank backfire
    /// </summary>
    private void Fire() {

        if (Input.GetButtonDown(jump)) {
            if (scaleX == 1) {
                newBullet = Instantiate(bulletPrefab, new Vector3(gunPosition.position.x, gunPosition.position.y, gunPosition.position.z + .1f), gunHolder.transform.localRotation) as GameObject;
                newBullet.transform.localScale = new Vector3(scaleX, scaleYZ, scaleYZ);
                newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * newBullet.GetComponent<TankBullet>().bulletSpeed, ForceMode2D.Force);
                rigidbody.AddForce(Vector2.left * backFire);
            }
            else if (scaleX == -1) {
                newBullet = Instantiate(bulletPrefab, new Vector3(gunPosition.position.x, gunPosition.position.y, gunPosition.position.z + .1f),
                                       new Quaternion(gunHolder.transform.localRotation.x, gunHolder.transform.localRotation.y, -gunHolder.transform.localRotation.z, gunHolder.transform.localRotation.w)) as GameObject;
                newBullet.transform.localScale = new Vector3(scaleX, scaleYZ, scaleYZ);
                newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * newBullet.GetComponent<TankBullet>().bulletSpeed, ForceMode2D.Force);
                rigidbody.AddForce(Vector2.right * backFire);
            }
            newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * newBullet.GetComponent<TankBullet>().upForce * angle, ForceMode2D.Force);
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>());
        }
    }
}