using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICollisionable3D
{
    
    /// <summary>
    /// �����蔻��p�̃f�[�^
    /// </summary>
    public IBaseCollisionData3D BaseData { get;  }
    /// <summary>
    /// �Փ˂̔�����Ƃ邩�ǂ����̃��[�h
    /// </summary>
    public CheckCollisionMode CheckCollisionMode { get; }
    /// <summary>
    /// �Փ˂��郌�C���[
    /// </summary>
    public LayerMask CollisionableLayer { get; }
    /// <summary>
    /// OnCollision
    /// </summary>
    /// <param name="collisionable"></param>
    public void OnCollisionEvent(ICollisionable3D collisionable);
    
    public GameObject gameObject { get; }
    
}
public interface ICollisionable2D
{
    /// <summary>
    /// �����蔻��p�̃f�[�^
    /// </summary>
    public IBaseCollisionData2D BaseData { get; }
    /// <summary>
    /// �Փ˂̔�����Ƃ邩�ǂ����̃��[�h
    /// </summary>
    public CheckCollisionMode CheckCollisionMode { get; }
    /// <summary>
    /// �Փ˂��郌�C���[
    /// </summary>
    public LayerMask CollisionableLayer { get; }
    /// <summary>
    /// OnCollision
    /// </summary>
    /// <param name="collisionable"></param>
    public void OnCollisionEvent(CollisionData2D collisionable);

    public GameObject gameObject { get; }
}
public enum CheckCollisionMode
{
    collisionable,
    dontCollisionable
}
/// <summary>
/// �Փˏ��
/// </summary>
public class CollisionableData2D
{
    public readonly ICollisionable2D collisionable;
    public readonly  Vector2 depth;
    public CollisionableData2D(ICollisionable2D collisionable,Vector2 depth)
    {
        this.collisionable = collisionable;
        this.depth = depth;
    }
}

