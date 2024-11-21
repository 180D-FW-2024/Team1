using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class NewCharacterController : MonoBehaviour
{
    //simple unity character controller, allowing movement in x and z axis
    //no rotation, only wasd
    public float speed = 10.0f;
    public float return_speed = 5.0f;
    public float forward_speed = 5.0f;
    public float jumpForce = 500.0f;
    public float max_x = 3.3f;
    public bool canJump = true;
    public bool grounded = false;
    public Rigidbody rb;
    public Animator animator;
    public Text coinText;

    public GameObject cameraTarget;
    public GameObject wide_fence_obstacle;
    public float movementIntensity;
    private bool dead = false;
    private int coins = 0;
    private bool add_obstacle_unlocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && add_obstacle_unlocked)
        {
            add_obstacle_unlocked = false;
            coinText.text = "Coins: " + coins + (add_obstacle_unlocked ? "         PRESS P TO SEND ATTACK" : "");
            GameObject obj_spawned = Instantiate(wide_fence_obstacle, new Vector3(4.4f, 4f, transform.position.z + 20), Quaternion.identity);
        }
        forward_speed = 9 + transform.position.z / 50;
        speed = 10 + transform.position.z / 100;
        if (rb != null && !dead)
        {
            if (transform.position.y > 3.1 && transform.position.y < 4.9)
            {
                grounded = true;
                
            }
            else
            {
                grounded = true;
            }
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, forward_speed);
            if (grounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetBool("Jump", true);
                    rb.AddForce(Vector3.up * jumpForce);
                }

                //add wasd controls but dont use addforce, juts set velocity
                animator.SetBool("Run Forward", true);
                animator.SetBool("Idle", false);

                

            }
            else
            {
                //if key down add downward speed
                if (Input.GetKey(KeyCode.S))
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y-2, rb.velocity.z);
                }
                animator.SetBool("Run Forward", false);
            }

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


            //set a max x position
            if (transform.position.x > max_x)
            {
                transform.position = new Vector3(max_x, transform.position.y, transform.position.z);

            }
            // set min
            if (transform.position.x < -max_x)
            {
                transform.position = new Vector3(-max_x, transform.position.y, transform.position.z);
            }
            if (transform.position.y < -1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            dead = true;
            forward_speed = 0;
            animator.SetBool("Death", true);
            animator.SetBool("Run Forward", false);
            animator.SetBool("Jump", false);

        }
        if (other.tag == "Coin")
        {
            coins++;
            coinText.text = "Coins: " + coins + (add_obstacle_unlocked ? "         PRESS P TO SEND ATTACK":"");
            Destroy(other.gameObject);
        }
        if(other.tag == "AddObstacle")
        {
            add_obstacle_unlocked = true;
            coinText.text = "Coins: " + coins + (add_obstacle_unlocked ? "         PRESS P TO SEND ATTACK": "");
            Destroy(other.gameObject);
        }
    }
}
