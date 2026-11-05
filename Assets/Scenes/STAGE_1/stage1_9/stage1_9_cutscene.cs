using UnityEditor.UI;
using UnityEngine;

public class stage1_9_cutscene : Cutscene
{
    public playerMovement the_player;
    public GameObject actor_refwell;
    public GameObject actor_bernard;
    public cameraController cam;

    public Transform camtarget;

    public AudioClip REFWELL_SONG;

    public override void match_step()
    {
        // CUTSCENE CODE GOES HERE ----------------------------
        switch(step)
        {
            case 0:
            break;

            case 1:
            the_player.reading = true;
            the_player.velocity.x = 0f;
            the_player.transform.position = new Vector3(actor_bernard.transform.position.x, the_player.transform.position.y, the_player.transform.position.z);

            HUD.instance.HUD_EXIT();

            cam.target = camtarget;

            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 2:
            send_message("Traveller, wait a second!", "default", "refwell");
            break;

            case 3:
            the_player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            actor_bernard.GetComponent<SpriteRenderer>().enabled = true;

            anims.Play("an_s1_9_walk_in");
            break;

            case 4:
            SoundManager.instance.PlayMusic(REFWELL_SONG);
            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 5:
            send_message("If I may break the fourth wall for a second...", "default", "refwell");
            break;

            case 6:
            send_message("So. The stage.", "default", "refwell");
            break;

            case 7:
            send_message("Yep! Far from finished, pal.", "mad", "refwell");
            break;

            case 8:
            send_message("There were WAAAY bigger plans for this demo, but lots of content had to be cut due to time constraints.", "mad", "refwell");
            break;

            case 9:
            send_message("But don't worry! Chances are a new demo will release sometime around mid 2027! Hopefully...", "default", "refwell");
            break;

            case 10:
            send_message("Oh, how do I know this? I don't! This cutscene isn't canon.", "default", "refwell");
            break;

            case 11:
            send_message("And hey, to whoever got to playtest this alpha build, hope you had fun even if it was really short!", "default", "refwell");
            break;

            case 12:
            Transition.instance.trans_in();

            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 13:
            send_message("WAIT. I almost forgot.", "default", "refwell");
            break;

            case 14:
            send_message("According to the requirements of Olimac's midterm, there has to be some sort of 'victory screen'...", "default", "refwell");
            break;

            case 15:
            SoundManager.instance.StopMusic();
            send_message("Yeah, just lemme...", "default", "refwell");
            break;

            case 16:
            GameManager.instance.change_room("Assets/Scenes/demo1end.unity");
            break;

            default:
            break;
        }
    }

    public void animate_refwell(string anim = "an_refwell_idle")
    {
        change_actor(actor_refwell);
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
