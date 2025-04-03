using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]

public abstract class BasePlayerCharacter : MonoBehaviour, ICollisionable2D
{
    protected delegate void CharacterUpdateDel();
    protected CharacterUpdateDel _characterUpdateDel;
    [SerializeField, Tooltip("à⁄ìÆ")]
    protected BaseMove _move;

    [SerializeField]
    private BaseMainSkill _mainSkill;
    public BaseMainSkill MainSkill { get => _mainSkill; }

    [SerializeField]
    protected CharacterStatus _status=new();
    public CharacterStatus Status { get => _status; }
    public IBaseCollisionData2D BaseData { get => _circle; }
    protected CircleData2D _circle;
    private CircleCollider2D _circleCollider;

   
    public CheckCollisionMode CheckCollisionMode => collisionMode;
    private CheckCollisionMode collisionMode = CheckCollisionMode.collisionable;
    public LayerMask CollisionableLayer => mask;
    [SerializeField]
    private LayerMask mask;


    [Injection]
    private IPublisher<CollisionableAddEvent> _collisionableAddEvent;//çUåÇÇÃìñÇΩÇËîªíËìnÇ∑óp
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        CheckActionInstance();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circle = new(transform.position,_circleCollider.radius);
        _characterUpdateDel += UpdateCircle;
        
        _characterUpdateDel += _mainSkill.UpdateSkill;
        
        
        SubCharacterStart();
       SendSkillDetections();
    }
    protected virtual void SubCharacterStart()
    {

    }
    public void Init()
    {
        _status.Initialize();
        _mainSkill?.Init();
    }
    public void CharacterUpdate()
    {
        _characterUpdateDel.Invoke();
    }
    public void FireMainSkill()
    {
        _mainSkill.MainSkill();
        Vector3 enePos= CharacterLib.instance.GetEnemys()[0].transform.position;
        float direction=enePos.x-transform.position.x;
        int rotY=0;
        if(direction<0)
        {
            rotY = 180;
        }
        transform.eulerAngles=new Vector3(transform.rotation.x,rotY,transform.rotation.z) ;
        
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
        _mainSkill ??= this.gameObject.AddComponent<MainSkill1>();
        

    }
    private void UpdateCircle()
    {
        _circle.SetData(transform.position, _circleCollider.radius);
    }
    public void SendSkillDetections()
    {
        _collisionableAddEvent.Publish(new CollisionableAddEvent(_mainSkill));
    }
    public virtual void OnCollisionEvent(CollisionData2D collisionable)
    {
        Debug.Log("Hit" + collisionable.collisionable.gameObject.transform.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
