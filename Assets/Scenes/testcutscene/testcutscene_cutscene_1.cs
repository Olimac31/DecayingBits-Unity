using UnityEngine;

public class testcutscene_cutscene_1 : Cutscene
{
    public playerMovement the_player;

    public AudioClip pxmas_music;

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
            
            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 2:
            send_message("This is a test cutscene, sending a test message to the TEXTWRITER!");
            break;

            case 3:
            send_message("awesome text being sent rn (epic)");
            break;

            case 4:
            anims.Play("enter");
            break;

            case 5:
            send_message("SULPI FROM SULPIQUEST LOOK!", "default", "pxmas");
            SoundManager.instance.PlayMusic(pxmas_music);
            break;

            case 6:
            send_message("We are in Unity now", "gaming", "pxmas");
            break;

            case 7:
            send_message("Purki? What happened to your spritesheet?");
            break;

            case 8:
            send_message("the programmer is too lazy to bring it over", "default", "pxmas");
            break;

            case 9:
            send_message("but it's okay, being a square is awesome, you should try that too!", "gaming", "pxmas");
            break;

            case 10:
            send_message("I think I like not being a square");
            break;

            case 11:
            send_message("your loss", "gaming", "pxmas");
            break;

            case 12:
            send_message("ok i will                  go now", "default", "pxmas");
            break;

            case 13:
            anims.Play("he_leaves");
            break;

            case 14:
            the_player.reading = false;
            SoundManager.instance.StopMusic();
            HUD.instance.HUD_ENTER();

            signal_cutscene_end();
            break;

            default:
            break;
        }
    }
}
