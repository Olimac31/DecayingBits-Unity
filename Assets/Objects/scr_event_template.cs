using UnityEngine;

public class scr_event_template : MonoBehaviour
{
    protected bool is_active = false;
    protected bool activated = false;
    public bool on_collide = true;
    protected playerMovement player;

    protected virtual void Start()
    {
        player = GameObject.Find("obj_Player").GetComponent<playerMovement>();
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        // Execute events that are not "on_collision" when the player
        // presses the interact button
        if(is_active && !activated)
        {
            if(!on_collide)
            {
                //If player isn't in a cutscene and presses the interact button
                if(player.keyInteract && !player.reading)
                {
                    execute_event();
                }
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            is_active = true;
            activated = false;

            if(on_collide)
            {
                execute_event();
            }
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            is_active = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            is_active = false;
            activated = false;
        }
    }

    protected virtual void execute_event()
    {
        activated = true;
        event_behavior();
    }

    public virtual void event_behavior()
    {
        Debug.Log("scr_event_template: EVENT TRIGGERED");
    }
}
