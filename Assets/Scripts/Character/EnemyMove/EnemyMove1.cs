using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyMove1 : BaseEnemyMove
{
    [SerializeField]
    private BaseEnemyAttack[] _enemyAttacks = new BaseEnemyAttack[] { };

    private int _moveIndex = 0;
    public override void UpdateMove()
    {

        //if (_isMoving)
        //{
        //    _lotationCount += Time.deltaTime;
        //    if (_lotationCount >= _lotationTime)
        //    {
        //        _lotationCount = 0;
        //        _isMoving = false;
        //    }
        //    return;
        //}
        //_isMoving = true;
        if (!_enemyAttacks[_moveIndex].IsActive)
        {
            _enemyAttacks[_moveIndex].Fire();
            return;
        }

        _enemyAttacks[_moveIndex].UpdateAttack();

        if (_enemyAttacks[_moveIndex].IsActive)
        {           
            return;
        }
        _moveIndex++;
        if (_moveIndex >= _enemyAttacks.Length)
        {
            _moveIndex = 0;
        }
    }

}
