/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - To create the background image that is displayed on the tablet
 *   when trying to turn off the alarm (woman image)
 * 
 * Attached to objects in game scene:
 * - Tablet that has the alarm playing
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AT_background_controller : MonoBehaviour
{
    // material with the 2D image texture (i.e. picture of woman for ipad background)
    public Material backgroundMaterial;

    // GameObject representing the background (object that displays the picture of woman)
    public GameObject backgroundObject; 

    /*
     * Start will be called before first frame update
     * 
     * We will create the background if not created yet and display it on the tablet
     */
    void Start()
    {
        // Check if the background object and material are assigned
        if (backgroundObject == null || backgroundMaterial == null)
        {
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

        // Creat the background mesh needed for our case
        backgroundMesh = CreateCube();

        // Attach the background material to the renderer
        backgroundRenderer.material = backgroundMaterial;

        // Assign the created mesh to the background object
        backgroundObject.GetComponent<MeshFilter>().mesh = backgroundMesh;
    }

    /*
     * Create a cube mesh with large dimensions
     *
     * Designed to house the background image.
     */
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
}

