using UnityEngine;

public class scr_actor : MonoBehaviour
{
    public float anim_speed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animator>().speed = anim_speed;
    }
}
