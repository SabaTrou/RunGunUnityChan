using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMainSkill : BaseSkill, ICollisionable2D
{
    protected bool _isActive = default;

    protected delegate void UpdateDel();
    protected UpdateDel _updateDel;
    
    protected bool _isReCasting = false;

    private ReactiveProperty<float> _reCastProperty;

    public abstract IBaseCollisionData2D BaseData { get; }

    public CheckCollisionMode CheckCollisionMode { get => _checkCollisionMode; }

    protected CheckCollisionMode _checkCollisionMode;
    public LayerMask CollisionableLayer { get => _layer; }
    [SerializeField]
    protected LayerMask _layer;
    
    protected override void SubStart()
    {

    }
    public void MainSkill()
    {
        if (_isReCasting)
        {
            return;
        }
        _skillData.NowGlobalCoolTime = 0;
        _isReCasting = true;
        _isActive = true;
    }
    public void UpdateSkill()
    {
        if (_isReCasting)
        {
            UpdateReCast();
        }
        if (!_isActive)
        {
            return;
        }
        
        _updateDel.Invoke();
        //Ç±Ç±Ç…èàóù
    }
    protected void StartReCast()
    {
        _isReCasting = true;
    }
    private void UpdateReCast()
    {
        if (_skillData.GlobalCoolTime <= _skillData.NowGlobalCoolTime)
        {
            _isReCasting = false;
            _skillData.NowGlobalCoolTime = _skillData.GlobalCoolTime;
            return;
        }
       
        _skillData.NowGlobalCoolTime += Time.deltaTime;
        
    }
    protected virtual void SkillMove()
    {

    }
    public virtual void OnCollisionEvent(CollisionData2D collisionable)
    {
        Debug.Log("Hit Collisionable is " + collisionable.collisionable.gameObject.transform.name);
    }
}
