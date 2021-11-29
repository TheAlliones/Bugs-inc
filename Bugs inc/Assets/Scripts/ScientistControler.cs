using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private Animator ani;
    private BoxCollider2D box;
    public bool left = false;
    private bool running = false;
    private bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        if (!dead)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < 10f)
            {
                if (!running)
                {
                    running = !running;
                }
                else
                {
                    if (left)
                    {
                        rb.AddForce(new Vector2(-10f, 0f));
                        if(isInFrontOfWall() && isGrounded())
                        {
                           rb.AddForce(new Vector2(-20f, 30f));
                        }
                    }
                    else
                    {

                        rb.AddForce(new Vector2(10f, 0f));
                        if (isInFrontOfWall() && isGrounded())
                        {
                          rb.AddForce(new Vector2(20f, 30f));

                        }
                    }

                }
            }
            else
            {
                running = false;
            }

            if (Vector3.Distance(player.transform.position, this.transform.position) < 4f)
            {
                PlayerControler pl = player.GetComponent<PlayerControler>();
                if (pl.playerBug.Equals(PlayerControler.PlayerBug.stag_beetle))
                {
                    dead = true;
                    running = false;
                }
            }
        
        }

        if (!dead)
        {
            if (running)
            {
                ani.SetInteger("state", 1);
            }
            else
            {
                ani.SetInteger("state", 0);
            }
        }
        else
        {
            this.rb.velocity = Vector2.zero;
            ani.SetInteger("state", 2);
            box.offset = new Vector2(-0.01278305f, -0.161397f);
            box.size = new Vector2(0.4827576f, 0.02195168f);
            this.gameObject.layer = LayerMask.NameToLayer("Scientist");
            rb.gravityScale = 5f;
            PlayerControler pl = player.GetComponent<PlayerControler>();
            if (pl.playerBug == PlayerControler.PlayerBug.stag_beetle)
            {
                this.rb.simulated = true;
            }
            else
            {
                if (this.rb.velocity.y == 0f)
                {
                    this.rb.simulated = false;
                }
                else
                {
                    this.rb.simulated = true;
                }
            }
            
        }

    }

    private bool isInFrontOfWall()
    {
        if (left)
        {
            return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.left, 0.2f, LayerMask.GetMask("Ground"));
        }
        else
        {
            return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.right, 0.2f, LayerMask.GetMask("Ground"));
        }

    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }
}
