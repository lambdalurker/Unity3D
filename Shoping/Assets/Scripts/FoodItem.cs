using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "SmartShop/Food Item")]
public class FoodItem : ScriptableObject
{
    public string foodName;
    public float  price;
    public int    protein;
    public int    vitamins;
    public int    fiber;
    public Color  itemColor = Color.white;

    // Nutrition score out of 100
    public float NutritionScore =>
        protein * 0.4f + vitamins * 0.3f + fiber * 0.3f;
}