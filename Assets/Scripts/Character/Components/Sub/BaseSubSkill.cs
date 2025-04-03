using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSubSkill : BaseSkill, ICollisionable2D
{
    protected bool _isActive = default;

    protected delegate void UpdateDel();
    protected UpdateDel _updateDel;
    public float _reCastTime = 1f;

    public abstract IBaseCollisionData2D BaseData { get; }

    public CheckCollisionMode CheckCollisionMode { get => _checkCollisionMode; }

    protected CheckCollisionMode _checkCollisionMode;
    public LayerMask CollisionableLayer { get => _layer; }
    [SerializeField]
    protected LayerMask _layer;

    protected override void SubStart()
    {

    }
    public void SubSkill()
    {

        _isActive = true;
    }
    public void UpdateSkill()
    {
        if (!_isActive)
        {
            return;
        }
        _updateDel.Invoke();
        //Ç±Ç±Ç…èàóù
    }
    protected virtual void SkillMove()
    {

    }
    public virtual void OnCollisionEvent(CollisionData2D collisionable)
    {
        Debug.Log("Hit Collisionable is " + collisionable.collisionable.gameObject.transform.name);
    }
}