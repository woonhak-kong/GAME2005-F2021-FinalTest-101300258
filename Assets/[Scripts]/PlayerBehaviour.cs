using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;

    public MyPhysicObject body;
    //public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float fireRate = 1;
    public float LauchSpeed = 4;
    private float LimitFiring = 0;
    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainSceneUI.IsStart)
        {
            _Fire();
            _Move();
        }
    }

    private void _Move()
    {
        Vector3 orientation;
        //if (isGrounded)
        //{
        if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            // forward right
            orientation = (transform.right + transform.forward) * 0.5f;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
        {
            // forward left
            orientation = (-transform.right + transform.forward) * 0.5f;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            // back right
            orientation = (transform.right + -transform.forward) * 0.5f;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0.0f && Input.GetAxisRaw("Vertical") < 0.0f)
        {
            // back left
            orientation = (-transform.right + -transform.forward) * 0.5f;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            // move right
            orientation = transform.right;
            //body.Velocity.x = transform.right * speed;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            // move left
            orientation = -transform.right;
            //body.Velocity.x = -transform.right * speed;
        }
        else if (Input.GetAxisRaw("Vertical") > 0.0f)
        {
            // move forward
            orientation = transform.forward;
            //body.Velocity.z = transform.forward * speed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0.0f)
        {
            // move Back
            orientation = -transform.forward;
            //body.Velocity.z = -transform.forward * speed;
        }
        else
        {
            orientation = Vector3.zero;
        }

        body.Velocity.x = orientation.x * speed;
        body.Velocity.z = orientation.z * speed;

        //body.Velocity = Vector3.Lerp(body.Velocity, Vector3.zero, 0.9f);
        //body.Velocity = new Vector3(body.Velocity.x, 0.0f, body.Velocity.z); // remove y


        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Debug.Log("space");
            body.Velocity.y = transform.up.y * 6;
            //body.Velocity = body.Velocity + transform.up * speed;
        }

        //transform.position += body.Velocity;
        //}
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0 && LimitFiring > fireRate)
        {
            //var randomSphereIndex = Random.Range(0, spherePrefabs.Count);
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward * 0.4f, Quaternion.identity);
            bullet.GetComponent<MyPhysicObject>().Velocity = bulletSpawn.forward * LauchSpeed;
            bullet.GetComponent<MyPhysicObject>().NewPosition = bullet.GetComponent<MyPhysicObject>().transform.position;

            MyPhysicsSystem system = FindObjectOfType<MyPhysicsSystem>();
            system.gameObjectList.Add(bullet.GetComponent<MyPhysicObject>());

            LimitFiring = 0.0f;
        }

        //if (Input.GetAxisRaw("Fire1") > 0.0f)
        //{
        //    // delays firing
        //    if (Time.frameCount % fireRate == 0)
        //    {

        //        var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
        //        tempBullet.transform.SetParent(bulletManager.gameObject.transform);
        //    }
        //}
        LimitFiring += Time.deltaTime;
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
