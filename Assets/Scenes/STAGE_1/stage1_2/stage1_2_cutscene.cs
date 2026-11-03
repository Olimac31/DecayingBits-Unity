using UnityEngine;

public class stage1_2_cutscene : Cutscene
{
    public playerMovement the_player;
    public cameraController cam;

    public AudioClip SFX_BLOCK_FALL, SFX_JUMP;

    public Transform camtarget;

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

            cam.target = camtarget;

            HUD.instance.HUD_EXIT();

            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 2:
            send_message("That door is too high to reach, it's a dead end.");
            break;

            case 3:
            SoundManager.instance.StopMusic();
            timer.set_wait_time(2.0f);
            timer.timer_start();
            break;

            case 4:
            anims.Play("an_s1_2_fall");
            break;

            case 5:
            GameManager.instance.change_room("Assets/Scenes/STAGE_1/stage1_3.unity");
            break;

            case 6:
            break;

            case 99: // PLACEHOLDER
            the_player.reading = false;
            HUD.instance.HUD_ENTER();

            signal_cutscene_end();
            break;

            default:
            break;
        }
    }

    public void make_actor_visible()
    {
        the_player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void PLAY_BLOCK_FALL_SFX()
    {
        SoundManager.instance.PlaySound(SFX_BLOCK_FALL);
    }
    public void PLAY_JUMP_SFX()
    {
        SoundManager.instance.PlaySound(SFX_JUMP);
    }
}
