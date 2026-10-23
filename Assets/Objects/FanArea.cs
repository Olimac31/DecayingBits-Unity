using UnityEngine;

public class FanArea : detects_player
{
    public bool activated = true;
    GameObject the_player;

    float air_force = 190.0f;
    float air_force_max = 25f;

    void Start()
    {
        the_player = GameObject.Find("obj_Player");
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(the_player != null)
        {
            playerMovement temp_player = the_player.GetComponent<playerMovement>();
            if (colliding && temp_player.velocity.y < air_force_max)
            {
                temp_player.velocity.y += air_force * Time.deltaTime;
                temp_player.can_cancel_jump = false;
            }
        }
    }
}
