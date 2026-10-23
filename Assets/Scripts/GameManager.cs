using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public int GlobalPixelsPerUnit = 16; //IMPORTANT
    public static GameManager instance;

    public float playerHP = 3.0f; //Player Health
    public float playerHPMAX = 3.0f; //Max player health
    public bool playerJustDied = false;
    public int playerCoin = 0;

    public int targetSpawn = 0; // The target spawnpoint, used between every single room transition
    public string checkpointRoom = ""; // The room where the checkpoint is
    public int checkpointTargetSpawn = 0; // Checkpoint spawn point
    
    public string preDeathRoom = ""; // The room in which you were before dying
    public int preDeathSong = 0;
    public bool game_over_queued = false;
    public float game_over_timer = 0.0f;

    public Vector2 global_position_on_death = new Vector2(0, 0);
    public Vector2 camera_global_position_on_death = new Vector2(0, 0);
    public Vector2 camera_limit_h_on_death = new Vector2(-999, 999);
    public Vector2 camera_limit_v_on_death = new Vector2(-999, 999);

    public bool globalLockedBlocks = true;

    public UnityEvent health_changed;
    public UnityEvent coin_changed;

    /*
    ////////////////////////////
    //   Inventory Variables  //-------------------------------------
    ///////////////////////////
    */

    private Dictionary<string, List<string>> coin_tracker = new Dictionary<string, List<string>>();
    private Dictionary<string, List<string>> og_coin_tracker = new Dictionary<string, List<string>>();

    
    /*
    ///////////////////////////////
    //   DEMO/PLACEHOLDER STUFF  //-------------------------------------
    ///////////////////////////////
    */

    public bool player_broken_jump = true;
    float player_broken_jump_force = 16.0f;


    //Singleton code---------------------------------
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coin_tracker.Add("room name here", new List<string>(){"coin name here"});
        og_coin_tracker.Add("room name here", new List<string>(){"coin name here"});
    }

    // Update is called once per frame
    void Update()
    {
        // GAME OVER QUEUE
        if(game_over_queued)
        {
            game_over_timer += Time.deltaTime;

            if(game_over_timer >= 0.01f)
            {
                goto_gameover();
            }
        }
    }

    public void on_player_death()
    {
        preDeathRoom = SceneManager.GetActiveScene().path;
        playerHP = playerHPMAX;
        game_over_timer = 0.0f;
        game_over_queued = true;
    }

    public void goto_gameover()
    {
        // Reset game over queue variables
        game_over_queued = false;
        game_over_timer = 0.0f;

        // Go to game over scene
        SceneManager.LoadScene("Assets/Scenes/GameOver.unity", LoadSceneMode.Single);
    }

    public void on_health_change()
    {
        health_changed.Invoke();
    }

    public void on_coin_change()
    {
        coin_changed.Invoke();
    }

    public void grab_coin(int coins_grabbed, string coin_name, string room_where_grabbed)
    {
        playerCoin += coins_grabbed;

        // Check current room exists in the coin tracker
        if(coin_tracker.ContainsKey(room_where_grabbed))
        {
            coin_tracker[room_where_grabbed].Add(coin_name);
        }
        else
        {
            coin_tracker[room_where_grabbed] = new List<string>(){coin_name};
        }
        on_coin_change();
    }

    public bool return_coin_state(string coin_to_check, string room_to_check)
    {
        if (coin_tracker.ContainsKey(room_to_check))
        {
            foreach(string coin_id in coin_tracker[room_to_check])
            {
                if(coin_id == coin_to_check)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public float get_broken_jump()
    {
        return player_broken_jump_force;
    }
}
