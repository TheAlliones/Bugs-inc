using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer red;
    private Animator ani;
    private Collider2D waterCollider;
    private AudioSource audioSource;

    private float dirX = 0f;
    private bool isJumping = false;
    private float glidingScale = -1.5f;
    private int animationState = 0;
    public enum PlayerBug { ladybug, grasshopper, dung_beetle, stag_beetle, water_strider}

    private float jumpHight = 7f;
    private float movementSpeed = 7f;

    public PlayerBug playerBug;
    public RuntimeAnimatorController dung_beetle;
    public RuntimeAnimatorController ladybug;
    public RuntimeAnimatorController grasshopper;
    public RuntimeAnimatorController stag_beetle;
    public RuntimeAnimatorController water_strider;
    public UIManager uiManager;
    public GameObject water;



    void Start()
    {
        water = GameObject.Find("Grid/Water");
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        red = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        waterCollider = water.GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
       
    }
    void Update()
    {
        UpdateBugChoosen();
        UpdateMovement();
        UpdateAnimation();
    }

    private void UpdateMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if(playerBug != PlayerBug.grasshopper)
        {
            rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
        }
        if (dirX > 0)
        {

            if (isGrounded())
            {
                animationState = 1;
                if(playerBug == PlayerBug.grasshopper)
                {
                    if ((rb.velocity.y > -0.1f)&& (rb.velocity.y < 0.1f))
                    {
                        rb.velocity = new Vector2(dirX * movementSpeed, jumpHight);
                        isJumping = true;
                        animationState = 2;
                        audioSource.Play();
                        transform.localScale = new Vector3(-5, 5, 5);
                    }

                }
                else
                {
                    transform.localScale = new Vector3(-5, 5, 5);
                }
            }
            else if (playerBug != PlayerBug.grasshopper)
            {
                transform.localScale = new Vector3(-5, 5, 5);
            }


        }
        else if (dirX < 0)
        {
            if (isGrounded())
            {
                animationState = 1;
                if (playerBug == PlayerBug.grasshopper)
                {
                    if ((rb.velocity.y > -0.1f) && (rb.velocity.y < 0.1f))
                    {
                        rb.velocity = new Vector2(dirX * movementSpeed, jumpHight);
                        isJumping = true;
                        animationState = 2;
                        audioSource.Play();
                        transform.localScale = new Vector3(5, 5, 5);
                    }
                }
                else
                {
                    transform.localScale = new Vector3(5, 5, 5);
                }
            }else if(playerBug != PlayerBug.grasshopper)
            {
                transform.localScale = new Vector3(5, 5, 5);
            }

        }
        else if(animationState == 1)
        {
            animationState = 0;
        }

        if((playerBug == PlayerBug.grasshopper)&&!isGrounded()&& (rb.velocity.x > -0.1f) && (rb.velocity.x < 0.1f))
        {
            rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
        }



        if (((Input.GetKeyDown(KeyCode.W)) || (Input.GetButtonDown("Jump"))) && isGrounded())
        {
            if(playerBug != PlayerBug.grasshopper)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHight);
                isJumping = true;
                animationState = 2;
                audioSource.Play();
            }
        }

        if (isJumping&& rb.velocity.y < 0)
        {
            isJumping = false;
        }
        if(!isGrounded()&&playerBug == PlayerBug.ladybug&&!isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, glidingScale);
        }


        if (!isGrounded() && !isJumping)
        {
            animationState = 3;
        }
        if((animationState == 3) && isGrounded())
        {
            animationState = 0;
        }
    }
    private void UpdateBugChoosen()
    {
        if(playerBug == PlayerBug.dung_beetle)
        {
            box.offset = new Vector2(-0.0374521f, -0.3409437f);
            box.size = new Vector2(0.4487373f, 0.3125107f);
            ani.runtimeAnimatorController = dung_beetle;
            jumpHight = 7f;
            movementSpeed = 2;
            water.layer = LayerMask.NameToLayer("Water");
            waterCollider.enabled = false;
        }
        else if (playerBug == PlayerBug.grasshopper)
        {
            box.offset = new Vector2(-0.02498262f, -0.3431821f);
            box.size = new Vector2(0.5254813f, 0.3122787f);
            ani.runtimeAnimatorController = grasshopper;
            jumpHight = 16;
            movementSpeed = 8f;
            water.layer = LayerMask.NameToLayer("Water");
            waterCollider.enabled = false;
        }
        else if (playerBug == PlayerBug.ladybug)
        {
            box.offset = new Vector2(-0.03980691f, -0.4304265f);
            box.size = new Vector2(0.2320953f, 0.1335449f);
            ani.runtimeAnimatorController = ladybug;
            jumpHight = 7f;
            movementSpeed = 7f;
            water.layer = LayerMask.NameToLayer("Water");
            waterCollider.enabled = false;
        }
        else if (playerBug == PlayerBug.stag_beetle)
        {
            box.offset = new Vector2(0.005937606f, -0.3668149f);
            box.size = new Vector2(0.7887405f, 0.2650131f);
            ani.runtimeAnimatorController = stag_beetle;
            jumpHight = 10f;
            movementSpeed = 13;
            water.layer = LayerMask.NameToLayer("Water");
            waterCollider.enabled = false;
        }
        else if (playerBug == PlayerBug.water_strider)
        {
            box.offset = new Vector2(-0.03520145f, -0.3738385f);
            box.size = new Vector2(0.5459189f, 0.2509658f);
            ani.runtimeAnimatorController = water_strider;
            jumpHight = 4f;
            movementSpeed = 9f;
            water.layer = LayerMask.NameToLayer("Ground");
            waterCollider.enabled = true;



        }
    }
    private void UpdateAnimation()
    {
        ani.SetInteger("state", animationState);
    }
    private bool isGrounded()
    {
        if(Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground")))
        {
            return true;
        }
        else if (Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Box")))
        {
            return true;
        }
        else if(Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Scientist")))
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    public void ChangePlayerTo0()
    {
        playerBug = PlayerBug.ladybug;
        uiManager.UIClicked();
    }
    public void ChangePlayerTo1()
    {
        playerBug = PlayerBug.grasshopper;
        uiManager.UIClicked();
    }
    public void ChangePlayerTo2()
    {
        playerBug = PlayerBug.dung_beetle;
        uiManager.UIClicked();
    }
    public void ChangePlayerTo3()
    {
        playerBug = PlayerBug.stag_beetle;
        uiManager.UIClicked();
    }
    public void ChangePlayerTo4()
    {
        playerBug = PlayerBug.water_strider;
        uiManager.UIClicked();
    }
}
