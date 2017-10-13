using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//you will need to change Scenes
public class CustomisationSet : MonoBehaviour
{

    #region Variables
    // Texture2D List for skin,hair, mouth, eyes
    [Header("Texture List")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();

    // Index numbers for our current skin, hair, mouth, eyes textures
    [Header("Index")]
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex;

    // Renderer for our character mesh so we can reference a material list
    [Header("Renderer")]
    public Renderer character;

    // Max amount of skin, hair, mouth, eyes textures that our lists are filling with
    [Header("Max Index")]
    public int skinMax;
    public int hairMax, mouthMax, eyesMax;

    // Name of our character that the user is making
    [Header("Character Name")]
    public string charName = "Adventure";
    #endregion

    #region Start
    // In start we need to set up the following
    void Start()
    {
        #region for loop to pull textures from file
        // For loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            // Add our temp texture that we just found to the skin List
            skin.Add(temp);
        }
        // For loop looping from 0 to less than the max amount of hair textures we need
        for (int i = 0; i < hairMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
            Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;
            // Add our temp texture that we just found to the hair List
            hair.Add(temp);
        }

        // For loop looping from 0 to less than the max amount of mouth textures we need    
        for (int i = 0; i < mouthMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;
            // Add our temp texture that we just found to the mouth List
            mouth.Add(temp);
        }

        // For loop looping from 0 to less than the max amount of eyes textures we need
        for (int i = 0; i < eyesMax; i++)
        {
            // Creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;
            // Add our temp texture that we just found to the eyes List            
            eyes.Add(temp);
        }
        #endregion
        // Connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #region do this after making the function SetTexture
        // SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        #endregion
    }
    #endregion

    #region SetTexture
    // Create a function that is called SetTexture it should contain a string and int
    // the string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {
        // We need variables that exist only within this function
        // These are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        // Inside a switch statement that is swapped by the string name of our material
        #region Switch Material
        switch (type)
        {
            // Case skin
            case "Skin":
                // Index is the same as our skin index
                index = skinIndex;
                // Max is the same as our skin max
                max = skinMax;
                // Textures is our skin list .ToArray()
                textures = skin.ToArray();
                // Material index element number is 1
                matIndex = 1;
                // Break
                break;
            // Now repeat for each material 
            // Hair is 2
            case "Hair":
                // Index is the same as our index
                index = hairIndex;
                // Max is the same as our max
                max = hairMax;
                // Textures is our list .ToArray()
                textures = hair.ToArray();
                // Material index element number is 2
                matIndex = 2;
                // Break
                break;
            // Mouth is 3
            case "Mouth":
                // Index is the same as our index
                index = mouthIndex;
                // Max is the same as our max
                max = mouthMax;
                // Textures is our list .ToArray()
                textures = mouth.ToArray();
                // Material index element number is 2
                matIndex = 3;
                // Break
                break;
            // Eyes are 4
            case "Eyes":
                // Index is the same as our index
                index = eyesIndex;
                // Max is the same as our max
                max = eyesMax;
                // Textures is our list .ToArray()
                textures = eyes.ToArray();
                // Material index element number is 2
                matIndex = 4;
                // Break
                break;
        }
        #endregion
        #region OutSide Switch
        // Outside our switch statement
        // Index plus equals our direction
        index += dir;
        // Cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        // Material array is equal to our characters material list
        Material[] mat = character.materials;
        // Our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        // Our characters materials are equal to the material array
        character.materials = mat;
        // Create another switch that is goverened by the same string name of our material
        #endregion
        #region Set Material Switch
        switch (type)
        {
            //  Case skin
            case "Skin":
                //  Skin index equals our index
                skinIndex = index;
                //  Break
                break;

            //  Case hair
            case "Hair":
                //  Hair index equals our index
                hairIndex = index;
                //  Break
                break;

            //  Case mouth
            case "Mouth":
                //  Mouth index equals our index
                mouthIndex = index;
                //  Break
                break;

            //  Case eyes
            case "Eyes":
                //  Eyes index equals our index
                eyesIndex = index;
                //  Break
                break;
        }
        #endregion
    }

    #endregion

    #region Save
    // Function called Save this will allow us to save our indexes to PlayerPrefs
    void Save()
    {
        // SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        // SetString CharacterName
        PlayerPrefs.SetString("CharacterName", charName);
    }
    #endregion

    #region OnGUI
    //Function for our GUI elements
    void OnGUI()
    {
        // Create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        // Create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        #region Skin
        // GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        // GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Skin");
        // GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        // When pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        {
            SetTexture("Skin", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Hair
        // GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed the button will run SetTexture and grab the Hair Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }
        // GUI Box or Lable on the left of the screen with the contence Hair
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Hair");
        // GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        // When pressed the button will run SetTexture and grab the Hair Material and move the texture index in the direction  1
        {
            SetTexture("Hair", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Mouth
        // GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  -1
            SetTexture("Mouth", -1);
        }
        // GUI Box or Lable on the left of the screen with the contence Mouth
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Mouth");
        // GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        // When pressed the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  1
        {
            SetTexture("Mouth", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Eyes
        // GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // When pressed the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  -1
            SetTexture("Eyes", -1);
        }
        // GUI Box or Lable on the left of the screen with the contence Eyes
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Eyes");
        // GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        // When pressed the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  1
        {
            SetTexture("Eyes", 1);
        }
        // Move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Random Reset
        // Create 2 buttons one Random and one Reset
        // Random will feed a rnandom amount to the direction
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Random"))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyesMax - 1));
        }
        // Reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
        }
        // Move down the screen with the int using ++ each grouping of GUI elemetns are moved using thia
        #endregion
        #region Character Name and Save & Play
        // Name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12);
        // Move down the screen with the int using ++ each grouping GUI elements are moved using this
        i++;
        // GUI Button called Save and Play
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
        {
            // This button will run the save fuction and also load into the game level
            Save();
            SceneManager.LoadScene("Game");
        }

    }
    #endregion
    #endregion
}