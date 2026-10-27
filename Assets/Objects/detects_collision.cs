using UnityEngine;

public class detects_collision : MonoBehaviour
{
    public bool colliding = false;
    public bool detects_semisolid = false;

    protected playerMovement player;

    void Start()
    {
        colliding = false;
        player = GameObject.Find("obj_Player").GetComponent<playerMovement>();
    }
    // Start is called before the first frame update
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        enter_checks(other);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        enter_checks(other);
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ground" || (other.tag == "Semisolid" && detects_semisolid))
        {
            colliding = false;
        }
    }

    public bool is_colliding()
    {
        return colliding;
    }

    public virtual void enter_checks(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            colliding = true;
        }

        else if (detects_semisolid && other.tag == "Semisolid")
        {
            if (player.velocity.y <= 0)
            {
                colliding = true;
            }
        }
    }
}
