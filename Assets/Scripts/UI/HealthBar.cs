using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health healthComponent;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Gradient healthGradient;

    [Header("Animation")]
    [SerializeField] private float smoothSpeed = 5f;

    private float targetFillAmount;

    void Start()
    {
        // Automatically try to find the Health component on the parent (the Enemy)
        if (healthComponent == null)
            healthComponent = GetComponentInParent<Health>();

        if (healthComponent != null)
        {
            // Listen for health changes and set the initial UI state
            healthComponent.OnHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar(healthComponent.CurrentHealth);
        }
    }

    void Update()
    {
        if (fillImage != null)
        {
            // Smoothly slide the health bar fill
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, Time.deltaTime * smoothSpeed);
        }
    }

    private void UpdateHealthBar(float currentHealth)
    {
        // Calculate the percentage (0.0 to 1.0)
        float pct = currentHealth / healthComponent.MaxHealth;
        targetFillAmount = pct;

        // Change the color based on the gradient (e.g., Green to Red)
        if (fillImage != null) 
            fillImage.color = healthGradient.Evaluate(pct);

        // Update the text - Fixed the multi-line string error here
        if (healthText != null)
        {
            healthText.text = $"{Mathf.CeilToInt(currentHealth)} / {Mathf.CeilToInt(healthComponent.MaxHealth)}";
        }
    }
}
