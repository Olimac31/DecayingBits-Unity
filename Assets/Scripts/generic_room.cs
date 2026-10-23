using UnityEngine;

public class generic_room : MonoBehaviour
{
    public bool hud_visible_on_enter = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hud_visible_on_enter)
        {
            HUD.instance.HUD_ENTER();
        }
        else
        {
            HUD.instance.HUD_EXIT();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
