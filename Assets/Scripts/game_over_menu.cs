using UnityEngine;
using UnityEngine.SceneManagement;

public class game_over_menu : MonoBehaviour
{
    public bool exited = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && !exited)
        {
            exited = true;
            goback();
        }
    }

    public void goback()
    {
        SceneManager.LoadScene(GameManager.instance.preDeathRoom, LoadSceneMode.Single);
    }
}
