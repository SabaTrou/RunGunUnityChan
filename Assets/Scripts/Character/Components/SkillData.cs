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
    //���ݒl
    private int _nowDanage = 0;
    //�_���[�W�p�v���p�e�B
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
    /// �_���[�W�l�̃v���p�e�B���擾
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
    //�N�[���^�C���p�v���p�e�B
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
    //���ݒl
    private float _nowCoolTime = 0;
    #endregion
    /// <summary>
    /// �N�[���^�C���l�̃v���p�e�B���擾
    /// </summary>
    /// <returns></returns>
    public ReactiveProperty<float> GetCoolTimeProperty()
    {
        return _nowCoolTimeProperty;
    }
    /// <summary>
    /// ������
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
    /// GCD�l�̃v���p�e�B���擾   
    /// </summary>
    /// <returns></returns>
    public ReactiveProperty<float> GetGCDProperty()
    {
        return _nowGCDProperty;
    }
    /// <summary>
    /// ������
    /// </summary>
    private void InitGCD()
    {
        _nowGlobalCoolTime = _globalCoolTime;
        _nowGCDProperty = new(_globalCoolTime);
    }
    #endregion globalCoolTime
    #region Initialize
    /// <summary>
    /// ���Ȃ��Ɠ����Ȃ��c�͂�
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