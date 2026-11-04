using UnityEngine;

public class scr_lifeup : MonoBehaviour
{
    public AudioClip sfx_lifeup;
    float heal_amount = 1.0f;
    detects_player collision;
    bool grabbed = false;

    void Start()
    {
        collision = GetComponentInChildren<detects_player>();
    }


    // Update is called once per frame
    void Update()
    {
        if(collision.colliding && !grabbed)
        {
            SoundManager.instance.PlaySound(sfx_lifeup);
            GameManager.instance.heal_player(heal_amount);
            grabbed = true;
            
            Destroy(gameObject);
        }
    }
}
