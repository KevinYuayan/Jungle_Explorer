using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: EnemyController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Nov. 3, 2019
/// Description: Controller for the Skeleton/Enemy Object
/// Revision History:
/// </summary>
public class EnemyController : MonoBehaviour
{
    public SpriteRenderer enemySpriteRenderer;
    public Rigidbody2D enemyRigidBody;
    public bool isGrounded;
    public bool hasGroundAhead;
    public bool hasWallAhead;
    public Transform lookAhead;
    public Transform wallAhead;
    public bool isFacingRight = true;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position - new Vector3(0, enemySpriteRenderer.bounds.extents.y + 0.08f, 0), new Vector2(0.9f, 0.08f), 0.0f, Vector2.down, 0.08f, 1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        hasWallAhead = Physics2D.Linecast(
            transform.position,
            wallAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded)
        {
            if (isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(movementSpeed, 0.0f);
            }

            if (!isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(-movementSpeed, 0.0f);
            }

            if (!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                isFacingRight = !isFacingRight;
            }
        }
    }
}
