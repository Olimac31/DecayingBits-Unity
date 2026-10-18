using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GodotTimer : MonoBehaviour
{
    const string TIMER_OFF = "off";
    const string TIMER_ON = "on";

    string timer_state = TIMER_OFF;

    public float wait_time = 1f; // Current time
    float time_left = -1.0f;
    public bool oneshot = true; // Whether timer loops or not
    public bool autostart = false;

    public UnityEvent timeout;


    // CLASS FUNCTIONS ------------------------------------------------
    public void set_wait_time(float new_wait_time = 1f)
    {
        if (new_wait_time <= 0f)
        {
            Debug.LogWarning("GodotTimer: Wait time must be greater than zero.", this);
            return;
        }
        
        wait_time = new_wait_time;
    }
    public void timer_start()
    {
        change_state(TIMER_ON);
    }

    public void timer_stop()
    {
        time_left = -1.0f;
        change_state(TIMER_OFF);
        autostart = false;
    }

    public float get_time_left()
    {
        return time_left;
    }

    // MONOBEHAVIOUR -------------------------------------------------------
    void Start()
    {
        if (autostart)
        {
            change_state(TIMER_ON);
        }
    }
    void Update()
    {
        state_behavior();
    }

    // STATE MACHINE --------------------------------------------------

    void change_state(string new_state = TIMER_OFF)
    {
        timer_state = new_state;
        state_enters();
    }

    void state_enters()
    {
        switch(timer_state)
        {
            case TIMER_OFF:
            break;

            case TIMER_ON:
            time_left = wait_time;
            break;

            default:
            break;
        }
    }

    void state_behavior()
    {
        switch(timer_state)
        {
            case TIMER_OFF:
            break;
            
            case TIMER_ON:
            time_left -= Time.deltaTime;

			if (time_left < 0)
            {
				if (!oneshot)
                {
					time_left += wait_time;
				}
                else
                {
					timer_stop();
				}
				timeout.Invoke();
                Debug.Log("GODOTTIMER: timeout!");
            }
            break;

            default:
            break;
        }
    }
    
}
