using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackGroundUpdater : MonoBehaviour
{
    private const float _maxLength = 1f;
    private const string _propName = "_MainTex";

    [SerializeField]
    private Vector2 _offsetSpeed;

    private Material _material;

    
    private void Start()
    {
        if (GetComponent<Image>() is Image i)
        {
            _material = i.material;
        }
    }

    private void Update()
    {
        if (!_material)
        {
            return; 
        }
        // xとyの値が0 ～ 1でリピートするようにする
        float x = Mathf.Repeat(Time.time * _offsetSpeed.x, _maxLength);
        float y = Mathf.Repeat(Time.time * _offsetSpeed.y, _maxLength);
        var offset = new Vector2(x, y);
        _material.SetTextureOffset(_propName, offset);
    }

    private void OnDestroy()
    {
        // ゲームをやめた後にマテリアルのOffsetを戻しておく
        if (_material)
        {
            return;
        }
        _material.SetTextureOffset(_propName, Vector2.zero);
    }
}