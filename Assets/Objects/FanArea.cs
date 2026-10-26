using Unity.VisualScripting;
using UnityEngine;

public class FanArea : detects_player
{
    public bool activated = true;
    GameObject the_player;

    float air_force = 190.0f;
    public float air_force_max_v = 25f;
    public float air_force_max_h = 25f;

    public float h_multiplier = 2.0f;
    public float v_multiplier = 1.0f;

    public enum force_direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT
    }

    public force_direction air_force_dir = force_direction.UP;


    void Start()
    {
        the_player = GameObject.Find("obj_Player");
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(the_player != null && colliding)
        {
            // Access player's movement script
            playerMovement temp_player = the_player.GetComponent<playerMovement>();

            // Apply force based on direction
            // VERTICAL -----------------------------------------------------------------
            switch(air_force_dir)
            {
                // UP
                case force_direction.UP or force_direction.UPLEFT or force_direction.UPRIGHT:
                if (temp_player.velocity.y < air_force_max_v)
                    {
                        temp_player.velocity.y += air_force * v_multiplier * Time.deltaTime;
                    }
                break;

                // DOWN
                case force_direction.DOWN or force_direction.DOWNLEFT or force_direction.DOWNRIGHT:
                if (temp_player.velocity.y > -air_force_max_v)
                    {
                        temp_player.velocity.y -= air_force * v_multiplier * Time.deltaTime;
                    }
                break;

                default:
                break;
            }
            // HORIZONTAL -----------------------------------------------------------------
            switch(air_force_dir)
            {
                // LEFT
                case force_direction.LEFT or force_direction.UPLEFT or force_direction.DOWNLEFT:
                if (temp_player.velocity.x > -air_force_max_h)
                    {
                        temp_player.velocity.x -= air_force * h_multiplier * Time.deltaTime;
                    }
                break;

                // RIGHT
                case force_direction.RIGHT or force_direction.UPRIGHT or force_direction.DOWNRIGHT:
                if (temp_player.velocity.x < air_force_max_h)
                    {
                        temp_player.velocity.x += air_force * h_multiplier * Time.deltaTime;
                    }
                break;

                default:
                break;
            }

            temp_player.can_cancel_jump = false;
        }
    }
}
