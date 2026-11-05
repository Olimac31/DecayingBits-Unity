using UnityEngine;

public class demo1end_cutscene : Cutscene
{
    public AudioClip fanfare;
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
            break;

            default:
            break;
        }
    }
}
