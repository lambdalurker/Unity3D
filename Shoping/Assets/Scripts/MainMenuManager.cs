using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider budgetSlider;
    public TextMeshProUGUI budgetLabel;
    public Button playButton;

    void Start()
    {
        // Update label whenever slider moves
        budgetSlider.onValueChanged.AddListener(OnSliderChanged);

        // Set initial label
        OnSliderChanged(budgetSlider.value);

        // When Play is clicked, save budget and load the shop
        playButton.onClick.AddListener(OnPlayClicked);
    }

    void OnSliderChanged(float value)
    {
        budgetLabel.text = "Starting Budget: $" + value.ToString("F0");
    }

    void OnPlayClicked()
    {
        // Save the chosen budget so other scenes can read it
        PlayerPrefs.SetFloat("Budget", budgetSlider.value);
        SceneManager.LoadScene("ShopScene");
    }
}