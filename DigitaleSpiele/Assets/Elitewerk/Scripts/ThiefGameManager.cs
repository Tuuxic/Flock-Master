using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThiefGameManager : MonoBehaviour
{
    public GameObject startScreen;
    public Button startButton;
    public GameObject player;

    void Start()
    {
        startScreen.SetActive(true);
        player.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        Cursor.lockState = CursorLockMode.None;
    }

    void StartGame()
    {
        startScreen.SetActive(false);
        player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
