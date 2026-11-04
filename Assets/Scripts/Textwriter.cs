using System;
using UnityEngine;
using UnityEngine.Events;

public class Textwriter : MonoBehaviour
{
    public static Textwriter instance;

    // VARIABLES ---------------------------------------------------
    public string state = "closed"; // Valid states: "reading", "finished", "closed", "question"

    public string message_to_parse = "";
    public string current_message = "";
    public float current_position = 0f;
    float write_speed = 70f;

    // Visuals
    public CharDialogueData character_data;
    public string current_emotion;
    float voice_beep_cooldown = 0f;
    float voice_beep_cooldown_max = 0.08f;

    public CanvasGroup canvas_group;
    

    /* GML code pending for translation
    is_question = false;
    answer_amount = 3
    answers[0] = "A"
    answers[1] = "B"
    answers[2] = "C"
    chosen_answer = 0
    */

    float enter_cooldown = 0f;
    float enter_cooldown_max = 0.1f;

    public UnityEvent closed;

    // Singleton code---------------------------------
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
        canvas_group = GetComponent<CanvasGroup>();
        change_state("closed");
    }

    // Update is called once per frame
    void Update()
    {
        state_behaviors();

        // Cooldown thingy
        if (enter_cooldown > 0)
        {
            enter_cooldown -= Time.deltaTime;
        }

        if (voice_beep_cooldown > 0)
        {
            voice_beep_cooldown -= Time.deltaTime;
        }
    }


    // STATE MACHINE + STATES ----------------------------------------
    void change_state(string new_state = "closed")
    {
        state = new_state;
        state_enters();
    }

    void state_enters()
    {
        switch(state)
        {
            case "closed":
            state_closed_enter();
            break;

            case "reading":
            state_reading_enter();
            break;

            case "finished":
            state_finished_enter();
            break;

            default:
            break;
        }

        GetComponentInChildren<Textwriter_DRAW>().state_enters();
    }
    void state_behaviors()
    {
        switch(state)
        {
            case "closed":
            state_closed_behavior();
            break;

            case "reading":
            state_reading_behavior();
            break;

            case "finished":
            state_finished_behavior();
            break;

            default:
            break;
        }
    }
    void state_closed_enter()
    {
        //is_question = false;
        closed.Invoke();
    }
    void state_closed_behavior()
    {
        
    }

    void state_reading_enter()
    {
        current_position = 0f;
        current_message = "";

    }
    void state_reading_behavior()
    {
        // Message parsing
        if (current_position < (float)message_to_parse.Length)
        {
            current_position += Time.deltaTime * write_speed;

            current_message = message_to_parse;
            // OLD TYPEWRITER METHOD --------------------
            //current_message = message_to_parse.Substring(0, (int)current_position);

            play_voice_beep();
        }
        else
        {
            change_state("finished");
        }
        
        // Skipping
        if (Input.GetButtonDown("Submit"))
        {
            current_position = (float)message_to_parse.Length;
            current_message = message_to_parse;
            change_state("finished");
            return;
        }
    }

    void state_finished_enter()
    {
        enter_cooldown = enter_cooldown_max;
    }
    void state_finished_behavior()
    {
        if (Input.GetButtonDown("Submit") && enter_cooldown <= 0)
        {
            change_state("closed");
        }
    }

    public void show_message(string message_to_show = "", string emotion_to_show = "default", string character_to_show = "default")
    {
        message_to_parse = message_to_show;
        current_emotion = emotion_to_show;
        change_character(character_to_show);
        change_state("reading");
    }

    public void play_voice_beep()
    {
        if (voice_beep_cooldown <= 0)
        {
            SoundManager.instance.PlaySound(character_data.voice_beep);
            voice_beep_cooldown = voice_beep_cooldown_max;
        }
    }

    public void change_character(string character_to_change_to)
    {
        // Load character data from lookup table, searching by string
        character_data = CharacterDialogueTable.instance.get_character(character_to_change_to);
    }
}
