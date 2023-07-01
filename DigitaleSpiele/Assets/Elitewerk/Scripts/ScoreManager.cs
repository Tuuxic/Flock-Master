using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public GameObject startScreen;
    public GameObject endScreen;
    public Button startButton;
    public GameObject player;
    public Text progressText;
    public Text scoreText;

    private int numStudents = 0;
    private float startTimestamp;
    private float endTimestamp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startScreen.SetActive(true);
        endScreen.SetActive(false);
        player.SetActive(false);
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        startScreen.SetActive(false);
        player.SetActive(true);
        startTimestamp = Time.time;
    }

    private void EndGame()
    {
        endScreen.SetActive(true);
        player.SetActive(false);
        endTimestamp = Time.time;
        UpdateScoreText();
    }

    private void UpdateProgressText()
    {
        progressText.text = numStudents.ToString() + " STUDENTS LEFT";
    }

    private void UpdateScoreText()
    {
        string seconds = (endTimestamp - startTimestamp).ToString("F2");
        scoreText.text = "TIME: " + seconds + " seconds";
    }

    public void RegisterStudent()
    {
        numStudents++;
        UpdateProgressText();
    }

    public void UnregisterStudent()
    {
        numStudents--;
        UpdateProgressText();
        if (numStudents == 0)
        {
            EndGame();
        }
    }
}
