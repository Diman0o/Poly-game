#if UNITY_EDITOR
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
                        string[] components = s.Split();
                        string x1 = components[0];
                        string y1 = components[1];
                        string x2 = components[2];
                        string y2 = components[3];
                        string x3 = components[4];
                        string y3 = components[5];
                        string r = components[6];
                        string g = components[7];
                        string b = components[8];
                        float commonMoveCoef = Random.Range(-0.3f, 0.3f);
                        string[] moveCoefs = new string[] {
                            Random.Range(commonMoveCoef - 0.1f, commonMoveCoef + 0.1f).ToString(System.Globalization.CultureInfo.InvariantCulture),
                            Random.Range(commonMoveCoef - 0.1f, commonMoveCoef + 0.1f).ToString(System.Globalization.CultureInfo.InvariantCulture),
                            Random.Range(commonMoveCoef - 0.1f, commonMoveCoef + 0.1f).ToString(System.Globalization.CultureInfo.InvariantCulture)
                        };
                        lines.Add(
                            x1 + "," +
                            y1 + "," +
                            x2 + "," +
                            y2 + "," +
                            x3 + "," +
                            y3 + "," +
                            (float.Parse(r) / 255f).ToString() + "," +
                            (float.Parse(g) / 255f).ToString() + "," +
                            (float.Parse(b) / 255f).ToString() + "," +
                            string.Join(",", moveCoefs)
                        );
                    }
                }
                await File.WriteAllLinesAsync("Assets/Resources/" + filename, lines);
                Debug.Log($"Randomized level {filename}");
            }
        }
    }
}
#endif //UNITY_EDITOR
