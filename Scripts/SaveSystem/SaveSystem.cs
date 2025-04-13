using UnityEngine;
using System.Collections.Generic;
using System.IO;



public static class SaveSystem{
    private static string file;
    private static bool loaded;
    private static DataState data;
   // public static  string json  = "";
    public static void Initialize(string fileName)
    {
        if (!loaded)
        {
            file = fileName;
            if (File.Exists(GetPath())) Load();
            else data = new DataState();
            loaded = true;
        }
    }

    static string GetPath()
    {
        return Application.persistentDataPath + "/" + file;
    }

    static void Load()
    {
        // Bước 1: Đọc toàn bộ nội dung của tệp JSON từ đường dẫn trả về bởi phương thức GetPath()
        string json = File.ReadAllText(GetPath());

        // Bước 2: Chuyển đổi nội dung JSON thành đối tượng DataState
        data = JsonUtility.FromJson<DataState>(json);

        // Bước 3: Hiển thị thông báo log để xác nhận đã tải dữ liệu thành công
        Debug.Log("[SaveGame] --> Loaded save file: " + GetPath());
    }

   public   static void SaveToDisk()  {
        if (data.items.Count == 0)
        {
            Debug.LogWarning("[SaveGame] Warning: Data list is empty, nothing to save!");
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log("[SaveGame] --> Save game data: " + json);
    }

    static void ReplaceItem(string name, string item)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name)
            {
                entry.Value = item;
                SaveToDisk();
                return;
            }
        }
        data.items.Add(new SaveData(name, item));
        SaveToDisk();
    }

    public static bool HasKey(string name)
    {
        return data.items.Exists(x => x.Key == name);
    }

    public static void SetInt(string name, int val)
    {
        ReplaceItem(name, val.ToString());
    }
    public static int GetInt(string name, int defaultValue = 0)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name) return int.Parse(entry.Value);
        }
        return defaultValue;
    }

    public static void SetFloat(string name, float val)
    {
        ReplaceItem(name, val.ToString());
    }
    public static float GetFloat(string name, float defaultValue = 0f)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name) return float.Parse(entry.Value);
        }
        return defaultValue;
    }

    public static void SetString(string name, string val)
    {
        ReplaceItem(name, val);
    }
    public static string GetString(string name, string defaultValue = "")
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name) return entry.Value;
        }
        return defaultValue;
    }

    public static void SetBool(string name, bool val)
    {
        ReplaceItem(name, val ? "1" : "0");
    }
    public static bool GetBool(string name, bool defaultValue = false)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name) return entry.Value == "1";
        }
        return defaultValue;
    }

    public static void SetVector2(string name, Vector2 val)
    {
        ReplaceItem(name, val.x + "," + val.y);
    }
    public static Vector2 GetVector2(string name, Vector2 defaultValue = default)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name)
            {
                string[] parts = entry.Value.Split(',');
                return new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
            }
        }
        return defaultValue;
    }

    public static void SetVector3(string name, Vector3 val)
    {
        ReplaceItem(name, val.x + "," + val.y + "," + val.z);
    }
    public static Vector3 GetVector3(string name, Vector3 defaultValue = default)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name)
            {
                string[] parts = entry.Value.Split(',');
                return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
            }
        }
        return defaultValue;
    }

    public static void SetColor(string name, Color val)
    {
        ReplaceItem(name, val.r + "," + val.g + "," + val.b + "," + val.a);
    }
    public static Color GetColor(string name, Color defaultValue = default)
    {
        foreach (var entry in data.items)
        {
            if (entry.Key == name)
            {
                string[] parts = entry.Value.Split(',');
                return new Color(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
            }
        }
        return defaultValue;
    }
}
