using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject infoScreen;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameWinScreen;

    private void Awake()
    {
        player.gameObject.SetActive(false);
        roomManager.paused = false;
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinScreen.SetActive(false);
        infoScreen.SetActive(false);
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
        SoundManager.Instance.Play(SoundEvents.GameOver);
        player.gameObject.SetActive(false);
        roomManager.paused = true;
        gameOverMenu.SetActive(true);
    }

    private void GameWin()
    {
        SoundManager.Instance.Play(SoundEvents.GameWin);
        player.gameObject.SetActive(false);
        roomManager.paused = true;
        gameWinScreen.SetActive(true);
    }

    public void PlayGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        DisplayInfo();
        player.gameObject.SetActive(true);
        roomManager.paused = false;
    }

    public void QuitGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        Application.Quit();
    }

    public void PauseGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        player.gameObject.SetActive(false);
        roomManager.paused = true;
        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
    }

    public void ExitGame()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        PlayerPrefs.DeleteAll();
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinScreen.SetActive(false);
    }

    public void DisplayInfo()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        infoScreen.SetActive(true);
    }

    public void HideInfo()
    {
        SoundManager.Instance.Play(SoundEvents.ButtonClick);
        infoScreen.SetActive(false);
    }

}
