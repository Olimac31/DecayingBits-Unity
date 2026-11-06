using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestart : MonoBehaviour
{
    public float waiting_time = 0.1f;
    public string sceneToLoad = "";

    public bool IS_HTML_BUILD = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!IS_HTML_BUILD)
        {
            Screen.SetResolution(864, 480, FullScreenMode.Windowed);
        }
        else
        {
            sceneToLoad = "Assets/Scenes/MainMenu.unity";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting_time > 0)
        {
            waiting_time -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}
