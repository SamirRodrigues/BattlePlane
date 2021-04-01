using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private bool gameStart = false;
    private bool gameOver = false;    

    private GameObject[] enemies;

    private int enemiesLength;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);            
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void Start()
    {
        gameStart = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            GameLoop();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameOver = false;
        }
    }    

    void GameLoop()
    {
        if (!gameStart)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1) //Game Scene
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                enemiesLength = enemies.Length;
                gameStart = true;
            }
        }
        else
        {
            if (PlayerManager.Instance == null && !gameOver && gameStart)
            {
                gameOver = true;
                gameStart = false;
                ChangeScene("GameOver");
            }

            if (enemiesLength <= 0)
            {
                gameOver = true;
                gameStart = false;
                ChangeScene("WonGameOver");
            }
        }
    }

    public void EnemyDefeated()
    {
        enemiesLength -= 1;
    }

    public void ResetConfigs()
    {
        gameStart = false;
        gameOver = true;
    }

    public void GameStart()
    {
        gameStart = true;
    }

    public void ChangeScene(string value)
    {        
        ResetConfigs();
        Time.timeScale = 1f;
        SceneManager.LoadScene(value);        
    }
}
