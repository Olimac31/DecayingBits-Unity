using UnityEngine;

public class detects_collision : MonoBehaviour
{
    public bool colliding = false;

    void Start()
    {
        colliding = false;
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground" || other.tag == "SemisolidTag")
        {
            colliding = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ground" || other.tag == "SemisolidTag")
        {
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ground" || other.tag == "SemisolidTag")
        {
            colliding = false;
        }
    }

    public bool is_colliding()
    {
        return colliding;
    }
}
