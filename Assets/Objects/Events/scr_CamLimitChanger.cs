using UnityEngine;

public class scr_CamLimitChanger : MonoBehaviour
{
    detects_player collision;
    playerMovement the_player;

    public float up, down, left, right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collision = GetComponent<detects_player>();
        the_player = GameObject.Find("obj_Player").GetComponent<playerMovement>();

        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(collision.colliding)
        {
            the_player.cam.GetComponent<cameraController>().camLimitUp = up;
            the_player.cam.GetComponent<cameraController>().camLimitDown = down;
            the_player.cam.GetComponent<cameraController>().camLimitLeft = left;
            the_player.cam.GetComponent<cameraController>().camLimitRight = right;
        }
    }
}
