using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectLoaderHelper : MonoBehaviour
{
    public GameObject[] myList;
    public static Dictionary<string, GameObject> myDico;
    public GameObject Player;
    public static bool loadScene;
    public string fileToLoad;

    public static ObjectLoaderHelper instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("Il existe deja une instance de ObjectLoaderHelper");
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        myDico = new Dictionary<string, GameObject>();
        myList = Resources.LoadAll<GameObject>("Map rooms");
        foreach (GameObject item in myList)
        {
            myDico.Add(item.name, item);
        }
    }

}
