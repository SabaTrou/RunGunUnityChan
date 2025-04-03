using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkillIcon : MonoBehaviour
{
    private Image _image;
    private float _maxTime = default;
    private void Start()
    {
        _image = GetComponent<Image>();
        
    }
    public void SetProperty(ReactiveProperty<float> property)
    {
       
        _maxTime = property.Value;
        property.Subscribe(OnValueChanged);
        
    }
    private void OnValueChanged(float time)
    {
        _image.fillAmount = time/_maxTime;
    }
}
