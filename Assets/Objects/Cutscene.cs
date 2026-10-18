using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour
{
    public Animator anims;
    public int step = -1;

    public GodotTimer timer;

    string last_character = "default";
    string last_emotion = "default";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (timer != null)
        {
            // Connect to Timer's timeout
            timer.timeout.AddListener(updateStep);
            timer.set_wait_time(0.01f);
            timer.timer_start();

            // Connect to Texwriter's close event
            Textwriter.instance.closed.AddListener(updateStep);
        }
    }

    public virtual void updateStep()
    {
        step += 1;
        match_step();
    }

    public virtual void match_step()
    {
        // CUTSCENE CODE GOES HERE ----------------------------
        switch(step)
        {
            case 0:
            break;

            default:
            break;
        }
    }

    public virtual void jump_to_step(int value, bool wait_for_timer = false)
    {
        step = value - 1;

        if(!wait_for_timer)
        {
            updateStep();
        }
    }

    public virtual void send_message(string message_to_send = "", string emotion_to_send = "default", string character_to_send = "default")
    {
        Textwriter.instance.show_message(message_to_send, emotion_to_send, character_to_send);
    }
}
