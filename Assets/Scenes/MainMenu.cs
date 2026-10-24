using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    const string STATES_CHOOSING = "choosing", STATES_LEAVING = "leaving", STATES_QUITTING = "quitting";
    string state = STATES_CHOOSING;
    int option_id = 0;
    int chosen_option = 0;
    GameObject option_list;
    List<Vector2> option_positions = new List<Vector2>();

    public GameObject select_square;
    Vector2 square_target_position = new Vector2(0, 0);

    public GameObject leaving_timer;
    Animator anims;
    

    public string scene_to_load = "";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        option_list = GameObject.Find("options");

        foreach(Transform pos in option_list.GetComponentInChildren<Transform>())
        {
            // Save the position of each option
            option_positions.Add(new Vector2(pos.position.x, pos.position.y));
        }

        anims = GameObject.Find("MENU_ANIMS").GetComponent<Animator>();

        leaving_timer.GetComponent<GodotTimer>().timeout.AddListener(leave_menu);

        change_option(0);
    }

    // Update is called once per frame
    void Update()
    {
        state_behavior();
    }

    public void change_state(string new_state = "")
    {
        state = new_state;

        switch(state)
        {
            case STATES_CHOOSING:
            chosen_option = 0;
            break;

            case STATES_LEAVING:
            anims.Play("anim_quit");
            leaving_timer.GetComponent<GodotTimer>().timer_start(); // TODO ADD LISTENER
            break;

            case STATES_QUITTING:
            Application.Quit();
            break;

            default:
            break;
        }
    }

    public void state_behavior()
    {
        switch (state)
        {
            case STATES_CHOOSING:
            behavior_choosing();
            break;

            case STATES_LEAVING:
            behavior_leaving();
            break;

            case STATES_QUITTING:
            break;

            default:
            break;
        }
    }


    public void change_option(int amount)
    {
        int temp_option = option_id + amount;

        // Lower limit
        if (temp_option < 0)
        {
            temp_option = 0;
        }

        // Upper limit
        if (temp_option > option_positions.Count - 1)
        {
            temp_option = option_positions.Count - 1;
        }

        option_id = temp_option;
        square_target_position = option_positions[option_id];
    }

    public void choose_an_option()
    {
        switch(option_id)
        {
            // Start
            case 0:
            scene_to_load = "Assets/Scenes/SampleScene.unity";
            change_state(STATES_LEAVING);
            break;

            // Credits
            case 1:
            scene_to_load = "Assets/Scenes/testroom1.unity";
            change_state(STATES_LEAVING);
            break;

            // Exit
            case 2:
            change_state(STATES_QUITTING);
            break;

            default:
            break;
        }
    }

    public void behavior_choosing()
    {
        // NAVIGATE OPTIONS
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            change_option(-1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            change_option(1);
        }

        if (Input.GetButtonDown("Submit"))
        {
            choose_an_option();
        }

        // VISUALS
        select_square.transform.position = Vector3.Lerp(select_square.transform.position, square_target_position, 20.0f * Time.deltaTime);
    }

    public void behavior_leaving()
    {
        
    }

    public void leave_menu()
    {
        GameManager.instance.change_room(scene_to_load);
    }
}
