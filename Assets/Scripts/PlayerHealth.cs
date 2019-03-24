using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Text HealthCounter;
    public Slider HealthSlider;
    private PlayerAudioManager audioManager;

    public override void Awake()
    {
        base.Awake();
        this.audioManager = GetComponent<PlayerAudioManager>();
        UpdateHealthUI();
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        this.audioManager.PlayOw();
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float value = (float) this.CurrentHealth / (float) this.MaxHealth;
        value *= this.HealthSlider.maxValue;
        this.HealthSlider.value = value;
        this.HealthCounter.text = this.CurrentHealth.ToString();
    }
}