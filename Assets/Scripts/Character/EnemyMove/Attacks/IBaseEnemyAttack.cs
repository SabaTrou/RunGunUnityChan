using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class BaseEnemyAttack:MonoBehaviour
{
    public bool IsActive { get => _isActive; }
    protected bool _isActive = false;
    public abstract void Fire();
    public abstract bool UpdateAttack();
   
}
