using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager gManager;

    public static bool isGamePauded = false;

    public GameObject pauseUI;
    [SerializeField]
    private GameObject pointer;   


    private void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePauded)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pointer.GetComponent<CustomPointer>().SetAtive(true);
        pauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isGamePauded = false;
    }

    private void Pause()
    {
        pointer.GetComponent<CustomPointer>().SetAtive(false);
        pauseUI.SetActive(true);
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        isGamePauded = true;
    }

    public void LoadMenu()
    {
        gManager.ChangeScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
