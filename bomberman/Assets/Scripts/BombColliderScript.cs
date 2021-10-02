using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BombColliderScript : MonoBehaviour
{
    [SerializeField] private Collider component;
        
    private void Start()
    {
        component.isTrigger = true;
    }

    private void OnEnable()
    {
        component.isTrigger = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        component.isTrigger = false;
    }
}