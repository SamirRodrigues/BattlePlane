using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; //	Lock the cursor inside the screen
        Cursor.visible = true;
    }

    public void MenuScene()
    {
        GameManager.Instance.ChangeScene("Menu");
    }

}
