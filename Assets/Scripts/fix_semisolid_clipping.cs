using UnityEngine;


// UNUSED ----------------------------------------------------------------
public class fix_semisolid_clipping : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.rigidbody.tag == "Player")
        {
            Vector3 temp_transform = other.rigidbody.GetComponent<playerMovement>().transform.position;

            temp_transform.y += 1.0f;
            other.rigidbody.GetComponent<playerMovement>().transform.position = temp_transform;
        }
    }
}
