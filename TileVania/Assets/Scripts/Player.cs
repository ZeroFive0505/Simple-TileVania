using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D feet;
    private CapsuleCollider2D playerBody;
    private GameObject enemy;

    //private bool right;
    //private Quaternion from = Quaternion.Euler(0f, 0f, 0f);
    //private Quaternion to = Quaternion.Euler(0f, 180f, 0f);



    
    [SerializeField] float speed = 5f;
    [SerializeField] float jump_speed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 10f);

    private bool isAlive;
    private int jump_count;
    private float gravity_origin;

    // Start is called before the first frame update
    void Start()
    {
        //right = true;
        jump_count = 0;
        isAlive = true;

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        feet = GetComponent<BoxCollider2D>();
        playerBody = GetComponent<CapsuleCollider2D>();
        animator.SetBool("Climbing", false);
        animator.SetBool("Running", false);
        gravity_origin = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
  
        if(!isAlive)
        {
            return;
        }
        run();
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")) || jump_count < 2)
        {
            jump();
        }

        climbing();
        flip();


        if (playerBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            if(isAlive)
            {
                rb2d.velocity = deathKick;
                animator.SetBool("Death", true);
                FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
            isAlive = false;
        }
    }


    private void run()
    {
        float horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");
        bool isMoving = Mathf.Abs(horizontalMove) > Mathf.Epsilon;
        animator.SetBool("Running", isMoving);
        Vector2 horizontal = new Vector2(horizontalMove * speed, rb2d.velocity.y);
        rb2d.velocity = horizontal;
        //if (x < 0 && right)
        //    flip();
        //else if (x > 0 && !right)
        //    flip();
        ////Debug.Log("X : " + x);
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    if (right)
        //        flip();
        //    //rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        //    //transform.position += Vector3.left * speed * Time.deltaTime;

        //    rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
        //    animator.SetBool("Running", true);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    animator.SetBool("Running", false);
        //    rb2d.velocity = new Vector2(0, transform.position.y);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    if(!right)
        //        flip();
        //    //rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        //    //transform.position += Vector3.right * speed * Time.deltaTime;
        //    rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);
        //    animator.SetBool("Running", true);
        //}
        //if(Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    rb2d.velocity = new Vector2(0, transform.position.y);
        //    animator.SetBool("Running", false);
        //}
    }

    void flip()
    {
        //if (!right)
        //    transform.rotation = Quaternion.Lerp(from, to, 0);
        //else
        //    transform.rotation = Quaternion.Lerp(to, from, 0);
        //right = !right;
        bool moving = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        if(moving)
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
    }

    void jump()
    {
        if(jump_count < 2)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpToAdd = new Vector2(0, jump_speed);
                rb2d.velocity += jumpToAdd;
                jump_count++;
            }
        }
        else
        {
            if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
                jump_count = 0;
        }
    }

    void climbing()
    {

        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            animator.SetBool("Climbing", false);
            rb2d.gravityScale = gravity_origin;
            return;
        }

        rb2d.gravityScale = 0;
        float verticalMove = CrossPlatformInputManager.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(verticalMove) > Mathf.Epsilon;
        animator.SetBool("Climbing", isMoving);
      
        Vector2 vertical = new Vector2(rb2d.velocity.x, verticalMove * speed);
        rb2d.velocity = vertical;
    }
}
