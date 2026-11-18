using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class scr_OptionsMenu : MonoBehaviour
{
    const int STATE_WAITING = 0, STATE_NORMAL = 1, STATE_INTERACTING = 2;
    int state = STATE_WAITING;

    int option_id = 0;
    int option_count = 5;

    public GameObject option_list;

    public AudioClip SFX_Select, SFX_Confirm, SFX_Cancel;

    public UnityEvent options_exited;

    public GameObject canvas;

    float cooldown = 0.0f;
    float cooldown_MAX = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        change_state(STATE_WAITING);
        change_option(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0.0f)
        {
            state_behaviors();
        }

        if(cooldown > 0.0f)
        {
            cooldown -= Time.unscaledDeltaTime;
        }
    }

    void change_state(int new_state = STATE_WAITING)
    {
        state = new_state;
        
        switch(state)
        {
            case STATE_WAITING:
            set_visibility(0.0f);
            break;

            case STATE_NORMAL:
            set_visibility(1.0f);
            change_option(0);
            break;

            case STATE_INTERACTING:
            break;
        }
    }

    void state_behaviors()
    {
        switch(state)
        {
            case STATE_WAITING:
            break;

            case STATE_NORMAL:
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
            }
            break;

            case STATE_INTERACTING:
            // CONTROLS ----------------------------------
            switch(option_id)
                {
                    // master volume, music volume and sfx volume
                    case 0 or 1 or 2:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            change_volume(-0.1f);
                        }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            change_volume(0.1f);
                        }
                    break;
                }
            
            // BACK ----------------------------------
            if(Input.GetButtonDown("Cancel"))
                {
                    change_state(STATE_NORMAL);
                    SoundManager.instance.PlaySound(SFX_Cancel);
                }
            break;
        }
    }

    public void change_option(int amount = 0, bool play_the_sfx = true)
    {
        int temp_option = option_id + amount;

        // Lower limit
        if (temp_option < 0)
        {
            temp_option = option_count - 1;
        }

        // Upper limit
        if (temp_option > option_count - 1)
        {
            temp_option = 0;
        }

        // SFX
        if(amount != 0 && play_the_sfx)
        {
            SoundManager.instance.PlaySound(SFX_Select);
        }

        option_id = temp_option;

        // VISUALS
        int i = 0;
        foreach(TextMeshProUGUI txt in option_list.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (i == option_id)
            {
                txt.alpha = 1.0f;
            }
            else
            {
                txt.alpha = 0.2f;
            }

            i++;
        }
    }

    void choose_an_option(bool play_the_sfx = true)
    {
        switch(option_id)
        {
            // Master Volume
            case 0:
            change_state(STATE_INTERACTING);
            break;

            // Music
            case 1:
            change_state(STATE_INTERACTING);
            break;
            
            // SFX
            case 2:
            change_state(STATE_INTERACTING);
            break;
            
            // Flashing Lights
            case 3:
            bool new_value = !GameManager.instance.get_bool_setting("flashing_lights");
            GameManager.instance.set_bool_setting("flashing_lights", new_value);
            break;

            // Quit
            case 4:
            change_state(STATE_WAITING);
            options_exited.Invoke();
            break;
        }

        if(play_the_sfx)
        {
            SoundManager.instance.PlaySound(SFX_Confirm);
        }
    }

    void set_visibility(float value = 1.0f)
    {
        canvas.GetComponent<CanvasGroup>().alpha = value;
    }

    public void OPEN_MENU()
    {
        cooldown = cooldown_MAX;
        change_state(STATE_NORMAL);
        change_option(0);
    }

    void change_volume(float amount = 0.1f)
    {
        float new_value = amount;

        switch(option_id)
            {
                // VOLUME ---------------------------
                
                // Master
                case 0:
                new_value = GameManager.instance.get_setting("master_vol") + amount;
                new_value = Mathf.Clamp(new_value, 0.0f, 1.0f);

                GameManager.instance.set_setting("master_vol", new_value);
                break;

                // Music
                case 1:
                new_value = GameManager.instance.get_setting("music_vol") + amount;
                new_value = Mathf.Clamp(new_value, 0.0f, 1.0f);
                
                GameManager.instance.set_setting("music_vol", new_value);
                break;

                // SFX
                case 2:
                new_value = GameManager.instance.get_setting("sfx_vol") + amount;
                new_value = Mathf.Clamp(new_value, 0.0f, 1.0f);
                
                GameManager.instance.set_setting("sfx_vol", new_value);
                break;

                default:
                break;
            }

        SoundManager.instance.PlaySound(SFX_Confirm);
    }
}
