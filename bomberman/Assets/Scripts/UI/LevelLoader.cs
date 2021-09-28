using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNewGame()
    {
       StartCoroutine(LoadLevel(1)); // Ou SceneManager.GetActiveScene().buildIndex + 1; pour la scene qui suit dans le build manager
    }

    public void LoadWinNewGame()
    {
        //CheckTimeScale();
        Time.timeScale = 1f;
        //PauseMenu.GameisPaused = false;
        //RoomManager.NombreActuelRoom = 0;
        //RoomManager.instance.NombreMaxRoom += 2;
       // RoomManager.instance.stopGenerate = false;
        StartCoroutine(LoadLevel(1));
        //EndGameHandle.done = false;
    }
    public void LoadLoseNewGame()
    {
        //CheckTimeScale();
        Time.timeScale = 1f;
        PauseMenu.GameisPaused = false;
        //RoomManager.NombreActuelRoom = 0;
        //RoomManager.instance.NombreMaxRoom = 6;
        //RoomManager.instance.stopGenerate = false;
        StartCoroutine(LoadLevel(1));
        EndGameHandle.done = false;
    }
    public void LoadLevelFromASave()
    {
        StartCoroutine(LoadLevel(2)); // Ou SceneManager.GetActiveScene().buildIndex + 1; pour la scene qui suit dans le build manager
        EndGameHandle.done = false;
    }
    public void LoadMainMenu()
    {
        CheckTimeScale();
        StartCoroutine(LoadLevel(0));
        //EndGameHandle.done = false;
    }

    public void LoadScene()
    {
        ObjectLoaderHelper.loadScene = true;
    }

    public void MainScene()
    {
        ObjectLoaderHelper.loadScene = false;
    }
    public void NewGame()
    {
        ObjectLoaderHelper.instance.fileToLoad = null;
    }
    public void SetTimeScaleTo1()
    {
        Time.timeScale = 1;
    }
    //public void FromSave()
    //{
    //    ObjectLoaderHelper.instance.fileToLoad = Application.persistentDataPath + SaveManager.directory + SaveManager.saveFilename;
    //}
    //public void FromEditor()
    //{
    //    ObjectLoaderHelper.instance.fileToLoad = Application.persistentDataPath + SaveManager.directory + SaveManager.editorFilename;
    //}



    IEnumerator LoadLevel(int _levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_levelIndex);
    }
    void CheckTimeScale()
    {
        Time.timeScale = 1f;
        //PauseMenu.GameisPaused = false;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
