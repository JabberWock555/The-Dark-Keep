using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] RoomManager roomManager;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameWinScreen;

    private void Awake()
    {
        player.gameObject.SetActive(false);
        roomManager.paused = false;
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinScreen.SetActive(false);
    }
    private void Update()
    {
        if (player.win)
        {
            GameWin();
        }
        else if (player.Health <= 0 && !startMenu.activeSelf)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        player.gameObject.SetActive(false);
        roomManager.paused = true;
        gameOverMenu.SetActive(true);
    }

    private void GameWin()
    {
        player.gameObject.SetActive(false);
        roomManager.paused = true;
        gameWinScreen.SetActive(true);
    }

    public void PlayGame()
    {
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        player.gameObject.SetActive(true);
        roomManager.paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        player.gameObject.SetActive(false);
        roomManager.paused = true;
        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
    }
    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinScreen.SetActive(false);
    }
}