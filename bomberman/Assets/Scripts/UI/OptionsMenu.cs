using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); 
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        /// On met toutes les resolutions possibles dans une liste de string
        /// 
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);


            /// On fait en sorte de vérifier quel est la résolution actuelle de l'utilisateur afin de la mettre par defaut
            /// 
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ///Permet de mettre la liste precedement générée en tant qu'option dans le dropdown
        ///
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    /// <summary>
    /// Contrôle du Volume
    /// </summary>
    /// <param name="_volume"></param>
    public void SetVolume (float _volume) 
    {
        audioMixer.SetFloat("Volume", _volume);
    }
    /// <summary>
    /// Contrôle de la qualité graphique
    /// </summary>
    /// <param name="_qualityIndex"></param>
    public void SetGraphics(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }
    public void SetResolution(int _resolutionIndex)
    {
        Resolution _resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Toggle du Fullscreen
    /// </summary>
    /// <param name="_isFullscreen"></param>
    public void SetFullScreen (bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
    }
}
