using UnityEngine;

// Attach this to every food cube in the scene
public class FoodObject : MonoBehaviour
{
    public FoodItem data;   // drag your FoodItem asset here

    // Colour the cube to match the food when the scene loads
    void Start()
    {
        if (data == null) return;
        var rend = GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = data.itemColor;
    }
}