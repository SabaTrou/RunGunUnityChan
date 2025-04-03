using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStatus
{
    #region moveSpeed
    [SerializeField]
    private float moveSpeed = 1f;
    private ReactiveProperty<float> moveSpeedProperty;
    public float MoveSpeed
    {
        get
        {
            return moveSpeedProperty.Value;
        }
        set
        {
            moveSpeedProperty.Value = value;
        }

    }

    public ReactiveProperty<float> GetMoveSpeedProperty()
    {
        return moveSpeedProperty;
    }
    #endregion
    #region HitPoint
    [SerializeField, Range(0, 9999)]
    private int hitPoint = 0;
    private ReactiveProperty<int> hitPointProperty;
   
    public int HitPoint
    {
        get
        {
            if (hitPointProperty.Value < 0)
            {
                return 0;
            }
            return hitPointProperty.Value;
        }
        set
        {
            hitPointProperty.Value = value;
        }
    }
    public ReactiveProperty<int> GetHpProperty()
    {
        return hitPointProperty;
    }
    #endregion
    #region Initialize
    public void Initialize()
    {
        moveSpeedProperty = new(moveSpeed);
        hitPointProperty = new(hitPoint);
    }
    #endregion
}


