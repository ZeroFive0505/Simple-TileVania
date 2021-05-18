using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D enemyFront;

    private bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        enemyFront = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyFront.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //Debug.Log("Enemy front is not touching ground");
            flip();
        }
        enemy_move();
    }


    void flip()
    {
        if(isFacingRight)
        {
            transform.localScale = new Vector2(-1f, 1f);
            isFacingRight = !isFacingRight;
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
            isFacingRight = !isFacingRight;
        }
    }

    void enemy_move()
    {
        if (isFacingRight)
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        else
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
    }
}
