using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI cartText;
    public TextMeshProUGUI infoText;    // shows selected food details

    [Header("Info Panel")]
    public GameObject      infoPanel;  // the popup panel
    public TextMeshProUGUI infoPanelText;

    Camera _cam;
    FoodObject _selected;

    void Start()
    {
        _cam = Camera.main;
        infoPanel.SetActive(false);
        RefreshHUD();
    }

    void Update()
    {
        DetectClick();
    }

    void DetectClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // Cast a ray from camera through mouse position
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, 20f)) return;

        var food = hit.collider.GetComponent<FoodObject>();
        if (food == null) return;

        _selected = food;
        ShowInfoPanel(food.data);
    }

    void ShowInfoPanel(FoodItem item)
    {
        infoPanel.SetActive(true);
        infoPanelText.text =
            $"<b>{item.foodName}</b>\n\n" +
            $"Price:     ${item.price:F2}\n" +
            $"Protein:   {item.protein}\n" +
            $"Vitamins:  {item.vitamins}\n" +
            $"Fiber:     {item.fiber}\n" +
            $"Nutrition: {item.NutritionScore:F0} pts\n\n" +
            $"Tap ADD to buy";
    }

    // Called by the ADD button
    public void OnAddButton()
    {
        if (_selected == null) return;

        bool success = GameManager.Instance.TryBuy(_selected.data);
        if (success)
        {
            infoPanelText.text = _selected.data.foodName + " added!";
            RefreshHUD();
        }
        else
        {
            infoPanelText.text = "Not enough budget!";
        }
    }

    // Called by the CLOSE button on the info panel
    public void OnClosePanel()
    {
        infoPanel.SetActive(false);
        _selected = null;
    }

    // Called by the CHECKOUT button
    public void OnCheckout()
    {
        SceneManager.LoadScene("ResultsScene");
    }

    void RefreshHUD()
    {
        budgetText.text = $"Budget: ${GameManager.Instance.RemainingBudget:F2}";
        cartText.text   = $"Items: {GameManager.Instance.cart.Count}";
    }
}