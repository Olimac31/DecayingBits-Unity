using UnityEngine;

public class detects_player : MonoBehaviour
{
    public bool colliding = false;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            colliding = true;
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            colliding = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            colliding = false;
        }
    }

    public bool is_colliding()
    {
        return colliding;
    }
}
