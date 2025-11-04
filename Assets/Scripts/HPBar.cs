using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider slider;
    private Character target;
    public void Bind(Character character)
    {
        if (target != null) Unbind();

        target = character;
        slider.minValue = 0;
        slider.maxValue = target.MaxHealth;
        slider.value = target.Health;
        Debug.Log($"[HPBar] Bind {target.name} -> {slider.value}/{slider.maxValue}");

        target.OnHealthChanged += OnHealthChanged;
    }
    public void Unbind()
    {
        if (target == null) return;
        target.OnHealthChanged -= OnHealthChanged;
        target = null;
    }
    private void OnDestroy() => Unbind();

    private void OnHealthChanged(int current, int max)
    {
        // ถ้าจะให้แถบเปลี่ยน max ตอนบัฟ/เนิร์ฟ ก็อัปเดต max ได้จากพารามิเตอร์นี้
        slider.maxValue = max;
        slider.value = current;
    }
    
}
