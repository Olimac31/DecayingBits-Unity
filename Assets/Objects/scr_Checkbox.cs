using UnityEngine;
using UnityEngine.UI;

public class scr_Checkbox : MonoBehaviour
{
    public string setting_to_check = "flashing_lights";

    public Sprite[] sprites;
    
    void Start()
    {
        GameManager.instance.setting_changed.AddListener(on_setting_changed);
        on_setting_changed();
    }

    void on_setting_changed()
    {
        if(GameManager.instance.get_bool_setting(setting_to_check))
        {
            GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            GetComponent<Image>().sprite = sprites[1];
        }
    }
}
