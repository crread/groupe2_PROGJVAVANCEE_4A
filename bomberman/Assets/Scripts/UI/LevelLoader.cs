using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Classe a recuperer dans l'inspecteur afin d'affilier les methodes aux boutons
/// </summary>
public class LevelLoader : MonoBehaviour
{

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    /// <summary>
    /// Permet de charger une nouvelle partie depuis le menu principale
    /// </summary>
    public void LoadNewGame()
    {
        SetTimeScaleTo1();
        StartCoroutine(LoadLevel(1));
    }
    /// <summary>
    /// Permet de charger une nouvelle partie apresen avoir termine une
    /// </summary>
    public void LoadWinNewGame()
    {
        SetTimeScaleTo1();
        StartCoroutine(LoadLevel(1));
    }
    /// <summary>
    /// Permet de charger le menu principale
    /// </summary>
    public void LoadMainMenu()
    {
        SetTimeScaleTo1();
        StartCoroutine(LoadLevel(0));
    }
    /// <summary>
    /// Verifie que le temps est bien retourne a la normal
    /// </summary>
    public void SetTimeScaleTo1()
    {
        Time.timeScale = 1f;
    }
    /// <summary>
    /// Permet de jouer une animation de transition avant de charger un niveau
    /// </summary>
    /// <param name="_levelIndex"></param>
    /// <returns></returns>
    IEnumerator LoadLevel(int _levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_levelIndex);
    }
}
