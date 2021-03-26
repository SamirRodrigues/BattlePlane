using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameManager gManager;

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
        Cursor.lockState = CursorLockMode.None; //	Lock the cursor inside the screen
        Cursor.visible = true;
    }

    public void MenuScene()
    {
        gManager.ChangeScene("Menu");
    }

}
