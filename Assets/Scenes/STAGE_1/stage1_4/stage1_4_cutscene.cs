using UnityEngine;

public class stage1_4_cutscene : Cutscene
{
    public playerMovement the_player;
    public GameObject refwell;
    public GameObject actor_bernard;
    public cameraController cam;

    public AudioClip SFX_RATLAUGH, SFX_JUMP, REFWELL_SONG, SFX_ACHIEVEMENT;

    public Transform camtarget;

    public GameObject cutsceneTrigger;

    public override void match_step()
    {
        // CUTSCENE CODE GOES HERE ----------------------------
        switch(step)
        {
            case 0:
            actor_bernard.GetComponent<SpriteRenderer>().enabled = false;

            if(GameManager.instance.refwell_cutscene_seen)
                {
                    jump_to_step(35);
                    Destroy(cutsceneTrigger);
                }
            break;

            case 1:
            the_player.reading = true;
            the_player.velocity.x = 0f;

            cam.target = camtarget; // Change camera to cutscene camera

            HUD.instance.HUD_EXIT();

            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 2:
            anims.Play("an_s1_4_refwell_enters");

            actor_bernard.transform.position = the_player.transform.position;

            the_player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            actor_bernard.GetComponent<SpriteRenderer>().enabled = true;

            animate_bernard("an_bernard_idle");
            break;

            case 3:
            animate_refwell("an_refwell_laugh");
            SoundManager.instance.PlaySound(SFX_RATLAUGH);
            SoundManager.instance.PlayMusic(REFWELL_SONG);
            
            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 4:
            animate_refwell("an_refwell_idle");
            send_message("Well well, hello there!", "default", "refwell");
            break;

            case 5:
            send_message("My name's Refwell! I'm the GUARDIAN of this little place you've been wandering around in for a bit.", "default", "refwell");
            break;

            case 6:
            send_message("You... don't seem like you're from around here, are you?", "default", "refwell");
            break;

            case 7:
            send_message("In that case... WELCOME to <color=red>Zone -1!</color>", "default", "refwell");
            break;

            case 8:
            timer.set_wait_time(1.5f);
            timer.timer_start();
            break;

            case 9:
            send_message("Say... you seem a bit unequipped.", "default", "refwell");
            break;

            case 10:
            send_message("Is your name 'BERNARD' by any chance?", "default", "refwell");
            break;

            case 11:
            anims.Play("an_s1_4_refwell_show_script");
            break;

            case 12:
            send_message("Well it's your lucky day, because I happen to have found a little something that came from where YOU came from!", "default", "refwell");
            break;

            case 13:
            send_message("It's got your name on it! Literally.", "default", "refwell");
            break;

            case 14:
            send_message("I just... borrowed it for a little while...", "default", "refwell");
            break;

            case 15:
            anims.Play("an_s1_4_refwell_hide_script");
            break;

            case 16:
            send_message("Not so fast, pal! See, my duty is to collect garbage that falls into Zone -1...", "default", "refwell");
            break;

            case 17:
            send_message("But it's so, SO boring most of the time...", "mad", "refwell");
            break;

            case 18:
            send_message("Except when I find GOLD like this one! One man's trash is another man's treasure!", "default", "refwell");
            break;

            case 19:
            send_message("The ultra-specific conditions, the intricate physics, all the tiny details!", "default", "refwell");
            break;

            case 20:
            animate_refwell("an_refwell_handshake");
            send_message("Did you know your walljump gives you a horizontal boost LARGER than your maximum ground speed? You can go above your speed cap!", "default", "refwell");
            break;

            case 21:
            animate_refwell("an_refwell_laugh");
            SoundManager.instance.PlaySound(SFX_RATLAUGH);
            send_message("I can't believe I get to meet the original owner of this piece of code, this is the most fun I've had in... FOREVER!", "default", "refwell");
            break;

            case 22:
            animate_refwell("an_refwell_idle");
            send_message("Mostly because the original owners tend to be...", "default", "refwell");
            break;

            case 23:
            refwell.GetComponent<SpriteRenderer>().flipX = true;
            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 24:
            refwell.GetComponent<SpriteRenderer>().flipX = false;
            send_message("...dead.", "mad", "refwell");
            break;

            case 25:
            send_message("But YOU are alive and well, Bernard! So I'm not gonna let the ocassion go to waste!", "default", "refwell");
            break;

            case 26:
            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 27:
            send_message("Here's the deal. I'll return HALF your jumping script now, but I'll hold on to the part that lets you walljump for a lil' longer.", "default", "refwell");
            break;

            case 28:
            send_message("If you can catch me at the top of this area, it's all yours to keep!", "default", "refwell");
            break;

            case 29:
            send_message("See you at the top. Good luck, hopping man!", "default", "refwell");
            break;

            case 30:
            anims.Play("an_s1_4_refwell_leaves");
            break;

            case 31:
            SoundManager.instance.StopMusic();
            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 32:
            SoundManager.instance.PlaySound(SFX_ACHIEVEMENT);
            send_message("JUMP HEIGHT fully restored!", "default", "default");
            break;

            case 33:
            Transition.instance.trans_in();

            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 34:
            Transition.instance.trans_out();
            // Hide actor, give control back to the player, also camera targets it again
            the_player.transform.position = actor_bernard.transform.position;
            the_player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            actor_bernard.GetComponent<SpriteRenderer>().enabled = false;
            cam.target = the_player.transform;

            // Cutscene seen
            GameManager.instance.refwell_cutscene_seen = true;
            
            // FINALLY restore his regular jump LOL
            GameManager.instance.player_broken_jump = false;
            the_player.setJumpSpeed(GameManager.instance.get_regular_jump());
            
            the_player.reading = false;

            HUD.instance.HUD_ENTER();

            timer.set_wait_time(0.1f);
            timer.timer_start();

            signal_cutscene_end();
            break;

            case 35:
            break;

            case 99: // UNUSED
            animate_refwell("an_refwell_laugh");
            SoundManager.instance.PlaySound(SFX_RATLAUGH);

            the_player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            actor_bernard.GetComponent<SpriteRenderer>().enabled = true;

            animate_bernard("an_bernard_idle");
            actor_bernard.GetComponent<SpriteRenderer>().flipX = true;

            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 100: // UNUSED
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

    public void PLAY_THE_JUMP_SFX()
    {
        SoundManager.instance.PlaySound(SFX_JUMP);
    }
}
