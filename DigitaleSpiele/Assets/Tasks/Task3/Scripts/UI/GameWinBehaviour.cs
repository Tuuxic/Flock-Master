using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinBehaviour : MonoBehaviour
{
    public Text textDisplay;
    private float startTime;
    void Start()
    {
        gameObject.SetActive(false);
        GameEventManager.AddListener<GameWinEvent>(OnWin);
        startTime = Time.time;
    }
    public void Setup()
    {
        float playtime = Time.time - startTime;
        gameObject.SetActive(true);
        Cursor.visible = gameObject.activeSelf;
        Cursor.lockState = CursorLockMode.Confined;

        // Disable Game Events
        GameEventManager.isRaiseEnabled = false;

        if(textDisplay != null)
        {
            int minutes = (int) playtime / 60;
            int seconds = (int) playtime % 60;
            textDisplay.text = "Playtime: " + minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }

        // Disable all irelavant Game Objects
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if ((o.tag != "UI") && (o.tag != "MainCamera") && (o.tag != "EventSystem"))
            {
                o.SetActive(false);
            }
        }

    }

    public void OnRestart()
    {
        GameEventManager.Reset();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnWin(GameWinEvent e)
    {
        Setup();
    }
}
