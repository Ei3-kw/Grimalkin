/* 
 * Project Grimalkin
 * Author: Timothy Ryall
 * 
 * Purpose:
 * - 
 * 
 * Attached to objects in game scene:
 * - 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeatMapGenerator : MonoBehaviour
{
    // Image of the heap map that is to be overlayed on tablet screen
    // to show the user where they were looking
    public Image heatmapImage;

    // Gradient to map mouse position density to colors.
    public Gradient heatmapGradient;

    // Adjust this to control the size of the units for intensity calculation.
    public float unitSize = 10.0f; 
    
    public Transform cam;

    public Camera camera1;




    public void show_heat_map()
    {
        // Get the mouse positions recorded during tracking.
        List<Vector2> mousePositions = MouseTracker.GetMousePositions();



        // Calculate the number of units in both x and y directions.
        int unitsX = Mathf.CeilToInt(heatmapImage.rectTransform.rect.width / unitSize);
        int unitsY = Mathf.CeilToInt(heatmapImage.rectTransform.rect.height / unitSize);


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


            // Calculate the unit index for this mouse position.
            int unitX = Mathf.FloorToInt(localPos.x / unitSize);
            int unitY = Mathf.FloorToInt(localPos.y / unitSize);


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

                Color color = heatmapGradient.Evaluate(intensity);
                color.a =  0.5f;
                

                heatMapTexture.SetPixel(x, y, color);
            }
        }

        // Apply changes to the texture.

        heatMapTexture.Apply();

        // Assign the heat map texture to the UI Image.
        // heatmapImage.sprite = Sprite.Create(heatMapTexture, new Rect(0, 0, unitsX, unitsY), Vector2.zero);
        heatmapImage.sprite = Sprite.Create(heatMapTexture, new Rect(0, 0, heatMapTexture.width, heatMapTexture.height), Vector2.zero);
    }
}
