using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string Key;
    public string Value;

    public SaveData() { }

    public SaveData(string key, string value)
    {
        this.Key = key;
        this.Value = value;
    }
}

[System.Serializable]
public class DataState
{
    public List<SaveData> items = new List<SaveData>();

    public DataState() { }

    public void AddItem(SaveData item)
    {
        items.Add(item);
    }
}

public class SerializatorJson
{

    // Lưu dữ liệu dưới dạng JSON
    public static void SaveJson(DataState state, string dataPath)
    {
        string json = JsonUtility.ToJson(state, true);
        File.WriteAllText(dataPath, json);
        Debug.Log("[SaveGame] Saved data to JSON: " + json);
    }

    // Tải dữ liệu từ file JSON
    public static DataState LoadJson(string dataPath)
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            DataState state = JsonUtility.FromJson<DataState>(json);
            Debug.Log("[SaveGame] Loaded data from JSON: " + json);
            return state;
        }
        else
        {
            Debug.LogWarning("[SaveGame] File not found. Returning empty data.");
            return new DataState();
        }
    }
}