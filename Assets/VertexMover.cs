﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexMover : MonoBehaviour
{
    public MeshFilter mesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.LogError("Vertex " + i + " " + vertices[i].x + " " + vertices[i].y + " " + vertices[i].z);
        }
    }
}
