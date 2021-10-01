using System;
using UnityEngine;

public class TrackerCollider : MonoBehaviour
{
    public string tagName;

    private void OnTriggerEnter(Collider other)
    {
        tagName = other.gameObject.tag;
    }

    private void OnTriggerExit(Collider other)
    {
        tagName = "";
    }
}
