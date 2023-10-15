using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityPlacer : MonoBehaviour
{
    private int numOfSides = 8;

    [SerializeField]
    private List<GameObject> cityPrefabs;

    [SerializeField]
    private List<GameObject> buildings;

    [SerializeField]
    private float scaleWidth, scaleHeight, distance;

    public float yOffset = -230f; // Y-coordinate offset for the background


    [ContextMenu("CreateCity")]
    private void CreateCity()
    {
        foreach (var building in buildings)
        {
            DestroyImmediate(building);
        }

        buildings = new List<GameObject>(); 

        int prefabIndex = 0;
        float angle = 0f;

        for (int i = 0; i < numOfSides; i++) 
        {
            buildings.Add(Instantiate(cityPrefabs[prefabIndex], transform));

            buildings[i].transform.Rotate(new Vector3(0f, angle, 0f));

            angle += 360f/numOfSides;

            prefabIndex++;
            if (prefabIndex >= cityPrefabs.Count)
            {
                prefabIndex = 0;
            }

        }
    }


    [ContextMenu("SetCityScale")]
    private void SetCityScale()
    {
        Debug.Log("SetCityScale");
        foreach (var b in buildings)
        {
            b.transform.localScale = new Vector3(scaleWidth, scaleHeight, 1f);
            float spriteLength = buildings[0].GetComponent<SpriteRenderer>().bounds.size.x;
            distance = spriteLength / 2f + (Mathf.Sqrt(2) / 2) * spriteLength;
            
            b.transform.position = new Vector3(b.transform.position.x, yOffset, b.transform.position.z);
            // b.transform.position = distance * b.transform.forward;

        }
    }



    private void OnValidate()
    {
        if (buildings.Count == numOfSides) {
            SetCityScale();
        }
    }
}
