using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandle : MonoBehaviour
{
    public static GameObject victoryScreen;
    public static GameObject drawScreen;
    public static void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public static void DrawScreen()
    {
        drawScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
