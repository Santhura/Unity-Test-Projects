using UnityEngine;
using System.Collections;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class TankScript : MonoBehaviour {

    //movement variables
    [Header("movement variables")]
    public string horizontalAxis;       //input for moving
    private float _speed;                // tank speed for moving
    private float _minSpeed;             // tanks minimum speed
    private float _maxSpeed;             // The tanks max speed
    private float _acceleration;         // acceleration of the tank 
    private float _decelaration;         // slowing the tank down

    // gun variables
    [Header("Gun variables")]
    public GameObject gunHolder;        // holder were the gun is holding on to
    public string verticalAxis;         //input for rotating the gun
    private float _angle;                // the angle of the gun 

    // bullet variables
    [Header("Bullet variables")]
    public GameObject bulletPrefab;     // the bullet that will be fired.
    public Transform gunPosition;       // the gun position of the tank
    public string jump;                 // axis to fire a bullet (space bar)
    private GameObject _newBullet;       // new bullet that will be created when fired
    private const float _backFire = 50;  // when a bullet is fired, the that has a little backfire

    private float _scaleX;               // change the sprite
    private const float _scaleYZ = 1;    // always set the y and z scale to value 1     

    /// <summary>
    /// property for the scale X
    /// get the X scale
    /// </summary>
    public float ScaleX {
        get { return _scaleX; }
        // set { scaleX = value; }      // !!! maybe need this later on !!!
    }

    private new Rigidbody2D _rigidbody;  // rigidbody of the player for physics
    private Animator _anim;              // animator of the tank

    [Header("Particles")]
    public ParticleSystem dustParticles;    // dust particles when driving fast

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _speed = 30;
        _minSpeed = _speed;
        _scaleX = 1;
        _acceleration = 1.005f;
        _decelaration = 1.5f;
        _maxSpeed = 100;
        _angle = 10;
    }

    // Update is called once per frame
    void Update() {
        transform.localScale = new Vector3(_scaleX, _scaleYZ, _scaleYZ);
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
        _anim.SetFloat("Speed", Input.GetAxis(horizontalAxis));
        if (Input.GetAxis(horizontalAxis) > .1f) {
            dustParticles.enableEmission = true;
            _speed *= _acceleration;
            _rigidbody.velocity = new Vector2(_speed * Time.deltaTime, 0);
            _scaleX = 1;
        }
        else if (Input.GetAxis(horizontalAxis) < -.1f) {
            dustParticles.enableEmission = true;
            _speed *= _acceleration;
            _rigidbody.velocity = new Vector2(-_speed * Time.deltaTime, 0);
            _scaleX = -1;
        }
        else {
            _speed -= _decelaration;
            if (_speed <= _minSpeed) {
                _speed = _minSpeed;
                dustParticles.enableEmission = false;
            }
        }

        if (_speed >= _maxSpeed)
            _speed = _maxSpeed;
    }

    /// <summary>
    /// Control the angle of the gun
    /// </summary>
    private void GunControl() {
        _angle += Input.GetAxis(verticalAxis) * Time.deltaTime * 10;
        _angle = Mathf.Clamp(_angle, 0, 60);
        gunHolder.transform.localRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }

    /// <summary>
    /// Fires a bullet to the right direction with the correct angle of the gun angle
    /// Also give the tank backfire
    /// </summary>
    private void Fire() {

        if (Input.GetButtonDown(jump)) {
            if (_scaleX == 1) {
                _newBullet = Instantiate(bulletPrefab, new Vector3(gunPosition.position.x, gunPosition.position.y, gunPosition.position.z + .1f), gunHolder.transform.localRotation) as GameObject;
                _newBullet.transform.localScale = new Vector3(_scaleX, _scaleYZ, _scaleYZ);
                _newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _newBullet.GetComponent<TankBullet>().bulletSpeed, ForceMode2D.Force);
                _rigidbody.AddForce(Vector2.left * _backFire);
            }
            else if (_scaleX == -1) {
                _newBullet = Instantiate(bulletPrefab, new Vector3(gunPosition.position.x, gunPosition.position.y, gunPosition.position.z + .1f),
                                       new Quaternion(gunHolder.transform.localRotation.x, gunHolder.transform.localRotation.y, 
                                                        -gunHolder.transform.localRotation.z, gunHolder.transform.localRotation.w)) as GameObject;
                _newBullet.transform.localScale = new Vector3(_scaleX, _scaleYZ, _scaleYZ);
                _newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _newBullet.GetComponent<TankBullet>().bulletSpeed, ForceMode2D.Force);
                _rigidbody.AddForce(Vector2.right * _backFire);
            }
            _newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _newBullet.GetComponent<TankBullet>().upForce * _angle, ForceMode2D.Force);
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), _newBullet.GetComponent<Collider2D>());
        }
    }
}