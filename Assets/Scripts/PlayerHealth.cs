using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Slider HealthSlider;
    private PlayerAudioManager audioManager;

    public override void Awake()
    {
        base.Awake();
        this.audioManager = GetComponent<PlayerAudioManager>();
        UpdateHealthSlider();
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        this.audioManager.PlayOw();
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        float value = (float) this.CurrentHealth / (float) this.MaxHealth;
        value *= this.HealthSlider.maxValue;
        this.HealthSlider.value = value;
    }
}