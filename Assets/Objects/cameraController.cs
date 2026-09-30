using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] public Transform target;
    private playerMovement current_player;

    public Vector2 offset = new Vector2(0.25f, -0.25f);

    public float camLimitUp, camLimitDown, camLimitLeft, camLimitRight;

    bool cameraWaiting = true;
    float cameraWaitingTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if(target.CompareTag("Player"))
        {
            current_player = target.GetComponent<playerMovement>();

            if(current_player != null)
            {
                current_player.onDeath.AddListener(on_player_death);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Fix odd bug where objects load weirdly for one frame
        if(cameraWaiting)
        {
            turnOnCamera();
        }
        
        // Go to player, then clamp your position
        transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, camLimitLeft + offset.x, camLimitRight + offset.x), Mathf.Clamp(transform.position.y, camLimitDown + offset.y, camLimitUp + offset.y), transform.position.z);
    }

    void turnOnCamera()
    {
        //Camera starts disabled to allow for objects to load first
        if(cameraWaitingTime > 0.0f)
        {
            cameraWaitingTime -= Time.deltaTime;
        }
        else
        {
            GetComponent<Camera>().enabled = true;
            cameraWaiting = false;
        }
    }

    public void on_player_death()
    {
        GameManager.instance.camera_global_position_on_death = new Vector2(transform.position.x, transform.position.y);
        GameManager.instance.camera_limit_h_on_death = new Vector2(camLimitLeft, camLimitRight);
        GameManager.instance.camera_limit_v_on_death = new Vector2(camLimitDown, camLimitUp);

        current_player.onDeath.RemoveListener(on_player_death);
    }
}
