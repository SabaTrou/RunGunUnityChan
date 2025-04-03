using System.Collections;
using System.Collections.Generic;
using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class BaseEnemyCharacter : MonoBehaviour,ICollisionable2D
{
    protected delegate void CharacterUpdateDel();
    protected CharacterUpdateDel _characterUpdateDel;
    [SerializeField, Tooltip("à⁄ìÆ")]
    protected BaseMove _move;

    

    [SerializeField]
    protected CharacterStatus _status=new();
    [SerializeField]
    private BaseEnemyMove _enemyAttackLotation;
    public CharacterStatus Status { get => _status; }
    public IBaseCollisionData2D BaseData { get => _circle; }
    protected CircleData2D _circle;
    private CircleCollider2D _circleCollider;
    private Vector3 _origin;
    private Vector3 _end;
    public CheckCollisionMode CheckCollisionMode => collisionMode;
    private CheckCollisionMode collisionMode = CheckCollisionMode.collisionable;
    public LayerMask CollisionableLayer => mask;
    [SerializeField]
    private LayerMask mask;


    [Injection]
    private IPublisher<CollisionableAddEvent> _collisionableAddEvent;//çUåÇÇÃìñÇΩÇËîªíËìnÇ∑óp
    // Start is called before the first frame update
    void Start()
    {
        CheckActionInstance();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circle = new(transform.position, _circleCollider.radius);
        _characterUpdateDel += UpdateCircle;
        _status.Initialize();
       if(_enemyAttackLotation!=null)
        {
            _characterUpdateDel += _enemyAttackLotation.UpdateMove;
        }


        SubCharacterStart();

    }
    protected virtual void SubCharacterStart()
    {

    }
    public void CharacterUpdate()
    {
        _characterUpdateDel.Invoke();
    }

    public virtual void MoveCharacter(Vector2 vector)
    {
        _move.CharacterMove(vector, _status.MoveSpeed);
    }


    /// <summary>
    /// serializeFieldÇ…ìoò^Ç≥ÇÍÇƒÇ»ÇØÇÍÇŒê∂ê¨
    /// </summary>
    private void CheckActionInstance()
    {

        _move ??= this.gameObject.AddComponent<CharacterMove>();
       


    }
    private void UpdateCircle()
    {
        _circle.SetData(transform.position, _circleCollider.radius);
    }
    public virtual void OnCollisionEvent(CollisionData2D collisionable)
    {
        Debug.Log("Hit" + collisionable.collisionable.gameObject.transform.name);
    }
}
