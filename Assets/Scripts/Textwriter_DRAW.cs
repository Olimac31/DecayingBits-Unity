using UnityEngine;
using UnityEngine.UI;

public class Textwriter_DRAW : MonoBehaviour
{
    public GameObject textbox;
    public GameObject text;
    public GameObject charname_text;
    public GameObject portrait;

    int frame_to_use = 0;
    float alt_frame_timer = 0f;
    float alt_frame_timer_MAX = 0.1f;

    TMPro.TextMeshProUGUI text_to_write_to;
    TMPro.TextMeshProUGUI charname_text_to_change;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_to_write_to = text.GetComponent<TMPro.TextMeshProUGUI>();
        charname_text_to_change = charname_text.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        state_behaviors();
    }

    public void state_enters()
    {
        switch(Textwriter.instance.state)
        {
            case "closed":
            text_to_write_to.text = "";
            charname_text_to_change.text = "";
            Textwriter.instance.canvas_group.alpha = 0;
            break;

            case "reading":
            // Reset text written and load character name
            charname_text_to_change.text = Textwriter.instance.character_data.charname;

            CharDialogueData temp_character = Textwriter.instance.character_data;
            portrait.GetComponent<Image>().sprite = temp_character.get_emotion(Textwriter.instance.current_emotion, 0);

            frame_to_use = 0;
            alt_frame_timer = alt_frame_timer_MAX;

            Textwriter.instance.canvas_group.alpha = 1;
            break;

            case "finished":
            portrait.GetComponent<Image>().sprite = Textwriter.instance.character_data.get_emotion(Textwriter.instance.current_emotion, 0);
            text_to_write_to.text = Textwriter.instance.message_to_parse;
            break;

            default:
            break;
        }
    }
    void state_behaviors()
    {
        switch(Textwriter.instance.state)
        {
            case "closed":
            break;

            case "reading":
            text_to_write_to.text = Textwriter.instance.current_message;

            // ALT FRAME
            if(alt_frame_timer > 0)
            {
                alt_frame_timer -= Time.deltaTime;
            }
            else
            {
                if(frame_to_use == 0)
                {
                    frame_to_use = 1;
                }
                else
                {
                    frame_to_use = 0;
                }
                alt_frame_timer = alt_frame_timer_MAX;
            }
            
            portrait.GetComponent<Image>().sprite = Textwriter.instance.character_data.get_emotion(Textwriter.instance.current_emotion, frame_to_use);
            
            break;

            case "finished":
            break;

            default:
            break;
        }
    }
}
