using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CircleMesh : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider; // COLLIDER

    public float radius;
    public int corners;
    [SerializeField] bool isUpdating;

    void Start()
    {
        mesh = new Mesh();

        meshCollider = GetComponent<MeshCollider>(); // COLLIDER
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        meshCollider.sharedMesh = mesh; // COLLIDER
    }

    void Update()
    {
        if (isUpdating) UpdateShape();
    }



    private void CreateShape()
    {
        mesh.Clear();

        UpdateShape();

        mesh.RecalculateNormals();
    }

    private void UpdateShape()
    {
        mesh.vertices = GetVertices(corners, radius);
        mesh.triangles = GetTriangles(corners);

        meshCollider.sharedMesh = mesh; // COLLIDER
    }

    int[] GetTriangles(int corners)
    {
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (corners - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        trianglesList.Reverse();
        int[] triangles = trianglesList.ToArray();
        return triangles;
    }


    Vector3[] GetVertices(int corners, float radius)
    {
        Vector3[] vertices = new Vector3[corners];
        for (int currentStep = 0; currentStep < corners; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / corners;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 vortex = new Vector3(x, 0, y);

            vertices[currentStep] = vortex;
        }
        return vertices;
    }
}
