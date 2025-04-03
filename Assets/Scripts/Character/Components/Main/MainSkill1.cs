using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class MainSkill1 : BaseMainSkill
{
    public override IBaseCollisionData2D BaseData { get => _circle; }
    private CircleData2D _circle;
    private CapsuleCollider2D _circleCollider;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _activeTime = 0.1f;
    private float _activeCount = 0f;
    private bool _isHit = false;
    protected override void SubStart()
    {
        _circleCollider = this.gameObject.GetComponent<CapsuleCollider2D>();
        //_circle = new(transform.position, _circleCollider.radius);
        //_updateDel += UpdateCircle;
        _updateDel += SkillMove;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _circleCollider.enabled = false;

    }
    private void UpdateCircle()
    {
        //_circle.SetData(transform.position, _circleCollider.radius);
    }
    protected override void SkillMove()
    {
        if (_checkCollisionMode != CheckCollisionMode.collisionable)
        {
            _checkCollisionMode = CheckCollisionMode.collisionable;
            _spriteRenderer.enabled = true;
            _circleCollider.enabled = true;
        }
        Debug.Log("skill");
        if (_activeTime <= _activeCount)
        {
            _activeCount = 0;
            _checkCollisionMode = CheckCollisionMode.dontCollisionable;
            _isHit = false;
            _isActive = false;
            _spriteRenderer.enabled = false;
            _circleCollider.enabled = false;
        }
        _activeCount += Time.deltaTime;
    }
    public override void OnCollisionEvent(CollisionData2D collisionable)
    {
        
        if (_isHit)
        {
            return;
        }
        switch (collisionable.collisionable)
        {
            case BaseEnemyCharacter enemyCharacter:
                {
                    enemyCharacter.CalculateDamage(_skillData.DefaultDamage);
                    Debug.Log("damage:" + _skillData.DefaultDamage);
                    break;
                }


        }
        _isHit = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive)
        {
            Debug.Log("Return by isActive");
            return;
        }
        if (_isHit)
        {
            Debug.Log("Return by isHit");
            return;
        }
        Debug.Log(CollisionableLib.collisionableLib);
        if (!CollisionableLib.collisionableLib.TryGetCollisionable(collision.gameObject, out ICollisionable2D collisionable))
        {
            _isHit = true;
            return;
        }
        switch (collisionable)
        {
            case BaseEnemyCharacter enemyCharacter:
                {
                    enemyCharacter.CalculateDamage(_skillData.DefaultDamage);
                    Debug.Log("damage:" + _skillData.DefaultDamage);
                    break;
                }


        }
        _isHit = true;
    }
}
