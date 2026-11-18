using TMPro;
using UnityEngine;

public class scr_VolumeSlider : MonoBehaviour
{
    public string option_to_get = "master_vol"; // "master_vol", "music_vol", "sfx_vol"

    TextMeshProUGUI txt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.setting_changed.AddListener(on_setting_change);
        txt = GetComponent<TextMeshProUGUI>();

        on_setting_change();
    }

    void on_setting_change()
    {
        float current_value = GameManager.instance.get_setting(option_to_get);

        int bar_length = 10;
        int filled = Mathf.RoundToInt(current_value * bar_length);
        int empty = bar_length - filled;

        txt.text = "[" + new string('=', filled) + new string(' ', empty) + "]" ;
    }
}
