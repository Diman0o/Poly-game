using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TriangleData
{
    public Vector3 a, b, c;
    public float aMoveCoef, bMoveCoef, cMoveCoef;
    public Color color;

    public TriangleData(
        float ax, float ay, float az,
        float bx, float by, float bz,
        float cx, float cy, float cz,
        float aMoveCoef, float bMoveCoef,
        float cMoveCoef, float colorR, float colorG, float colorB)
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
        this.aMoveCoef = aMoveCoef;
        this.bMoveCoef = bMoveCoef;
        this.cMoveCoef = cMoveCoef;
        color = new Color(colorR, colorG, colorB);

    }
}
public class DataReader
{
    // Start is called before the first frame update
    public static List<TriangleData> ReadLevel()
    {
        List <TriangleData> triangeDatas = new List<TriangleData>();
        string fileName = "level" + LevelsButtonsManager.levelNumber;
        Debug.Log(fileName);
        TextAsset mytxtData = (TextAsset)Resources.Load(fileName);
        string text = mytxtData.text;
        string[] lines = text.Split("\n");
        foreach (string line in lines) {
            string[] lineComponents = line.Split(",");
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
            triangeDatas.Add(data);
        }
        return triangeDatas;
    }
}
