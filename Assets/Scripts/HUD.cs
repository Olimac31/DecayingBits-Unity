using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public GameObject hp_object, coin_object, hp_fill;
    Animator anims;
    bool hud_is_on = false;

    TMPro.TextMeshProUGUI hp_text, coin_text;

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

    void Start()
    {
        hp_text = hp_object.GetComponent<TMPro.TextMeshProUGUI>();
        coin_text = coin_object.GetComponent<TMPro.TextMeshProUGUI>();
        anims = GetComponent<Animator>();

        // Update health text when health is changed in the Game Manager
        GameManager.instance.health_changed.AddListener(update_health_text);

        update_health_text();
    }

    void update_health_text()
    {
        hp_text.text = GameManager.instance.playerHP.ToString() + "/" + GameManager.instance.playerHPMAX.ToString();
        hp_fill.GetComponent<Image>().fillAmount = GameManager.instance.playerHP / GameManager.instance.playerHPMAX;
    }

    public void HUD_ENTER()
    {
        if (!hud_is_on)
        {
            anims.Play("HUD_enter");
            hud_is_on = true;
        }
    }
    public void HUD_EXIT()
    {
        if (hud_is_on)
        {
            anims.Play("HUD_exit");
            hud_is_on = false;
        }
    }

    public void HUD_HIDE()
    {
        anims.Play("HUD_hide");
        hud_is_on = false;
    }

    public void HUD_SHOW()
    {
        anims.Play("HUD_show");
        hud_is_on = true;
    }
}
