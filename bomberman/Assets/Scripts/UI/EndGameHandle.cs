using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameHandle : MonoBehaviour
{
    public static GameObject victoryScreen;
    public static GameObject drawScreen;
    
    [SerializeField] private GameObject player1;
    private static GameObject player1Static;
    [SerializeField] private GameObject player2;
    private static GameObject player2Static;
    [SerializeField] private TMP_Text playerNumber;
    private static TMP_Text playerNumberStatic;

    private void Start()
    {
        player1Static = player1;
        player2Static = player2;
        playerNumberStatic = playerNumber;
    }

    public static void VictoryScreen()
    {
        if (player1Static.activeSelf)
        {
            playerNumberStatic.text = "Victory player 1";
        }
        else
        {
            playerNumberStatic.text = "Victory player 2";
        }
        
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
