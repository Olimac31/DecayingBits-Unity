using UnityEngine;
using UnityEngine.SceneManagement;

public class game_over_menu : MonoBehaviour
{
    public GameObject bernard_dead;
    public cameraController cam;
    public bool exited = false;

    public Vector2 offset = new Vector2(0f, 0f);

    public GodotTimer timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HUD.instance.HUD_HIDE();

        cam.camLimitDown = GameManager.instance.camera_limit_v_on_death.x;
        cam.camLimitUp = GameManager.instance.camera_limit_v_on_death.y;
        cam.camLimitLeft = GameManager.instance.camera_limit_h_on_death.x;
        cam.camLimitRight = GameManager.instance.camera_limit_h_on_death.y;

        bernard_dead.transform.position = GameManager.instance.global_position_on_death;
        bernard_dead.transform.position = new Vector3(bernard_dead.transform.position.x + offset.x, bernard_dead.transform.position.y + offset.y, bernard_dead.transform.position.z);

        if(GameManager.instance.flipped_state_on_death)
        {
            bernard_dead.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            bernard_dead.GetComponent<SpriteRenderer>().flipX = false;
        }

        timer.timeout.AddListener(goback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goback()
    {
        SceneManager.LoadScene(GameManager.instance.preDeathRoom, LoadSceneMode.Single);
        Transition.instance.trans_none();

        timer.timeout.RemoveListener(goback);
    }
}
