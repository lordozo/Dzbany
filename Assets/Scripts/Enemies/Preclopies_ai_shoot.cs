﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preclopies_ai_shoot : MonoBehaviour
{
    public int enemySpeed;
    public int xMoveDirection;
    public bool hitGround = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0.1f;    
    }

    // Update is called once per frame
    void Update()
    {
        if(hitGround)
        if (!GlobalVariables.isPaused)
        {
            Vector2 size = GetComponent<BoxCollider2D>().size;
            Vector2 positiontopleft = new Vector2(transform.position.x - size.x / 2, transform.position.y);
            Vector2 positiontopright = new Vector2(positiontopleft.x + size.x, positiontopleft.y);


            Vector2 positiontopleftoff = new Vector2(positiontopleft.x - 0.1f, positiontopleft.y);
            Vector2 positiontoprightoff = new Vector2(positiontopright.x + 0.1f, positiontopright.y);

            RaycastHit2D left = Physics2D.Raycast(positiontopleft, Vector2.left);
            RaycastHit2D right = Physics2D.Raycast(positiontopright, Vector2.right);

            RaycastHit2D leftdown = Physics2D.Raycast(positiontopleftoff, Vector2.down);
            RaycastHit2D rightdown = Physics2D.Raycast(positiontoprightoff, Vector2.down);



            if (left != null && left.collider != null && left.distance <= 0.3f && xMoveDirection < 0 && (left.collider.tag.Equals("Ground") || left.collider.tag.Equals("Enemy") || left.collider.tag.Equals("Electro")))
            {
                ChangeDirection();
                if (left.collider.tag.Equals("Enemy"))
                {
                    if (left.collider.gameObject.GetComponent<Preclopies_ai_shoot>() != null)
                        left.collider.gameObject.GetComponent<Preclopies_ai_shoot>().ChangeDirection();
                }

            }
            else if (right != null && right.collider != null && right.distance <= 0.3f && xMoveDirection > 0 && (right.collider.tag.Equals("Ground") || right.collider.tag.Equals("Enemy") || right.collider.tag.Equals("Electro")))
            {
                ChangeDirection();
                if (right.collider.tag.Equals("Enemy"))
                {
                    if (right.collider.gameObject.GetComponent<Preclopies_ai_shoot>() != null)
                        right.collider.gameObject.GetComponent<Preclopies_ai_shoot>().ChangeDirection();
                }

            }
            else if (leftdown != null && leftdown.collider != null && leftdown.distance >= 3f && xMoveDirection < 0 && (leftdown.collider.tag.Equals("Ground") || leftdown.collider.tag.Equals("Enemy") || leftdown.collider.tag.Equals("Electro")))
            {
                ChangeDirection();

            }
            else if (rightdown != null && rightdown.collider != null && rightdown.distance >= 3f && xMoveDirection > 0 && (rightdown.collider.tag.Equals("Ground") || rightdown.collider.tag.Equals("Enemy") || rightdown.collider.tag.Equals("Electro")))
            {
                ChangeDirection();

            }
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Vector2 player_position = collision.collider.transform.position;


            Vector2 player_size = collision.collider.GetComponent<BoxCollider2D>().size * collision.collider.gameObject.transform.localScale;


            player_position.y -= player_size.y / 2 - 0.1f;

            Vector2 enemy_position = transform.position;

            Vector2 enemy_size = gameObject.GetComponent<BoxCollider2D>().size * gameObject.transform.localScale;

            enemy_position.y += enemy_size.y / 2;


            if (player_position.y >= enemy_position.y)
            {
                Destroy(gameObject);
                collision.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
            }
            else
            {
                if (collision.collider.tag.Equals("Player"))
                {
                    collision.gameObject.GetComponent<Player_Health>().TakeDamage(20);
                }

            }
        } else if (collision.gameObject.tag.Equals("Ground")) {
            hitGround = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            int i =  Random.Range(0, 2);
            int[] k = { 1, -1 };
            GetComponent<Preclopies_ai_shoot>().xMoveDirection = k[i];
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Vector2 player_position = collision.collider.transform.position;


            Vector2 player_size = collision.collider.GetComponent<BoxCollider2D>().size * collision.collider.gameObject.transform.localScale;


            player_position.y -= player_size.y / 2 - 0.1f;

            Vector2 enemy_position = transform.position;

            Vector2 enemy_size = gameObject.GetComponent<BoxCollider2D>().size * gameObject.transform.localScale;

            enemy_position.y += enemy_size.y / 2;


            if (player_position.y >= enemy_position.y)
            {
                Destroy(gameObject);
                collision.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
            }
            else
            {
                if (collision.collider.tag.Equals("Player"))
                {
                    collision.gameObject.GetComponent<Player_Health>().TakeDamage(20);
                }

            }
        } else if (collision.gameObject.tag.Equals("Ground")) {
            hitGround = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void ChangeDirection()
    {
        xMoveDirection *= -1;
    }

}
