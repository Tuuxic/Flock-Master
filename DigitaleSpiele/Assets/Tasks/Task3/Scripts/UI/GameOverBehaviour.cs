using GameEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
        GameEventManager.AddListener<GameOverEvent>(OnSimpleEvent);   
    }
    public void Setup()
    {
        gameObject.SetActive(true);
        Cursor.visible = gameObject.activeSelf;
        Cursor.lockState = CursorLockMode.Confined;

        // Disable Game Events
        GameEventManager.isRaiseEnabled = false;

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

    void OnSimpleEvent(GameOverEvent e)
    {
        Setup();
    }
}
