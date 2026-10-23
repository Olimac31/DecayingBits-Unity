using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    playerMovement player;
    Animator anims;
    SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<playerMovement>();
        anims = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anims.speed = 0.2f;

        // FLIPPING ANIMATION
        if (player.intMove < 0) sprite.flipX = true;
        if (player.intMove > 0) sprite.flipX = false;

        // MOVING --------------------------------------------
        if (player.velocity.x > 0.4f || player.velocity.x < -0.4f)
        {
            anims.SetBool("is_walking", true);
        }
        else
        {
            anims.SetBool("is_walking", false);
            anims.speed = 0.1f;
        }

        // JUMPING and FALLING ------------------------------
        if (player.bolGround() == false)
        {
            anims.SetBool("is_airborne", true);
            anims.speed = 0.2f;

            if (player.velocity.y < 0.1f)
            {
                anims.SetBool("is_falling", true);
            }
            else
            {
                anims.SetBool("is_falling", false);
            }
        }
        else
        {
            anims.SetBool("is_airborne", false);
            anims.SetBool("is_falling", false);
        }

        if (player.invincible > 0.0f)
        {
            if (player.invincible > 2.5f)
            {
                Color tmp = sprite.color;
                tmp.r = 1;
                tmp.g = 0;
                tmp.b = 0;
                tmp.a = 1.0f;
                sprite.color = tmp;

                anims.Play("player_hurt");
            }
            if (player.invincible < 2.5f && player.invincible > 2.4f)
            {
                anims.Play("player_walk");
            }
            else if(player.invincible > 0.2f)
            {
                Color tmp = sprite.color;
                tmp.r = 1;
                tmp.g = 1;
                tmp.b = 1;
                tmp.a = 0.5f;

                sprite.color = tmp;
            }
            else
            {
                Color tmp = sprite.color;
                tmp.r = 1;
                tmp.g = 1;
                tmp.b = 1;
                tmp.a = 1.0f;
                sprite.color = tmp;
            }
        }
    }
}
