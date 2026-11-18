using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class scr_PAUSE_MENU : MonoBehaviour
{
    public static scr_PAUSE_MENU instance;

    public bool CAN_PAUSE = true;
    public bool PAUSED = false;

    const int STATE_PAUSED = 0, STATE_NORMAL = 1, STATE_IN_OPTIONS = 2, STATE_QUITTING = 3;
    int state = STATE_NORMAL;

    GameObject option_list;
    public AudioClip SFX_Select, SFX_Confirm;
    int option_count = 2;
    int option_id = 0;

    Animator anims;

    public GodotTimer exit_timer;

    //Singleton code---------------------------------
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anims = GetComponent<Animator>();
        anims.Play("pause_hide");

        option_list = GameObject.Find("pause_options");

        exit_timer.timeout.AddListener(on_exit_timer_timeout);
    }

    // Update is called once per frame
    void Update()
    {
        state_behavior();
    }

    public void change_state(int new_state = STATE_NORMAL)
    {
        state = new_state;

        switch(state)
        {
            case STATE_NORMAL:
            option_id = 0;
            PAUSED = false;

            Time.timeScale = 1.0f;
            break;

            case STATE_PAUSED:
            anims.Play("pause_enter");
            PAUSED = true;

            Time.timeScale = 0.0f;
            break;

            case STATE_QUITTING:
            anims.Play("pause_exit");
            HUD.instance.HUD_EXIT();
            exit_timer.timer_start();

            GameManager.instance.change_room("Assets/Scenes/MainMenu.unity");
            break;

            case STATE_IN_OPTIONS:
            break;

            default:
            break;
        }
    }

    public void state_behavior()
    {
        switch (state)
        {
            case STATE_NORMAL:
            behavior_normal();
            break;

            case STATE_PAUSED:
            behavior_paused();
            break;

            case STATE_QUITTING:
            behavior_quitting();
            break;

            case STATE_IN_OPTIONS:
            behavior_in_options();
            break;

            default:
            break;
        }
    }

    void behavior_normal()
    {
        
    }

    void behavior_paused()
    {
        // NAVIGATE OPTIONS
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            change_option(-1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            change_option(1);
        }

        if (Input.GetButtonDown("Submit"))
        {
            choose_an_option();
            SoundManager.instance.PlaySound(SFX_Confirm);
        }

        // VISUALS
        int i = 0;
        foreach(TextMeshProUGUI txt in option_list.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (i != option_id)
            {
                txt.alpha = 0.2f;
            }
            else
            {
                txt.alpha = 1.0f;
            }

            i++;
        }

        // COUNT
        option_count = i;
    }

    void behavior_in_options()
    {
        
    }

    void behavior_quitting()
    {
        
    }

    public void change_option(int amount, bool play_the_sfx = true)
    {
        int temp_option = option_id + amount;

        // Lower limit
        if (temp_option < 0)
        {
            temp_option = 0;
            play_the_sfx = false;
        }

        // Upper limit
        if (temp_option > option_count - 1)
        {
            temp_option = option_count - 1;
            play_the_sfx = false;
        }

        // SFX
        if(amount != 0 && play_the_sfx)
        {
            SoundManager.instance.PlaySound(SFX_Select);
        }

        option_id = temp_option;
    }

    public void choose_an_option()
    {
        switch(option_id)
        {
            // Continue
            case 0:
            change_state(STATE_NORMAL);
            anims.Play("pause_exit");
            break;

            // Options
            case 1:
            // PLACEHOLDER
            change_state(STATE_NORMAL);
            anims.Play("pause_exit");
            break;

            // Exit
            case 2:
            change_state(STATE_QUITTING);
            break;

            default:
            break;
        }
    }

    public void request_pause()
    {
        if(state == STATE_NORMAL && CAN_PAUSE)
        {
            change_state(STATE_PAUSED);
            SoundManager.instance.PlaySound(SFX_Confirm);
        }
    }

    void on_exit_timer_timeout()
    {
        change_state(STATE_NORMAL);
    }
}
