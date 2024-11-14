using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class NewCharacterController : MonoBehaviour
{
    //simple unity character controller, allowing movement in x and z axis
    //no rotation, only wasd
    public float speed = 10.0f;
    public float return_speed = 5.0f;
    public float forward_speed = 5.0f;
    public float jumpForce = 500.0f;
    public bool canJump = true;
    public bool grounded = false;
    public Rigidbody rb;
    public Animator animator;

    public GameObject cameraTarget;
    public float movementIntensity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, forward_speed);
            if (canJump)
            {
                if (grounded)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        rb.AddForce(Vector3.up * jumpForce);
                    }
                }
            }
            if (grounded)
            {

                //add wasd controls but dont use addforce, juts set velocity


                if (Input.GetKey(KeyCode.A))
                {
                    //complete the following line

                    rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
                }
                //the below line but fixed
                else
                {

                    if (Mathf.Abs(transform.position.x) > 0.5)
                    {
                        rb.velocity = new Vector3(transform.position.x > 0 ? -return_speed : return_speed, rb.velocity.y, rb.velocity.z);
                    }
                    else
                    {
                        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                    }
                }

            }
            //set a max x position
            if (transform.position.x > 3)
            {
                transform.position = new Vector3(3, transform.position.y, transform.position.z);

            }
            // set min
            if (transform.position.x < -3)
            {
                transform.position = new Vector3(-3, transform.position.y, transform.position.z);
            }

        }
    }
}
