using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class LaserAttack :BaseEnemyAttack
{
    private Laser[] _lasers=new Laser[] { };
    [SerializeField]
    GameObject _position;
    Vector3 _attackPos = default;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;
    private void Start()
    {
        _lasers = GetComponentsInChildren<Laser>();
        if(_position==null)
        {
            return;
        }
        _attackPos = _position.transform.position;
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }
    public override void Fire()
    {
        _isActive = true ;
        transform.root.gameObject.transform.position = _attackPos;
        
        foreach(Laser laser in _lasers)
        {
            laser.FireLaser();
        }
        
    }
    public override bool UpdateAttack()
    {
        int finishCount = 0;
        foreach (Laser laser in _lasers)
        {
            if(laser.GetIsFinished())
            {
                finishCount++;
            }
        }
        if(finishCount>=_lasers.Length)
        {
            _isActive = false ;
            return true;
        }
        return false;
    }

}
