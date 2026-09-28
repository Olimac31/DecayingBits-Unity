using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float movX; // keyLeft/keyRight replacement
    public float intMove = 0;
    public bool keyJump, keyJumpDown, keyJumpUp, keyInteract;
    
    public const float MULTIPLIER = 1.0f;
    float GRAVITY = 1.0f;
    float WALK_SPEED = 1.2f * MULTIPLIER; // 3.5
    float AIR_WALK_SPEED = 2.4f * MULTIPLIER; // 4.0
    float JUMP_SPEED = 32.0f * MULTIPLIER; // 6.5
    float WALKSPD = 18.0f * MULTIPLIER; // 10.0
    float RUNSPD = 26.0f * MULTIPLIER;
    float SKIDDING_MULTIPLIER = 3.0f;
    float MAX_SPEED;
    public Vector2 velocity = new Vector2(0, 0);

    float AIR_GRAVITY = 45.0f * MULTIPLIER; // 12.0f
    float GROUND_GRAVITY = 120.0f * MULTIPLIER;
    
    public bool reading = false;
    public int facingDirection = -1;
    detects_collision colFloor;
    detects_collision colHead;

    // Jump variables
    public bool jumped = false;
    public float coyote = 0f;
    public float coyoteMAX = 20f;
    public bool has_max_vspeed = true;
    public float max_vspeed = 20f * MULTIPLIER;

    
    Rigidbody2D rbody;
    //Collider2D myCollider;
    //playerAudio myAudio;
    //public playerHealth myHealth;
    
    //public bool isGrounded = false;

    public GameObject colLeft;
    public GameObject colRight;
    float wallJumpLock = 0;
    float wallJumpLockMAX = 10f;
    int wallJumpSide = 0;

    public bool ignore_gravity = false;

    // Custom Characters -------------------------------

    public bool has_walljump = true;
    public bool has_air_friction = true;
    public bool has_skidding_physics = true;
    public bool doubleJump = false;

    public Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        Transform find_colfloor = transform.Find("colFloor");
        colFloor = find_colfloor.GetComponent<detects_collision>();

        Transform find_colhead = transform.Find("colHead");
        colHead = find_colhead.GetComponent<detects_collision>();

        startPos = new Vector2(transform.position.x, transform.position.y);
        velocity = Vector2.zero;
        MAX_SPEED = WALKSPD;
    }

    // Update is called once per frame
    void Update()
    {
        rbody.gravityScale = 0;
        // INPUTS ---------------------------------------------
        if(!reading)
        {
            //Continuous inputs
            movX = Input.GetAxisRaw("Horizontal");
            keyJump = Input.GetButton("keyS");
            keyInteract = Input.GetButtonDown("keyUp");

            //Instantaneous inputs
            if(Input.GetButtonDown("keyS"))
            {
                keyJumpDown = true;
            }

            if(Input.GetButtonUp("keyS"))
            {
                keyJumpUp = true;
            }
        }
        else
        {
            movX = 0;
            intMove = 0;
            keyJump = false;
            keyJumpUp = false;
            keyJumpDown = false;
            keyInteract = false;
        }
        if (movX != 0)
        {
        intMove = Mathf.Sign(movX);
        }
        else
        {
            intMove = 0;
        }
    }
    void FixedUpdate()
    {
        if (movX != 0)
        {
            facingDirection = (int)Mathf.Sign(movX);
        }

        // Gravity
        if (!bolGround())
        {
            if (!ignore_gravity)
            {
                velocity.y -= GRAVITY * Time.fixedDeltaTime;
            }
            if (has_max_vspeed)
            {
                if (velocity.y < -max_vspeed)
                {
                    velocity.y = -max_vspeed;
                }
            }

            if (coyote > 0)
            {
                coyote -= 60 * Time.fixedDeltaTime;
            }
        }
        else
        {
            reset_variables_on_ground();
        }

        // Moving ----------------------------------------------
        // LEFT
        if (intMove == -1 && velocity.x > -MAX_SPEED)
        {
            if (bolGround())
            {
                velocity.x += -WALK_SPEED * Time.fixedDeltaTime * 60.0f;
            }
            else
            {
                velocity.x += -AIR_WALK_SPEED * Time.fixedDeltaTime * 60.0f;
            }
            // Fast turn-around / skidding
			if(has_skidding_physics)
            {
				if(velocity.x > 0)
                {
					velocity.x += -WALK_SPEED * SKIDDING_MULTIPLIER * Time.fixedDeltaTime * 60.0f;
                }
            }
        }
        // RIGHT
        else if (intMove == 1 && velocity.x < MAX_SPEED)
        {
            if (bolGround())
            {
                velocity.x += WALK_SPEED * Time.fixedDeltaTime * 60.0f;
            }
            else
            {
                velocity.x += AIR_WALK_SPEED * Time.fixedDeltaTime * 60.0f;
            }
            // Fast turn-around / skidding
			if(has_skidding_physics)
            {
				if(velocity.x < 0)
                {
					velocity.x += WALK_SPEED * SKIDDING_MULTIPLIER * Time.fixedDeltaTime * 60.0f;
                }
            }
        }

        // STOP
        else if (bolGround() || (!bolGround() && intMove == 0 && has_air_friction))
        {
            velocity.x -= (WALK_SPEED * 0.5f) * Mathf.Sign(velocity.x) * Time.fixedDeltaTime * 60.0f;
        }

        // DEADZONE / reduce position bugs
        if(velocity.x < WALK_SPEED && velocity.x > -WALK_SPEED)
        {
            velocity.x = 0;
        }

        // HITTING YOUR HEAD
        if (bolCeiling() && velocity.y > 0)
        {
            velocity.y = 0;
        }
        
        // BUMP AGAINST WALLS
        if (bolLeft() && velocity.x < 0 && intMove == -1)
        {
            velocity.x = 0;
        }
        if (bolRight() && velocity.x > 0 && intMove == 1)
        {
            velocity.x = 0;
        }

        // ABILITIES ///////////////////////////////////////////
        // JUMPING -----------------------
        basic_jumping_behavior();

        // AIR/GROUND GRAVITY --------------
        air_gravity_behavior();

        // WALLJUMP -------------------------
        walljump_behavior();

    
        //Set back instantaneous inputs to false at the end of the FixedUpdate
        if(keyJumpDown) keyJumpDown = false;
        if(keyJumpUp) keyJumpUp = false;

        //APPLYING VELOCITY
        rbody.linearVelocity = new Vector2(velocity.x, velocity.y);
    }

    public void basic_jumping_behavior()
    {
        if(keyJump && bolGround() && !jumped)
            {
                jump(JUMP_SPEED);
            }
        
        if (keyJumpUp && velocity.y > 0)
            {
                velocity.y = velocity.y * 0.2f;
            }
    }

    public void air_gravity_behavior()
    {
        if (!bolGround() && keyJump && velocity.y > 0)
        {
            GRAVITY = AIR_GRAVITY;
        }
        else
        {
            GRAVITY = GROUND_GRAVITY;
        }
    }

    public void walljump_behavior()
    {
        if (has_walljump)
        {
            // If you already jumped, you're airborne and there's a wall
            // on either side
            if (!bolGround() && (bolLeft() || bolRight()))
            {
                if (keyJumpDown)
                {
                    int side = 1;
                    if (bolLeft())
                    {
                        side = -1;
                    }

                    jump(JUMP_SPEED * 0.7f);
                    velocity.x = -RUNSPD * 1.1f * side;
                }
            }
        }
    }


    public void reset_variables_on_ground()
        {
            velocity.y = 0;
            coyote = coyoteMAX;
            jumped = false;
        }
    
    public void jump(float force)
    {
        velocity.y = force;
        jumped = true;
    }

    public bool bolGround()
    {
        return colFloor.is_colliding();
    }

    public bool bolCeiling()
    {
        return colHead.is_colliding();
    }

    public bool bolLeft()
    {
        return colLeft.GetComponent<detects_collision>().is_colliding();
    }
    public bool bolRight()
    {
        return colRight.GetComponent<detects_collision>().is_colliding();
    }

    public void stopMovement()
    {
        reading = true;
        velocity.x = 0;
        velocity.y = 0;
        intMove = 0;
    }
    public void resumeMovement()
    {
        reading = false;
    }
}
