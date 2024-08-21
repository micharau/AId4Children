using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    // Dictionary to hold the asset names and their corresponding activation states
    private Dictionary<string, bool> assetActivation = new Dictionary<string, bool>();

    // List of asset GameObjects to manage
    public List<GameObject> assets;

    // Path to the CSV file
    public string relativeCsvPath = "/Scenarios/Fall Leon.csv";

    void Start()
    {
        // Create the full path to the CSV file
        string csvFilePath = Path.Combine(Application.dataPath, relativeCsvPath);
        LoadScenarioFromCSV(csvFilePath);
        ManageAssets();
    }

    void LoadScenarioFromCSV(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("CSV file not found at: " + path);
            return;
        }

        string[] lines = File.ReadAllLines(path);

        // Loop through each line (row) and read the second (asset name) and third (activation value) columns
        foreach (string line in lines)
        {
            string[] columns = line.Split(',');

            // Ensure the current row has at least three columns
            if (columns.Length < 3)
            {
                Debug.LogError("CSV row does not contain enough columns.");
                continue;
            }
            
            string assetName = columns[1].Trim();
            string activationValue = columns[2].Trim();

            // Parse activation value to a boolean
            bool isActive = activationValue == "1";
            assetActivation[assetName] = isActive;
        }
    }

    void ManageAssets()
    {
        foreach (var asset in assets)
        {
            if (assetActivation.TryGetValue(asset.name, out bool isActive))
            {
                asset.SetActive(isActive);
            }
            else
            {
                Debug.LogWarning("Asset with name " + asset.name + " not found in CSV.");
            }
        }
    }
}
