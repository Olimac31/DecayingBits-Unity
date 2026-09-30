using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int GlobalPixelsPerUnit = 16; //IMPORTANT
    public static GameManager instance;

    public float playerHP = 3.0f; //Player Health
    public float playerHPMAX = 3.0f; //Max player health
    public bool playerJustDied = false;

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
}
