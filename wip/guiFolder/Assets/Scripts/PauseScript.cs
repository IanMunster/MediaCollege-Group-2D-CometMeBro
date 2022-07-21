using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{

    public GameObject PauseUI;
    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Quit(string name)
    {
        Application.LoadLevel(name);
    }

    public void Resume()
    {
        paused = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("P is pressed");
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;

        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

    }

}