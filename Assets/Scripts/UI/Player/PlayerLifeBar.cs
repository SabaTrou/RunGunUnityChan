using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerLifeBar : MonoBehaviour
{
    private Slider _slider;
    private int _maxValue=default;
    private ReactiveProperty<int> _playerLifeProperty;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();

    }
    public void SetProperty(ReactiveProperty<int> lifeProperty)
    {
        Debug.Log("lifeProperty:"+lifeProperty);
        _maxValue = lifeProperty.Value;
        lifeProperty.Subscribe(OnLifeChanged);
        
        Debug.Log("maxHealth "+_maxValue);
    }
    private void OnLifeChanged(int life)
    {
        _slider.value = (float)life/_maxValue;
        Debug.Log(_slider.value+"max"+_maxValue+"now"+life);
    }
   
}
