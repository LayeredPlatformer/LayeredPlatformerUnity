using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class PauseController : MonoBehaviour
    {
    public bool paused;
    public CanvasGroup _pauseCanvasGroup;
    

    public bool isPaused
    {
        get
        {
            return paused;
        }
    }
    public void start()
    {
        paused = false;
    }
    public void Pause()
    {
        Console.WriteLine("Pause Called");
        if (paused)
            {
                Time.timeScale = 1;
                paused = false;
            _pauseCanvasGroup.alpha = 0;
            _pauseCanvasGroup.interactable = false;
            _pauseCanvasGroup.blocksRaycasts = false;
            
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            _pauseCanvasGroup.alpha = 1;
            _pauseCanvasGroup.interactable = true;
            _pauseCanvasGroup.blocksRaycasts = true;
        }
        
        }
    }

