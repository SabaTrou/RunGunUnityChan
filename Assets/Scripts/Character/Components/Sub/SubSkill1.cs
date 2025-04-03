using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SubSkill1 : BaseSubSkill
{
    public override IBaseCollisionData2D BaseData { get => _circle; }
    private CircleData2D _circle;
    private CircleCollider2D _circleCollider;
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _activeTime = 0.1f;
    private float _activeCount = 0f;
    private bool _isHit = false;
    protected override void SubStart()
    {
        _circleCollider = this.gameObject.GetComponent<CircleCollider2D>();
        _circle = new(transform.position, _circleCollider.radius);
        _updateDel += UpdateCircle;
        _updateDel += SkillMove;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }
    private void UpdateCircle()
    {
        _circle.SetData(transform.position, _circleCollider.radius);
    }
    protected override void SkillMove()
    {
        if (_checkCollisionMode != CheckCollisionMode.collisionable)
        {
            _checkCollisionMode = CheckCollisionMode.collisionable;
            _spriteRenderer.enabled = true;
        }
        Debug.Log("skill");
        if (_activeTime <= _activeCount)
        {
            _activeCount = 0;
            _checkCollisionMode = CheckCollisionMode.dontCollisionable;
            _isHit = false;
            _isActive = false;
            _spriteRenderer.enabled = false;
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
                    Debug.Log("enemyHp:" + enemyCharacter.Status.HitPoint);
                    break;
                }


        }
        _isHit = true;

    }
}
