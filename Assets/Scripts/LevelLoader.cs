using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static Vector3 startRotation;
    private static Vector3 targetPoint;
    public static Mesh CreateTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        var normal = Vector3.Cross((vertex1 - vertex0), (vertex2 - vertex0)).normalized;
        var mesh = new Mesh
        {
            vertices = new [] {vertex0, vertex1, vertex2},
            normals = new [] {normal, normal, normal},
            uv = new [] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)},
            triangles = new [] {0, 1, 2}
        };

        return mesh;
    }

    public static void DrawTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Color triangleColor)
    {

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        GameObject gameObjectBack = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        gameObject.GetComponent<MeshFilter>().mesh = CreateTriangle(vertex0, vertex1, vertex2);
        //gameObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Specular")); //Set default shader to used material
        gameObject.GetComponent<MeshRenderer>().material.color = triangleColor;
        //gameObjectBack.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Specular")); //Set default shader to used material
        gameObjectBack.GetComponent<MeshFilter>().mesh = CreateTriangle(vertex2, vertex1, vertex0);
        gameObjectBack.GetComponent<MeshRenderer>().material.color = triangleColor;
    }

    public static Vector3 TransformPoint(Vector3 vertex, float moveCoef)
    {
        Vector3 transformedVertex = vertex + ((vertex - targetPoint) * moveCoef);
        return transformedVertex;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = new Vector3(0, 0, CameraRotationAround.zoom);

        LevelData levelData = DataReader.ReadLevel();
        startRotation = levelData.startRotation;
        List<TriangleData> triangleDatas = levelData.triangleDatas;

        foreach (var triangleData in triangleDatas)
        {
            DrawTriangle(
                TransformPoint(triangleData.a, triangleData.aMoveCoef),
                TransformPoint(triangleData.b, triangleData.bMoveCoef),
                TransformPoint(triangleData.c, triangleData.cMoveCoef),
                triangleData.color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
