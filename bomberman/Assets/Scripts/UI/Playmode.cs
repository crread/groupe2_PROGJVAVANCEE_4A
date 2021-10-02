using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playmode : MonoBehaviour
{
    public enum Mode { PVP, PVR, PVmcts, mctsVmcts };
    public static Mode mymode;

    public static Playmode instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
    }

    public void PvpButton()
    {
        mymode = Mode.PVP;
    }
    public void PvRButton()
    {
        mymode = Mode.PVR;
    }
    public void PvMCTSButton()
    {
        mymode = Mode.PVmcts;
    }
}
