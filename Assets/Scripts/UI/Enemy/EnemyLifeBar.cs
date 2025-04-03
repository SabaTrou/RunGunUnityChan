using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyLifeBar : MonoBehaviour
{

    private Slider slider;

    private int _maxHealth = default;
    private void Start()
    {
        this.slider = this.gameObject.GetComponent<Slider>();
    }
    public void SetProperty(ReactiveProperty<int> property)
    {
        property.Subscribe(SetHealth);
    }
    public void SetDefault(int maxHp)
    {
        _maxHealth = maxHp;
    }
    private void SetHealth(int hp)
    {
        if (slider == null)
        {
            return;
        }
        slider.value = (float)hp / _maxHealth;
    }
}
