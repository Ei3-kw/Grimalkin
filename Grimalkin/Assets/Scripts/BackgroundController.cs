using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Material backgroundMaterial; // Material with the 2D image texture
    public GameObject backgroundObject; // GameObject representing the background

    void Start()
    {
        // Check if the background object and material are assigned
        if (backgroundObject == null || backgroundMaterial == null)
        {
            Debug.LogError("Background Object or Material is not assigned!");
            return;
        }

        // Create a new GameObject for the background if it's not assigned
        if (backgroundObject == null)
        {
            backgroundObject = new GameObject("Background");
        }

        // Attach a MeshRenderer to the background GameObject
        MeshRenderer backgroundRenderer = backgroundObject.GetComponent<MeshRenderer>();
        if (backgroundRenderer == null)
        {
            backgroundRenderer = backgroundObject.AddComponent<MeshRenderer>();
        }

        // Create a new Mesh with large dimensions (e.g., cube or sphere)
        Mesh backgroundMesh = new Mesh();

        // Customize the mesh here (e.g., create a large cube or sphere)
        // For a cube:
        backgroundMesh = CreateCube();
        // For a sphere:
        // backgroundMesh = CreateSphere();

        // Attach the background material to the renderer
        backgroundRenderer.material = backgroundMaterial;

        // Assign the created mesh to the background object
        backgroundObject.GetComponent<MeshFilter>().mesh = backgroundMesh;
    }

    // Create a cube mesh with large dimensions
    Mesh CreateCube()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = {
            new Vector3(-1000f, -1000f, -1000f),
            new Vector3(-1000f, -1000f, 1000f),
            new Vector3(1000f, -1000f, 1000f),
            new Vector3(1000f, -1000f, -1000f)
        };

        int[] triangles = {
            0, 1, 2,
            0, 2, 3
        };

        Vector2[] uv = {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        return mesh;
    }

    // Create a sphere mesh with large dimensions
    Mesh CreateSphere()
    {
        Mesh mesh = new Mesh();
        // Create a sphere mesh here
        return mesh;
    }
}

