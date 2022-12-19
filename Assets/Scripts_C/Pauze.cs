using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauze : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject pauseMenuUI;
    public Animation vignetteanim;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Button pressed");
            if (gameIsPaused)
            {
                Resume();
                vignetteanim.Play();
            }
            else
            {
                Pause();
            }
        }
    }
    //resumed de game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    //word geactivate on esc
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
