using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Wall : MonoBehaviour, ICollisionable2D
{
    public IBaseCollisionData2D BaseData { get => _boxData; }
    private BoxData2D _boxData;
    public CheckCollisionMode CheckCollisionMode { get => _collisionMode; }
    private CheckCollisionMode _collisionMode;
    public LayerMask CollisionableLayer { get => _mask; }
    [SerializeField]
    private LayerMask _mask = default;
    private BoxCollider _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        //CreateBoxData();

    }

    private void SetBoxData()
    {
        Vector3 herf = new Vector3(0, _boxCollider.size.y / 2, 0);
        Vector3 origin = transform.position + herf;
        Vector2 end = transform.position - herf;
        
    }
    private void CreateBoxData()
    {
        Vector2 herf = new Vector3(0, transform.lossyScale.y*_boxCollider.size.y/2);
        Vector2 width= new Vector3(transform.lossyScale.x * _boxCollider.size.x, 0);
        Vector2 origin = (Vector2)transform.position + herf;
        Vector2 end = (Vector2)transform.position - herf;
        _boxData = new(origin,end,width.x);
       
        int i = 0;
        foreach(Vector2 vector in _boxData.GetVertices())
        {
           
            i++;
        }
        
    }
    public void OnCollisionEvent(CollisionData2D collisionable)
    {
        
    }
}
