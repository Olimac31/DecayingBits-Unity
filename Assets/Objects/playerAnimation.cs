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
        }

        // JUMPING and FALLING ------------------------------
        if (player.bolGround() == false)
        {
            anims.SetBool("is_airborne", true);

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
    }
}
