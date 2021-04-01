using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.ChangeScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
