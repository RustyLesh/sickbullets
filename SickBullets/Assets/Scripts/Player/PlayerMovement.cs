using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Takes hotkey inputs and moves player accordingly.
 */
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public float accSpeed = 15f;
    public bool canJump;
    public bool canFall;
    public int deaths;
    public Text deathCount;
    public GameObject shotSpawn;

    public float jumpDelay;
    public float fallDelay;

    float tempMoveSpeed; //Keeps Value of base move speed
    float jumpDelayCounter;
    float fallDelayCounter;
    BoxCollider2D bc;
    Rigidbody2D rb;
    Animator playerAC;
    bool crouching;

    [HideInInspector] public bool isFliped;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        playerAC = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Movement

        //Player press A, MoveLeft
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

            //Stops Left movement after letting go of A
        if(Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Player press D, Move right
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

            //Stops right movement after letting go of D
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Jump
        if (Input.GetKey(KeyCode.Space) && canJump && !Input.GetKey(KeyCode.S) && Time.time > jumpDelayCounter)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpDelayCounter = Time.time + jumpDelay;
        }

        //JumpDown
        if (Input.GetKey(KeyCode.Space) && canJump && Input.GetKey(KeyCode.S) && canFall)
        {
            bc.isTrigger = true;
            jumpDelayCounter = Time.time + jumpDelay;
        }

        //
        if (bc.isTrigger && Time.time > fallDelayCounter)
        {
            bc.isTrigger = false;
            fallDelayCounter = Time.time + fallDelay;
        }

        //Run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tempMoveSpeed = moveSpeed;
            moveSpeed = accSpeed;
        }
            //Returns movespeed to default
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = tempMoveSpeed;
        }
        Animating();

    }

    void Animating()
    {
        // Crouch
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAC.SetBool("isCrouching", true);
            print("crouching");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAC.SetBool("isCrouching", false);
            print("stoped crouching");
        }

        //Flip, turn around
        if (Input.GetKey(KeyCode.A) && !isFliped)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            isFliped = true;
        }

        if (Input.GetKey(KeyCode.D) && isFliped)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;

            isFliped = false;
        }

        //Aim up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAC.SetBool("isAimingUp", true);
            print("aiming up");
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerAC.SetBool("isAimingUp", false);
            print("Stoped aiming up");
        }

        //Aim down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAC.SetBool("isAimingDown", true);
            print("aiming down");
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerAC.SetBool("isAimingDown", false);
            print("Stoped aiming  down");
        }

        //aim back
        if (isFliped)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerAC.SetBool("isAimingBack", true);
                print("aiming back");
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerAC.SetBool("isAimingBack", false);
                print("Stoped aiming back");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerAC.SetBool("isAimingBack", true);
                print("aiming back");
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerAC.SetBool("isAimingBack", false);
                print("Stoped aiming back");
            }
        }


    }
}
