using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this) 
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    #region Scene Transitions
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("LoseScene");
    }
    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
    #endregion
}
