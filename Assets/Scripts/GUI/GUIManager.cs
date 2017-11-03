using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    #region Variables
    [Header("Bools")]
    public bool gameScene;
    public bool showOptions;
    public bool pause;
    public bool fullScreenToggle;


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

    [Header("GUI Text")]
    public Text forwardText;
    public Text backwardsText;
    public Text leftText;
    public Text rightText;
    public Text jumpText;
    public Text sprintText;
    public Text crouchText;
    public Text interactText;

    [Header("GUIElements")]
    public GameObject menu;
    public GameObject options;
    public Toggle fullWindowToggle;


    [Header("References")]
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public AudioSource music;
    public Light dirLight;

    [Header("Resolutions")]
    public int index;
    public int[] resX, resY;
    public Dropdown resolutionDropdown;


    #endregion

    void Start()
    {
        Time.timeScale = 1;

        #region PauseMenu
        if (gameScene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #endregion 
        fullScreenToggle = true;

        ResolutionChange();

        if (PlayerPrefs.HasKey("Volume"))
        {
            Load();
        }
        if (volumeSlider != null && music != null)
        {
            volumeSlider.value = music.volume;
        }
        if (brightnessSlider != null && dirLight != null)
        {
            brightnessSlider.value = dirLight.intensity;
        }

        #region Setup Keys
        // Set our keys to the preset keys we may have save or set the keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "C"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

        forwardText.text = forward.ToString();
        backwardsText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        crouchText.text = crouch.ToString();
        sprintText.text = sprint.ToString();
        interactText.text = interact.ToString();
        #endregion

    }

    void Update()
    {
        if (volumeSlider != null && music != null)
        {
            if (music.volume != volumeSlider.value)

            {
                music.volume = volumeSlider.value;
            }
        }
        if (brightnessSlider != null && dirLight != null)
        {
            if (brightnessSlider.value != dirLight.intensity)
            {
                dirLight.intensity = brightnessSlider.value;
            }
        }
        if (gameScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
    public void Return()
    {
        TogglePause();
    }

    public void ShowOptions()
    {
        ToggleOptions();
    }

    public bool ToggleOptions()
    {
        if (showOptions)
        {
            showOptions = false;
            menu.SetActive(true);
            options.SetActive(false);
            return false;
        }

        else
        {
            showOptions = true;
            menu.SetActive(false);
            options.SetActive(true);
            return true;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", music.volume);
        PlayerPrefs.SetFloat("Brightness", dirLight.intensity);

        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    }

    public void Load()
    {
        music.volume = PlayerPrefs.GetFloat("Volume");
        dirLight.intensity = PlayerPrefs.GetFloat("Brightness");
    }

    public void Default()
    {
        volumeSlider.value = 1;
        brightnessSlider.value = 1;
        PlayerPrefs.SetFloat("Volume", 1);
        PlayerPrefs.SetFloat("Brightness", 1);
    }

    public bool TogglePause()
    {
        if (pause)
        {
            if (!showOptions)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                menu.SetActive(false);
                pause = false;
            }
            else
            {
                showOptions = false;
                options.SetActive(false);
                menu.SetActive(true);


            }
            return false;

        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pause = true;
            menu.SetActive(true);
            return true;
        }
    }
    #region Key Press Event
    void OnGUI()
    {
        Event e = Event.current;
        // If forward is set to none
        if (forward == KeyCode.None)
        {
            // If an event is triggered by a key press
            if (e.isKey)
            {
                Debug.Log("Key Pressed: " + e.keyCode);
                // If that key is not the same as any other keys
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                {
                    // Set forward to the event key that was pressed
                    forward = e.keyCode;
                    // Set the holding key to none
                    holdingKey = KeyCode.None;
                    // Set the GUI to the new Key
                    forwardText.text = forward.ToString();
                }
                // Otherwise if it is the same as another key
                else
                {
                    // Set the forward key back to the previous key
                    forward = holdingKey;
                    // Set the holding key to none
                    holdingKey = KeyCode.None;
                    // Set the GUI to the previous key
                    forwardText.text = forward.ToString();
                }
            }
        }
    }
    #endregion
    #region Controls
    public void Forward()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = forward;
            // Set this button to non allowing only this to be editable
            forward = KeyCode.None;
            // Set the text to none
            forwardText.text = forward.ToString();

        }
    }

    public void Backward()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = backward;
            // Set this button to non allowing only this to be editable
            backward = KeyCode.None;
            // Set the text to none
            backwardsText.text = backward.ToString();

        }
    }

    public void Left()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || backward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = left;
            // Set this button to non allowing only this to be editable
            left = KeyCode.None;
            // Set the text to none
            leftText.text = left.ToString();

        }
    }

    public void Right()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = right;
            // Set this button to non allowing only this to be editable
            right = KeyCode.None;
            // Set the text to none
            rightText.text = right.ToString();

        }
    }

    public void Jump()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || sprint == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = jump;
            // Set this button to non allowing only this to be editable
            jump = KeyCode.None;
            // Set the text to none
            jumpText.text = jump.ToString();
        }
    }

    public void Sprint()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = sprint;
            // Set this button to non allowing only this to be editable
            sprint = KeyCode.None;
            // Set the text to none
            sprintText.text = sprint.ToString();
        }
    }

    public void Crouch()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = crouch;
            // Set this button to non allowing only this to be editable
            crouch = KeyCode.None;
            // Set the text to none
            crouchText.text = crouch.ToString();
        }
    }

    public void Interact()
    {
        // If none of the other keys are blank then we cane dit this key
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || crouch == KeyCode.None))
        {
            // Set our holding key to the key of this button
            holdingKey = interact;
            // Set this button to non allowing only this to be editable
            interact = KeyCode.None;
            // Set the text to none
            interactText.text = interact.ToString();
        }
    }
    #endregion
    #region FullScreen Toggle And Resolutions
    public void FullScreenToggle()
    {
        fullScreenToggle = !fullScreenToggle;
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ResolutionChange()
    {
        index = resolutionDropdown.value;

        Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
    }
    #endregion


}

