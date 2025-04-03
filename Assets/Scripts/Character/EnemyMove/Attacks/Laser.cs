using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Unity.Mathematics;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Laser : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private float _activeTime = 1f;
    [SerializeField]
    private float _castTime = 1f;
    [SerializeField]
    private Color _castColor = Color.white;
    private Color _laserColor;
    private bool _isHit = false;
    private bool _isActive = false;
    private bool _isFinished = false;
    //[SerializeField]
    private int _damage = 1;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;
    // Start is called before the first frame update
    void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _laserColor = _spriteRenderer.color;
    }

   public async void FireLaser()
    {
        _isFinished = false;
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = _castColor;
        await Task.Delay((int)_castTime * 1000);
        _spriteRenderer.color = _laserColor;
        _isActive = true;
        _boxCollider.enabled = true;
       
        await Task.Delay((int)_activeTime * 1000);
        _isActive = false;
        _isHit = false;
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _isFinished = true;

    }
    public bool GetIsFinished()
    {
        return _isFinished;
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
    }
    private void OnTriggerStay2D(Collider2D collision)
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
            if(collision.gameObject==null)
            {
                Debug.Log("object is null");
            }
            Debug.Log("Return to "+collision.gameObject.transform.root.name);
            _isHit = true;
            return;
        }
        Debug.Log("collisionable is "+collisionable);
        switch (collisionable)
        {
            case BasePlayerCharacter player:
                {
                    player.CalculateDamage(_damage);
                    Debug.Log("playerHp:" + player.Status.HitPoint);
                    break;
                }


        }
        _isHit = true;
    }
}

