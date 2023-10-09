using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



// Heatmap show up but covers original image
public class HeatMapGenerator : MonoBehaviour
{
    // public MouseTracker mouseTracker;
    public Image heatmapImage; // UI Image for displaying the heat map.

    public Gradient heatmapGradient; // Gradient to map mouse position density to colors.

    public float unitSize = 10.0f; // Adjust this to control the size of the units for intensity calculation.
    
    public Transform cam;
    // public void GenerateHeatMap()

    public Camera camera1;


    public void Start()

    {

    }

    public void show_heat_map()
    {
        // Get the mouse positions recorded during tracking.
        List<Vector2> mousePositions = MouseTracker.GetMousePositions();
        //Debug.Log(mousePositions[4]);


        // Calculate the number of units in both x and y directions.
        int unitsX = Mathf.CeilToInt(heatmapImage.rectTransform.rect.width / unitSize);
        int unitsY = Mathf.CeilToInt(heatmapImage.rectTransform.rect.height / unitSize);
        // Debug.Log(heatmapImage.rectTransform.rect.width);
        // Debug.Log(heatmapImage.rectTransform.rect.height);

        // Debug.Log(unitsX);
        // Debug.Log(unitsY);


        // Create a texture for the heat map.
        Texture2D heatMapTexture = new Texture2D(unitsX, unitsY, TextureFormat.RGBAFloat, false);
        heatMapTexture.filterMode = FilterMode.Bilinear;

        // Create an array to store the density for each unit.
        float[,] unitDensity = new float[unitsX, unitsY];

        // Calculate the maximum density for normalization.
        float maxDensity = 0.0f;

        foreach (Vector2 mousePos in mousePositions)
        {
            // Convert the mouse position to local coordinates relative to the heat map image.
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(heatmapImage.rectTransform, mousePos, camera1, out localPos);
            // localPos=cam.InverseTransformPoint(mousePos);
            // localPos = mousePos;
            // Calculate the unit index for this mouse position.
            int unitX = Mathf.FloorToInt(localPos.x / unitSize);
            int unitY = Mathf.FloorToInt(localPos.y / unitSize);
            // Debug.Log("x");
            // Debug.Log(unitX);
            // Debug.Log("y");
            // Debug.Log(unitY);
            // Increment the density for the corresponding unit.
            if (unitX >= 0 && unitX < unitsX && unitY >= 0 && unitY < unitsY)
            {
                unitDensity[unitX, unitY] += 1.0f;
                maxDensity = Mathf.Max(maxDensity, unitDensity[unitX, unitY]);
            }
        }

        // Normalize the density values and set the pixel color on the heat map texture.
        for (int x = 0; x < unitsX; x++)
        {
            for (int y = 0; y < unitsY; y++)
            {
                float normalizedDensity = unitDensity[x, y] / maxDensity;
                float intensity = normalizedDensity;
                // Debug.Log(intensity);

                //Debug.Log(intensity);
                Color color = heatmapGradient.Evaluate(intensity);
                color.a =  0.5f;
                

                heatMapTexture.SetPixel(x, y, color);
                // Debug.Log(x);
                // // Debug.Log(intensity);
                // Debug.Log(y);
                // Debug.Log(color);



            }
        }

        // Apply changes to the texture.

        heatMapTexture.Apply();

        // Assign the heat map texture to the UI Image.
        // heatmapImage.sprite = Sprite.Create(heatMapTexture, new Rect(0, 0, unitsX, unitsY), Vector2.zero);
        heatmapImage.sprite = Sprite.Create(heatMapTexture, new Rect(0, 0, heatMapTexture.width, heatMapTexture.height), Vector2.zero);
        Debug.Log("done");
    }
}
