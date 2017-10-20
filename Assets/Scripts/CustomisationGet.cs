using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CustomisationGet : MonoBehaviour
{
    public Renderer character;

    void Start()
    {
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        LoadTexture();
    }

    public void LoadTexture()
    {
        if (!PlayerPrefs.HasKey("CharacterName"))
        {
            SceneManager.LoadScene(1);
        }
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        gameObject.name = PlayerPrefs.GetString("CharacterName");
    }
    public void SetTexture(string type, int dir)
    {
        Texture2D tex = null;
        int matIndex = 0;
        switch (type)
        {
            case "Skin":
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                matIndex = 1;
                break;

            case "Hair":
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                matIndex = 2;
                break;

            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                matIndex = 3;
                break;

            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                matIndex = 4;
                break;

            case "Armour":
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                matIndex = 5;
                break;

            case "Clothes":
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                matIndex = 6;
                break;
        }
        Material[] mats = character.materials;
        mats[matIndex].mainTexture = tex;
        character.materials = mats;
    }
}
