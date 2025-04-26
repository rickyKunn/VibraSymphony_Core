using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    public async void WriteCsv(List<string> Devicedata, string DelayData)
    {
        string filePath;

        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Path.Combine(Application.persistentDataPath, "PreData.csv");
        }
        else
        {
            filePath = Path.Combine(Application.dataPath, "PreData.csv");
        }
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            await writer.WriteLineAsync(string.Join(",", Devicedata));
            await writer.WriteLineAsync(DelayData);
        }

    }
}
