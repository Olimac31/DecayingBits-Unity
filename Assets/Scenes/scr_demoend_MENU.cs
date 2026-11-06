using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_demoend_MENU : MonoBehaviour
{
    protected const string STATE_WAITING = "waiting", STATE_NORMAL = "normal";
    string state = STATE_WAITING;
    bool exited = false;

    public AudioClip SFX_CONFIRM;

    protected virtual void Start()
    {
        
    }

    void Update()
    {
        states_behavior();
    }

    void states_enter()
    {
        switch(state)
        {
            case STATE_NORMAL:
            break;

            case STATE_WAITING:
            break;

            default:
            break;
        }
    }

    void states_behavior()
    {
        switch(state)
        {
            case STATE_NORMAL:
                if(Input.GetButtonDown("Submit") && !exited)
                {
                    SoundManager.instance.PlaySound(SFX_CONFIRM);
                    GameManager.instance.change_room("Assets/Scenes/MainMenu.unity", 1.0f);
                    exited = true;
                }
            break;

            case STATE_WAITING:
            break;

            default:
            break;
        }
    }

    public void change_state(string new_state = "")
    {
        state = new_state;

        switch(state)
        {
            case STATE_NORMAL:
            break;

            case STATE_WAITING:
            break;

            default:
            break;
        }
    }
}
