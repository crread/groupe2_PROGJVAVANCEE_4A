using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HUD : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject drawScreen;
    public Image bombDispo;
    public static Image bomb;
    private void Start()
    {
        EndGameHandle.victoryScreen = victoryScreen;
        EndGameHandle.drawScreen = drawScreen;
        bomb = bombDispo;
        StartCoroutine(Timer());
    }

    //                  /!\ ALERTE /!\                              EN TRAVAUX NE PAS TOUCHER                     /!\ ALERTE /!\

    [SerializeField] TMPro.TMP_Text textTime;
    [SerializeField] float myTime = 120;
    bool endAnimation = false;
    bool over = false;

    private void Update()
    {
        if (endAnimation)
        {
            myTime -= Time.deltaTime;
            DisplayTime(myTime);
            //textTime.text = "TIME : " + myTime;
            if (myTime <= 0 && !over)
            {
                over = true;
                EndGameHandle.DrawScreen();
            }
        }

    }
    public static void SetBombUnavailable()
    {
        Color _temp = bomb.color;
        _temp.a = 50;
        bomb.color = _temp;
    }
    public static void SetBombAvailable()
    {
        Color _temp = bomb.color;
        _temp.a = 255;
        bomb.color = _temp;
    }

    void DisplayTime(float _time)
    {
        _time += 1;

        float minutes = Mathf.FloorToInt(_time / 60);
        float seconds = Mathf.FloorToInt(_time % 60);

        textTime.text = "Timer : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(4);
        endAnimation = true;
    }
}
