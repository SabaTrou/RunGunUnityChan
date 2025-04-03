using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyMove : MonoBehaviour
{
    [SerializeField]
    protected float _lotationTime = 3f;//Ÿ‚Ìs“®‚Ö‚Ì‘JˆÚ‚Ü‚Å‚ÌŠÔ
    protected float _lotationCount = 0f;
    protected bool _isMoving = false;
    public virtual void UpdateMove()
    {

    }
}
