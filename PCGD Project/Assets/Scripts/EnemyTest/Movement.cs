using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    public float enemySpd = 10f;
    private int moveDir;
    private int curDir; // You can store your current direction here but I'm not using it in this code. This is how you would do it though
    private bool canChange;
    private Vector2 moveDirection;
    float rotationOffset = 90f;
    Transform player;

    //public Movement movement;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
        if (moveDir < 1 || moveDir > 4)
        {
            while (moveDir < 1 || moveDir > 4) { moveDir = Random.Range(1, 5); }
        }

        canChange = true; // Allows the enemy to pick a random direction when true
    }

    private void Update()
    {
        if (PlayerDetection.playerIsDetected == false)
        {
            MovementHandler();
            enemyRb.MovePosition(enemyRb.position + (moveDirection * enemySpd) * Time.fixedDeltaTime);
        }
        else
        {
            Move();
            Rotate();
        }
    }


    void ChangeDirection()
    {
        // Can move in any direction
        if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == true &&
                                                                                                 WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
            if (moveDir < 1 || moveDir > 4)
            {
                while (moveDir < 1 || moveDir > 4)
                {
                    moveDir = Random.Range(1, 5);
                    if (moveDir == curDir)
                    {
                        moveDir = Random.Range(1, 5);
                    }
                }// End of While loop
            }// End of IF moveDir statement
        } // End of all paths open IF statement

        // Cannot move right
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == false &&
                                                                                                     WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
            while (moveDir < 1 || moveDir > 4 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }// End of While loop
        }// End of Right path closed all other paths open Statement

        // Cannot move Left or Right
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == false &&
                                                                                                      WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            while (!(moveDir == 1) && !(moveDir == 4))
            {
                moveDir = Random.Range(1, 5);
            }// End of While Loop
        }// End of Up or Down path open IF Statement

        // Can only move up
        else if (WallDetection.pathOpenDown == false && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == false &&
                                                                                                       WallDetection.pathOpenUp == true)
        {
            moveDir = 4;
        } // End of Up movement only IF statement

        // Can only move Up or Right
        else if (WallDetection.pathOpenDown == false && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == true &&
                                                                                                      WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);// 1-4  -- 1=down 2=left 3=right 4=up
            while (!(moveDir == 3) &&  !(moveDir == 4)) // can only be 3 or 4
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of Up or Right Movement IF statement

        // Can move Left, Right or Up
        else if (WallDetection.pathOpenDown == false && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == true &&
                                                                                                     WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir > 4 || moveDir < 2) // Cannot be 1
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of Left,Right,Up IF statement

        // Can only move Left or Up
        else if (WallDetection.pathOpenDown == false && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == false &&
                                                                                                      WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5); // 1 = down 2 = left 3 = right 4 = up
            while (!(moveDir == 2) && !(moveDir == 4))
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of Left/Up IF statement

        // Cannot move left
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == true &&
                                                                                                     WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir < 1 || moveDir > 4 || moveDir == 2)
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of Down, Right, Up IF statement

        // Can move any direction except Up
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == true &&
                                                                                                   WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir < 1 || moveDir > 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of any direction BUT Up statement

        // Cannot move Right or Up
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == false &&
                                                                                                   WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5); // 1 = down 2 = left 3 = right 4 = up
            while (!(moveDir == 1) && !(moveDir == 2))
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of can't move Right or Up statement

        // Cannot move Left or Up
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == true &&
                                                                                                   WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            while (!(moveDir == 1) && !(moveDir == 3))
            {
                moveDir = Random.Range(1, 5);
            }
        }// End of Can't move Left or Up statement

        // Can only move Down
        else if (WallDetection.pathOpenDown == true && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == false &&
                                                                                                   WallDetection.pathOpenUp == false)
        {
            moveDir = 1;
        } // End of can only move Down statement

        // Can only move Left
        else if (WallDetection.pathOpenDown == false && WallDetection.pathOpenLeft == true && WallDetection.pathOpenRight == false &&
                                                                                                   WallDetection.pathOpenUp == false)
        {
            moveDir = 2;
        } // End of can only move Left statement

        // Can only move Right
        else if (WallDetection.pathOpenDown == false && WallDetection.pathOpenLeft == false && WallDetection.pathOpenRight == true &&
                                                                                                   WallDetection.pathOpenUp == false)
        {
            moveDir = 3;
        } // End of can only move Right

        else { moveDir = 4; } // In case we forgot a scenario or since we didn't add move up only, we'll move Up by default
    }

    void MoveLeft()
    {
        curDir = 2;
        enemyRb.transform.eulerAngles = new Vector3(0, 0, -90);
        moveDirection = Vector2.left;
    }

    void MoveRight()
    {
        curDir = 3;
        enemyRb.transform.eulerAngles = new Vector3(0, 0, 90);
        moveDirection = Vector2.right;
    }

    void MoveUp()
    {
        curDir = 4;
        enemyRb.transform.eulerAngles = new Vector3(0, 0, 180);
        moveDirection = Vector2.up;
    }

    void MoveDown()
    {
        curDir = 1;
        enemyRb.transform.eulerAngles = new Vector3(0, 0, 0);
        moveDirection = Vector2.down;
    }

    void FollowPlayer()
    {
        enemyRb.transform.position = Vector2.MoveTowards(enemyRb.position, player.position, 0.2f); 
    }

    void MovementHandler()
    {
        if (moveDir == 1)
        {
            if (WallDetection.pathOpenDown == true)
            {
                MoveDown();
            }
            else { ChangeDirection(); }
        }
        else if (moveDir == 2)
        {
            if (WallDetection.pathOpenLeft == true)
            {
                MoveLeft();
            }
            else { ChangeDirection(); }
        }
        else if (moveDir == 3)
        {
            if (WallDetection.pathOpenRight == true)
            {
                MoveRight();
            }
            else { ChangeDirection(); }
        }
        else if (moveDir == 4)
        {
            if (WallDetection.pathOpenUp == true)
            {
                MoveUp();
            }
            else { ChangeDirection(); }
        }

        if (canChange == true)
        {
            StartCoroutine(RandomMovement());
        }
    }

    IEnumerator RandomMovement()
    {
        canChange = false;
        var timer = Random.Range(1.0f, 2.0f);
        yield return new WaitForSeconds(timer);
        ChangeDirection();
        yield return new WaitForSeconds(0.5f);
        canChange = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerDetection.playerIsDetected == false)
        {
            if (collision.gameObject.tag == "GameAreaBorder")
            {
                ChangeDirection();
            }
        }
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpd * Time.deltaTime);
    }

    void Rotate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + rotationOffset));
    }
}