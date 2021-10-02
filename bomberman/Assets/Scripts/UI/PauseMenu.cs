using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public static GameObject pauseMenuUI;

    private void Awake()
    {
        pauseMenuUI = this.gameObject;
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// Methode a appeler pour faire apparaitre le menu Pause
    /// </summary>
    /// <param name="_status"></param>
    public static void OnPauseMenu(bool _status)
    {
        if (!_status)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    private static void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    static void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    /// <summary>
    /// Methode public a placer dans l'event du boutton UI
    /// </summary>
    public void Button_Resume()
    {
        Resume();
    }
    /// <summary>
    /// Quitte le jeu
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
}
