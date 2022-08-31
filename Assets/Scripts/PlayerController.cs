using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode shoot;

    private Rigidbody2D theRB;

    public Transform groundCheckPoint;

    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator anim;

    public GameObject bullet;
    public Transform firePoint;

    //public AudioSource shootSound;
    
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("Grounded", isGrounded);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
        }

        if (Input.GetKeyDown(jump) && isGrounded == true)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    PlayerPrefs.DeleteKey("PrefScores");
        //    PlayerPrefs.DeleteKey("PrefNames");
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Debug.Log("PlayerPrefs scores outputted");
        //    Debug.Log("Scores: " + PlayerPrefs.GetString("PrefScores"));
        //    Debug.Log("Names: " + PlayerPrefs.GetString("PrefNames"));
        //}
    }
}
