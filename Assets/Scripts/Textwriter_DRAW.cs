using UnityEngine;
using UnityEngine.UI;

public class Textwriter_DRAW : MonoBehaviour
{
    public GameObject textbox;
    public GameObject text;
    TMPro.TextMeshProUGUI text_to_write_to;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_to_write_to = text.GetComponent<TMPro.TextMeshProUGUI>();
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
            Textwriter.instance.canvas_group.alpha = 0;
            break;

            case "reading":
            text_to_write_to.text = "";
            Textwriter.instance.canvas_group.alpha = 1;
            break;

            case "finished":
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
            break;

            case "finished":
            break;

            default:
            break;
        }
    }
}
