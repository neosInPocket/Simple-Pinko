using System.IO;
using UnityEngine;

public class DataPreferences : MonoBehaviour
{
    [SerializeField] private bool clearOptions;
    [SerializeField] private Preferences defaultPreferences;
    private static string path => Application.persistentDataPath + "/DataPreferences.json";
    public static Preferences Preferences { get; private set; }

    private void Awake()
    {
        if (clearOptions)
        {
            ResetPreferences();
        }
        else
        {
            GetSettings();
        }
    }

    public void ResetPreferences()
    {
        Preferences = defaultPreferences;
        SavePreferences();
    }

    public static void SavePreferences()
    {
        if (!File.Exists(path))
        {
            NewSaveFile();
        }
        else
        {
            WriteDiskPreferences();
        }
    }

    private static void GetSettings()
    {
        if (!File.Exists(path))
        {
            NewSaveFile();
        }
        else
        {
            string text = File.ReadAllText(path);
            Preferences = JsonUtility.FromJson<Preferences>(text);
        }
    }

    private static void NewSaveFile()
    {
        Preferences = new Preferences();
        File.WriteAllText(path, JsonUtility.ToJson(Preferences));
    }

    private static void WriteDiskPreferences()
    {
        File.WriteAllText(path, JsonUtility.ToJson(Preferences));
    }
}
