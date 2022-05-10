using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class LevelRandomizer
{

    [MenuItem("Tools/Randomize Levels")]
    public static async void Execute()
    {

        DirectoryInfo dir = new DirectoryInfo("Assets/LevelTemplates");
        FileInfo[] info = dir.GetFiles("*.csv");
        foreach (FileInfo fi in info)
        {
            string filename = fi.Name;
            if (!File.Exists("Assets/Resources/" + filename)) {
                List<string> lines = new List<string>();
                string[] startRotation = new string[] {
                    180f.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    Random.Range(-90f, 90f).ToString(System.Globalization.CultureInfo.InvariantCulture),
                    Random.Range(-90f, 90f).ToString(System.Globalization.CultureInfo.InvariantCulture)};
                lines.Add(string.Join(",", startRotation));
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        float commonMoveCoef = Random.Range(-0.3f, 0.3f);
                        string[] moveCoefs = new string[] {
                            Random.Range(commonMoveCoef - 0.1f, commonMoveCoef + 0.1f).ToString(System.Globalization.CultureInfo.InvariantCulture),
                            Random.Range(commonMoveCoef - 0.1f, commonMoveCoef + 0.1f).ToString(System.Globalization.CultureInfo.InvariantCulture),
                            Random.Range(commonMoveCoef - 0.1f, commonMoveCoef + 0.1f).ToString(System.Globalization.CultureInfo.InvariantCulture)
                        };
                        lines.Add(s + "," + string.Join(",", moveCoefs));
                    }
                }
                await File.WriteAllLinesAsync("Assets/Resources/" + filename, lines);
                Debug.Log($"Randomized level {filename}");
            }
        }
    }
}
