using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    private AudioSource m_MyAudio;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (_instance == this) return;
        Destroy(gameObject);
    }

    private void Start()
    {
        m_MyAudio = GetComponent<AudioSource>();
        m_MyAudio.Play();
    }

}

