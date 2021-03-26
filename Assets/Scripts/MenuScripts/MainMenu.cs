using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gManager;

    private void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Play()
    {
        gManager.ChangeScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
