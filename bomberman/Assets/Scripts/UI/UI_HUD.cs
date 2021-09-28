using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_HUD : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    private void Start()
    {
        EndGameHandle.victoryScreen = victoryScreen;
        EndGameHandle.defeatScreen = defeatScreen;
    }
    //[SerializeField] TMPro.TMP_Text textTime;
    //[SerializeField] int myTime = 60;
    //bool endAnimation = false;


    //private void Start()
    //{
    //    StartCoroutine(Timer());
    //}

    //private void Update()
    //{
    //    if (endAnimation)
    //    {
    //        myTime = (int)Mathf.Round(myTime - Time.deltaTime);
    //        textTime.text = "TIME : " + myTime;
    //    }

    //}
    //IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(4);
    //    endAnimation = true;
    //}
}
