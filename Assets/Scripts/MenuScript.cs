using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    #region Variables
    [Header("Bools")]
    public bool showOptions;
    public bool fullScreenToggle;
    public bool muteToggle;
    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode sprint;
    public KeyCode crouch;
    public KeyCode interact;
    // This remembers the key code of a key we're trying to change
    public KeyCode holdingKey;
    [Header("References")]
    public float volumeSlider, holdingVolume;
    public float brightnessSlider;
    public AudioSource music;
    public Light dirLight;
    [Header("Resolutions and Screen Elements")]
    public int index;
    public int[] resX, resY;
    public float scrW, scrH;
    [Header("Art")]
    public GUIStyle background;
    public GUISkin menuSkin;


    #endregion
    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;

        dirLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        music = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        music.volume = 1;
        volumeSlider = music.volume;
        brightnessSlider = dirLight.intensity;
    }

    void Update()
    {
        if (music != null)
        {
            if (muteToggle == false)
            {
                if (music.volume != volumeSlider)
                {
                    holdingVolume = volumeSlider;
                    music.volume = volumeSlider;
                }
            }
            else
            {
                volumeSlider = 0;
                music.volume = 1;
            }
        }
        if (dirLight != null)
        {
            if (brightnessSlider != dirLight.intensity)
            {
                dirLight.intensity = brightnessSlider;
            }
        }
        if (muteToggle == true && volumeSlider != 0)
        {
            holdingVolume = volumeSlider;
            volumeSlider = 0;
        }
        if (muteToggle == false && volumeSlider != holdingVolume)
        {
            volumeSlider = holdingVolume;
        }
    }

    void OnGUI()
    {
        if (!showOptions)//if we are on our Main Menu and not our Options
        {

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", background);//background
            GUI.skin = menuSkin;
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Naruto");//title
            //Buttons
            if (GUI.Button(new Rect(0.7f * scrW, 4 * scrH, 4 * scrW, scrH), "Play"))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect(0.7f * scrW, 5 * scrH, 4 * scrW, scrH), "Options"))
            {
                showOptions = true;
            }
            if (GUI.Button(new Rect(0.7f * scrW, 6 * scrH, 4 * scrW, scrH), "Exit"))
            {
                Application.Quit();
            }
        }

        else if (showOptions)//if we are on our Options Menu!!!!!
        {
            //set our aspect shiz if screen size changes
            if (scrW != Screen.width / 16)
            {
                scrW = Screen.width / 16;
                scrH = Screen.height / 9;
            }
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");//background
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Options");//title

            if (GUI.Button(new Rect(14.875f * scrW, 8.375f * scrH, scrW, 0.5f * scrH), "Back"))
            {
                showOptions = false;
            }
            #region KeyBinding
            GUI.Box(new Rect(scrW * 13, scrH * 2.90f, scrW, scrH), "W");

            GUI.Box(new Rect(scrW * 13f, scrH * 4, scrW, scrH), "S");

            GUI.Box(new Rect(scrW * 11.90f, scrH * 4, scrW, scrH), "A");

            GUI.Box(new Rect(scrW * 14.10f, scrH * 4, scrW, scrH), "D");

            GUI.Box(new Rect(scrW * 11.90f, scrH * 5.10f, scrW, scrH), "E");

            GUI.Box(new Rect(scrW * 13, scrH * 5.10f, scrW, scrH), "LMB");

            GUI.Box(new Rect(scrW * 14.10f, scrH * 5.10f, scrW, scrH), "2");
            #endregion
            #region Brightness and Audio
            int lightSoundIndex = 0;
            GUI.Box(new Rect(0.25f * scrW, 3 * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Volume");//Label
            volumeSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.25f * scrH), volumeSlider, 0, 1);
            if (GUI.Button(new Rect(3.75f * scrW, 3 * scrH + (lightSoundIndex * (scrH * 0.5f)), 0.5f * scrW, 0.5f * scrH), "M"))//Label
            {
                ToggleVolume();
            }
            lightSoundIndex++;
            GUI.Box(new Rect(0.25f * scrW, 3 * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Brightness");//Label
            brightnessSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.25f * scrH), brightnessSlider, 0, 1);
            //box     0.25f    || 3 + i*0.5f     || 1.75f || 0.5f
            //sliders 2        || 3.125f + i*0.5f|| 1.75f || 0.25f
            #endregion
            #region Resolution and Screen
            #endregion
        }
    }
    bool ToggleVolume()
    {
        if (muteToggle == true)
        {
            volumeSlider = holdingVolume;
            muteToggle = false;
            return false;
        }
        else
        {
            muteToggle = true;
            holdingVolume = volumeSlider;
            volumeSlider = 0;
            music.volume = 0;
            return true;
        }
    }
}