using UnityEngine;
using UnityEngine.SceneManagement;

public class coin : MonoBehaviour
{
    int coin_value = 1;
    public AudioClip coin_sfx;
    bool grabbed = false;
    detects_player collision;
    Animator anims;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collision = GetComponentInChildren<detects_player>();
        anims = GetComponent<Animator>();
        anims.speed = 0.2f;

        if (GameManager.instance.return_coin_state(this.gameObject.name, SceneManager.GetActiveScene().path))
        {
            die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(collision.colliding && !grabbed)
        {
            grabbed = true;
            GameManager.instance.grab_coin(coin_value, this.gameObject.name, SceneManager.GetActiveScene().path);
            SoundManager.instance.PlaySound(coin_sfx);
            die();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }
}
