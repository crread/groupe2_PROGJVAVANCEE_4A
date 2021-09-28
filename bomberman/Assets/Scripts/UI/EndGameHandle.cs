using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandle : MonoBehaviour
{
    //[SerializeField] private static RoomTemplates roomTemplates;
    public static bool done = false;
    public static GameObject victoryScreen;
    public static GameObject defeatScreen;
    public static void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        //done = true;
       //RoomTemplates.instance.VictoryScreen.SetActive(true);
        Time.timeScale = 0f;
       // PauseMenu.GameisPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void LoseScreen()
    {
        defeatScreen.SetActive(true);
        //done = true;
        //RoomTemplates.instance.LoseScreen.SetActive(true);
        Time.timeScale = 0f;
        //PauseMenu.GameisPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
