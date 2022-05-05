using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Color myColor;
    public Vector3 v0;
    public Vector3 v1;
    public Vector3 v2;
    public static Mesh Triangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
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

    public static void CreateTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Color triangleColor)
    {

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        //gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject gameObjectBack = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        //gameObjectBack.transform.localScale = new Vector3(1, 1, 1);

        gameObject.GetComponent<MeshFilter>().mesh = Triangle(vertex0, vertex1, vertex2);
        gameObject.GetComponent<MeshRenderer>().material.color = triangleColor;
        gameObjectBack.GetComponent<MeshFilter>().mesh = Triangle(vertex2, vertex1, vertex0);
        gameObjectBack.GetComponent<MeshRenderer>().material.color = triangleColor;
    }
    // Start is called before the first frame update
    void Start()
    {

        v0 = new Vector3(-1, -1, 0);
        v1 = new Vector3(0, 1, 0);
        v2 = new Vector3(1, -1, 2);

        myColor = new Color(0.3f, 0.5f, 0);

        CreateTriangle(v0, v1, v2, myColor);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
