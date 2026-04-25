using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float budget;
    public float spent;
    public float totalNutrition;
    public List<string> cart = new List<string>();

    void Awake()
    {
        // Singleton — one GameManager lives across all scenes
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load budget chosen on Main Menu
        budget = PlayerPrefs.GetFloat("Budget", 20f);
    }

    // Returns true if item was added, false if can't afford
    public bool TryBuy(FoodItem item)
    {
        if (item.price > budget - spent) return false;
        spent         += item.price;
        totalNutrition += item.NutritionScore;
        cart.Add(item.foodName);
        return true;
    }

    public float RemainingBudget => budget - spent;
}