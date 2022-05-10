using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LevelData
{
    public Vector3 startRotation;
    public List<TriangleData> triangleDatas;

    public LevelData(
        Vector3 startRotation,
        List<TriangleData> triangleDatas)
    {
        this.startRotation = startRotation;
        this.triangleDatas = triangleDatas;
    }
}
public class TriangleData
{
    public Vector3 a, b, c;
    public float aMoveCoef, bMoveCoef, cMoveCoef;
    public Color color;

    public TriangleData(
        float ax, float ay, float az,
        float bx, float by, float bz,
        float cx, float cy, float cz,
        float colorR, float colorG, float colorB,
        float aMoveCoef, float bMoveCoef, float cMoveCoef)
    {
        a.x = ax;
        a.y = ay;
        a.z = az;
        b.x = bx;
        b.y = by;
        b.z = bz;
        c.x = cx;
        c.y = cy;
        c.z = cz;
        color = new Color(colorR, colorG, colorB);
        this.aMoveCoef = aMoveCoef;
        this.bMoveCoef = bMoveCoef;
        this.cMoveCoef = cMoveCoef;
    }
}
public class DataReader
{
    // Start is called before the first frame update
    public static LevelData ReadLevel()
    {
        string fileName = "level" + LevelsButtonsManager.levelNumber;
        TextAsset mytxtData = (TextAsset)Resources.Load(fileName);
        string text = mytxtData.text;
        string[] lines = text.Split("\n");
        return new LevelData(
            parseFirstLine(lines[0]),
            parseTriangles(lines));
    }

    static Vector3 parseFirstLine(string firstLine)
    {
        string[] startRotationStrings = firstLine.Split(",");
        return new Vector3(
            float.Parse(startRotationStrings[0], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(startRotationStrings[1], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(startRotationStrings[2], CultureInfo.InvariantCulture.NumberFormat));
    }

    static List<TriangleData> parseTriangles(string[] lines)
    {
        List<TriangleData> triangleDatas = new List<TriangleData>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            Debug.Log(line);
            string[] lineComponents = line.Split(",");
            foreach (var n in lineComponents)
            {
                Debug.Log(n);
                float.Parse(n, CultureInfo.InvariantCulture.NumberFormat);
            }
            TriangleData data = new TriangleData(
                float.Parse(lineComponents[0], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[1], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[2], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[3], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[4], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[5], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[6], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[7], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[8], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[9], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[10], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[11], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[12], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[13], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(lineComponents[14], CultureInfo.InvariantCulture.NumberFormat));
            triangleDatas.Add(data);
        }
        return triangleDatas;
    }
}
