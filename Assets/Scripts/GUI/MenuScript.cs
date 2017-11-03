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

        // Set our keys to the preset keys we may have save or set the keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "C"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));


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

            Event e = Event.current;

            int bindingColA = 0;
            int bindingColB = 0;
            int buttonColA = 0;
            int buttonColB = 0;


            string[] keysLabelColA = { "Forward", "Backward", "Left", "Right" };    // Array of key labels
            string[] keysLabelColB = { "Crouch", "Jump", "Sprint", "Interact" };    // Column B
            string[] buttonLabelColA = { "W", "A", "S", "D" };                      // Array of buttons
            string[] buttonLabelColB = { "Space", "Left Ctrl", "Left Shift", "E" }; // Column B


            foreach (string keysLabelLeft in keysLabelColA)
            {
                // Console.WriteLine("{0} ", keysLabelLeft);
                GUI.Box(new Rect(9f * scrW, 3f * scrH + (bindingColA * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), keysLabelLeft); // label
                bindingColA++;
            }

            foreach (string buttonsLabelLeft in buttonLabelColA)
            {
                if (GUI.Button(new Rect(11f * scrW, 3f * scrH + (buttonColA * (scrH * 0.5f)), scrW, 0.5f * scrH), buttonsLabelLeft)) // button
                {
                    Debug.Log("Clicked the button");


                    if (forward == KeyCode.None)
                    {
                        // If an event is triggered by a key press
                        if (e.isKey)
                        {
                            Debug.Log("Key Presssed: " + e.keyCode);
                            // If that key is not the same as any other key 
                            if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                            {
                                // Set forward to the event key that was pressed
                                forward = e.keyCode;

                                // Set the holding key to none
                                holdingKey = KeyCode.None;

                                // Set the GUI to the new key
                                buttonLabelColA[0] = forward.ToString();
                            }

                            // Otherwise if it is the same as another key
                            else
                            {
                                // Set the forward key back to the previous key
                                forward = holdingKey;

                                // Set the holding to none
                                holdingKey = KeyCode.None;

                                // Set the GUI to the previous key
                                buttonLabelColA[0] = forward.ToString();
                            }
                        }
                    }
                }
                buttonColA++;
            }

            foreach (string keysLabelRight in keysLabelColB)
            {
                // Console.WriteLine("{0} ", keysLabelLeft);
                GUI.Box(new Rect(12.1f * scrW, 3f * scrH + (bindingColB * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), keysLabelRight); // label
                bindingColB++;
            }

            foreach (string buttonsLabelRight in buttonLabelColB)
            {
                if (GUI.Button(new Rect(14.1f * scrW, 3f * scrH + (buttonColB * (scrH * 0.5f)), scrW, 0.5f * scrH), buttonsLabelRight)) // button
                {
                    Debug.Log("Clicked the button");
                }
                buttonColB++;
            }
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