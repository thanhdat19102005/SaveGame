using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSaveGame : MonoBehaviour
{
    //INT (UI)
    [Header("Save int")]
    public Text countIntText;
    public InputField inputIntField;
    public int cubeIntCount = 0;
    [Space(10)]

    //Next variables
    [Header("Save next")]
    public float floatCount;
    public Vector2 vect2;
    public Vector3 vect3;
    public Color color;
    public string stringSave;
    public bool saveBool;

    // Use this for initialization
    private void Start()
    {
        SaveSystem.Initialize("saveData.json");

        // Load Save int
        cubeIntCount = SaveSystem.GetInt("cubeCount", 0);
        countIntText.text = cubeIntCount.ToString();

        // Load save Next
        floatCount = SaveSystem.GetFloat("float", 0f);
        saveBool = SaveSystem.GetBool("bool", false);
        vect2 = SaveSystem.GetVector2("vect2", Vector2.zero);
        vect3 = SaveSystem.GetVector3("vect3", Vector3.zero);
        color = SaveSystem.GetColor("color", Color.white);
        stringSave = SaveSystem.GetString("string", "");
    }

    // Button Save INT
    public void SaveCube()
    {
        countIntText.text = inputIntField.text;
        cubeIntCount = int.Parse(inputIntField.text);
        SaveSystem.SetInt("cubeCount", cubeIntCount);
        SaveSystem.SaveToDisk();
    }

    // Save "NEXT"
    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    public   void SaveData()
    {
        SaveSystem.SetFloat("float", floatCount);
        SaveSystem.SetBool("bool", saveBool);
        SaveSystem.SetVector2("vect2", vect2);
        SaveSystem.SetVector3("vect3", vect3);
        SaveSystem.SetColor("color", color);
        SaveSystem.SetString("string", stringSave);
        SaveSystem.SaveToDisk();
    }
}
