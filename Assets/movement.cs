using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class movement : MonoBehaviour
{


    //add coyote time for jumps
    //add dash
    //add wall jump lol
    //add gizmos for debugging


    public const float DEFAULT_FORCE = 2.0f;
    //ublic const float MAX_AIRSPEED;
    public const float MAX_SPEED = 10.0f;

    public float gravity = -20.0f;

    public bool isGrounded = true;

    public float friction = -0.2f;

    public float xForce = DEFAULT_FORCE;
    public float zForce = DEFAULT_FORCE;
    public float yForce = 500;
    
    public float xForce_air = 1.0f;
    public float zForce_air = 1.0f;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    public Transform cam;

    private Rigidbody rb;

    //use this for initialization  

    void Start()
    {

    }
    //Update is called once per frame  
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        //air control settings, there is probably a better way to do this but whatever 
        if (!isGrounded)
        {
            xForce = xForce_air;
            zForce = zForce_air;

        }
        else
        {
            xForce = DEFAULT_FORCE;
            zForce = DEFAULT_FORCE;
        }


        //this is for x axis' movement  
        float x = 0.0f;
        if (Input.GetKey(KeyCode.A))
        {
            x = x - xForce;
        }
 

        if (Input.GetKey(KeyCode.D))
        {
            x = x + xForce;
        }

        //this is for z axis' movement  

        float z = 0.0f;
        if (Input.GetKey(KeyCode.S))
        {
            z = z - zForce;
        }

        if (Input.GetKey(KeyCode.W))
        {
            z = z + zForce;
        }
        //this is for z axis' movement  


        //jump only if sphere is low enough to the ground
        //add coyote time + jump queueing to make things more responsive 
        float y = 0.0f;
        //if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Rigidbody>().worldCenterOfMass.y <= 1.2 && isGrounded)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            y = yForce;
            isGrounded = false;
        }

        

        if (!Input.anyKey)
        {
            //checks if ball is moving slow enough to stop rolling
            //applied friction if moving fast enough


            if (rb.velocity.magnitude < 0.01)
            {
                rb.velocity = new Vector3(0, 0, 0);

            }else if (rb.velocity.magnitude > 0.01)
            {
                rb.AddForce(rb.velocity * friction);
            }



        }
        
        
            Vector3 forcevector = new Vector3(x, y, z);
            //this is for turning characteer towards movement direction
            float targetAngle = Mathf.Atan2(forcevector.normalized.x, forcevector.normalized.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            //Debug.Log(moveDir);
            rb.AddForce(moveDir.normalized.x * DEFAULT_FORCE, y, moveDir.normalized.z * DEFAULT_FORCE);
        

        
        


        


    }


    //make sure collision happens from the ground and not from side
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("colliding");
        if(collision.gameObject.name.Contains("Terrain"))
        {
            //Debug.Log("touching terrain");
            isGrounded = true;
        }
        
        
    }


    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        //consider this is the wrong type of magnitude, should be looking at x,y not the entire vector
        if (rb.velocity.magnitude > MAX_SPEED)
        {
            //rb.velocity = GetComponent<Rigidbody>().velocity.normalized * MAX_SPEED;


            //trying to apply max speed only in the x,z plane (where the character can move)
            //trying not to apply max speed to gravity
            //keep velocity in the y
            float vy = rb.velocity.y;
            //get x, and z normals
            float nx = rb.velocity.normalized.x;
            float nz = rb.velocity.normalized.z;
            //limit speeds only in the x,z direction. 
            rb.velocity = new Vector3(nx * MAX_SPEED, vy, nz * MAX_SPEED);
            //Debug.Log(rb.velocity);
            //Debug.Log(rb.velocity.magnitude);


        }

        //add gravity
        rb.AddForce(new Vector3(0,gravity, 0));


    }

     


}