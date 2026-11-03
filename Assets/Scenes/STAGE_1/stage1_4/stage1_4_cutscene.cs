using UnityEngine;

public class stage1_4_cutscene : Cutscene
{
    public playerMovement the_player;
    public GameObject refwell;
    public GameObject actor_bernard;
    public cameraController cam;

    public AudioClip SFX_RATLAUGH, SFX_JUMP;

    public Transform camtarget;

    public override void match_step()
    {
        // CUTSCENE CODE GOES HERE ----------------------------
        switch(step)
        {
            case 0:
            actor_bernard.GetComponent<SpriteRenderer>().enabled = false;
            break;

            case 1:
            the_player.reading = true;
            the_player.velocity.x = 0f;

            HUD.instance.HUD_EXIT();

            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 2:
            animate_refwell("an_refwell_laugh");
            SoundManager.instance.PlaySound(SFX_RATLAUGH);

            the_player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            actor_bernard.GetComponent<SpriteRenderer>().enabled = true;

            animate_bernard("an_bernard_idle");
            actor_bernard.GetComponent<SpriteRenderer>().flipX = true;

            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 3:
            animate_refwell("an_refwell_idle");
            send_message("We are in a cutscene now lol", "default", "refwell");
            break;

            default:
            break;
        }
    }

    public void animate_refwell(string anim = "an_refwell_idle")
    {
        change_actor(refwell);
        change_actor_anim(anim);
        play_animation_on_chosen_actor();
    }

    public void animate_bernard(string anim = "an_bernard_idle")
    {
        change_actor(actor_bernard);
        change_actor_anim(anim);
        play_animation_on_chosen_actor();
    }
}
