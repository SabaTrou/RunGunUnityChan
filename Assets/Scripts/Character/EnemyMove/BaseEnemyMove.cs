using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyMove : MonoBehaviour
{
    [SerializeField]
    protected float _lotationTime = 3f;//次の行動への遷移までの時間
    protected float _lotationCount = 0f;
    protected bool _isMoving = false;
    public virtual void UpdateMove()
    {

    }
}
