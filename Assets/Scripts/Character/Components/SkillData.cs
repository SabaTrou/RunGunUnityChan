using UnityEngine;

[System.Serializable]
public class SkillData
{
    #region damage
    #region default
    public int DefaultDamage{ get => _damage;}
    [SerializeField]
    private int _damage = 100;
    #endregion
    #region now
    //現在値
    private int _nowDanage = 0;
    //ダメージ用プロパティ
    private ReactiveProperty<int> _damageProperty;
    public int NowDamage
    {
        get
        {
            return _damageProperty.Value;
        }
        set
        {
            _damageProperty.Value = value;
        }

    }
    #endregion 
    /// <summary>
    /// ダメージ値のプロパティを取得
    /// </summary>
    /// <returns></returns>
    public ReactiveProperty<int> GetDamageProperty()
    {
        return _damageProperty;
    }
    private void InitDamage()
    {
        _nowDanage = _damage;
        _damageProperty = new(_damage);
    }
    #endregion damage
    #region coolTime
    #region default
    public float DefaultCoolTime { get => _coolTime; }
    [SerializeField]
    private float _coolTime = 0;
    #endregion
    #region now
    //クールタイム用プロパティ
    private ReactiveProperty<float> _nowCoolTimeProperty;
    public float NowCoolTime
    {
        get
        {
            return _nowCoolTimeProperty.Value;
        }
        set
        {
            _nowCoolTimeProperty.Value = value;
        }
    }
    //現在値
    private float _nowCoolTime = 0;
    #endregion
    /// <summary>
    /// クールタイム値のプロパティを取得
    /// </summary>
    /// <returns></returns>
    public ReactiveProperty<float> GetCoolTimeProperty()
    {
        return _nowCoolTimeProperty;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    private void InitCoolTime()
    {
        _nowCoolTime = _coolTime;
        _nowCoolTimeProperty = new(_coolTime);
    }
    #endregion coolTime
    #region globalCoolTime
    #region default
    public float GlobalCoolTime { get => _globalCoolTime; }
    [SerializeField]
    private float _globalCoolTime = 2.5f;
    #endregion
    #region now
    public float NowGlobalCoolTime
    {
        get
        {
            return _nowGCDProperty.Value;
        }
        set
        {
            _nowGCDProperty.Value = value;
        }
    }
    private float _nowGlobalCoolTime;
    private ReactiveProperty<float> _nowGCDProperty;
    #endregion
    /// <summary>
    /// GCD値のプロパティを取得   
    /// </summary>
    /// <returns></returns>
    public ReactiveProperty<float> GetGCDProperty()
    {
        return _nowGCDProperty;
    }
    /// <summary>
    /// 初期化
    /// </summary>
    private void InitGCD()
    {
        _nowGlobalCoolTime = _globalCoolTime;
        _nowGCDProperty = new(_globalCoolTime);
    }
    #endregion globalCoolTime
    #region Initialize
    /// <summary>
    /// しないと動かない…はず
    /// </summary>
    public void Initialize()
    {
        InitDamage();
       
        InitCoolTime();
        InitGCD();
        Debug.LogWarning(_nowGCDProperty);
    }
    #endregion
}