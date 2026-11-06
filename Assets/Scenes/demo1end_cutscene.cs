using UnityEngine;

public class demo1end_cutscene : Cutscene
{
    public AudioClip fanfare;

    public scr_demoend_MENU menu;
    public override void match_step()
    {
        // CUTSCENE CODE GOES HERE ----------------------------
        switch(step)
        {
            case 0:
            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 1:
            SoundManager.instance.PlaySound(fanfare);

            timer.set_wait_time(1.0f);
            timer.timer_start();
            break;

            case 2:
            send_message("There we go! That's your victory screen.", "default", "refwell");
            break;

            case 3:
            send_message("See y'all around.", "default", "refwell");
            break;

            case 4:
            anims.Play("an_demoend_enter");

            timer.set_wait_time(1.3f);
            timer.timer_start();
            break;

            case 5:
            menu.change_state("normal");
            
            timer.set_wait_time(0.2f);
            timer.timer_start();
            break;

            case 6:
            break;

            default:
            break;
        }
    }
}
