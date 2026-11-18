using UnityEngine;

public class stage1_0_cutscene : Cutscene
{
    public playerMovement the_player;
    public Animator playeractor;

    public cameraController cam;

    public override void match_step()
    {
        // CUTSCENE CODE GOES HERE ----------------------------
        switch(step)
        {
            case 0:
            if(GameManager.instance.intro_cutscene_seen)
            {
                playeractor.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                cam.target = the_player.transform;
                cam.camLimitUp = 9f;
                HUD.instance.HUD_ENTER();

                GameManager.instance.player_broken_jump = true;
                the_player.setJumpSpeed(GameManager.instance.get_broken_jump());

                jump_to_step(10);
                return;
            }
            else
            {
                the_player.reading = true;
                the_player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                anims.Play("s1_0_phase1");

                GameManager.instance.player_broken_jump = true;
                the_player.setJumpSpeed(GameManager.instance.get_broken_jump());

                playeractor.speed = 0.1f;
            }
            break;

            case 1:
            Textwriter.instance.show_message("As you wake up, you realize you don't recognize your surroundings.");
            break;

            case 2:
            Textwriter.instance.show_message("Your legs feel weak, your vision is still a bit blurry, and you have no idea how you ended up here.");
            break;

            case 3:
            Textwriter.instance.show_message("Maybe you should get up and try to find a way back home.");
            break;

            case 4:
            timer.set_wait_time(1f);
            timer.timer_start();
            break;

            case 5:
            playeractor.Play("s1_0_ber_stand");

            timer.set_wait_time(2.9f);
            timer.timer_start();
            break;

            case 6:
            the_player.transform.position = playeractor.transform.position;
            the_player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            playeractor.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            cam.target = the_player.transform;
            cam.camLimitUp = 9f;

            timer.set_wait_time(1f);
            timer.timer_start();
            break;

            case 7:
            Textwriter.instance.show_message("(You feel as if your JUMP HEIGHT has been reduced to 25%).");
            break;

            case 8:
            the_player.reading = false;
            HUD.instance.HUD_ENTER();

            anims.Play("s1_0_phase2"); // tutorial buttons
            break;

            case 9:
            GameManager.instance.intro_cutscene_seen = true;
            anims.Play("s1_0_phase3"); // tutorial buttons loop
            
            signal_cutscene_end();
            break;

            // Skip cutscene
            case 10:
            anims.Play("s1_0_SKIP");
            
            signal_cutscene_end();
            break;


            default:
            break;
        }
    }
}
