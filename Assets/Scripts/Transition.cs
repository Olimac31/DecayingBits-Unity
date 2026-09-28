using UnityEngine;

public class Transition : MonoBehaviour
{
    public static Transition instance;
    Animator anim_player;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim_player = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void trans_in()
    {
        anim_player.Play("transition_in");
    }

    public void trans_out()
    {
        anim_player.Play("transition_out");
    }

    public void trans_none()
    {
        anim_player.Play("transition_reset");
    }

    public void trans_instant_cover()
    {
        anim_player.Play("transition_instant_cover");
    }
}
