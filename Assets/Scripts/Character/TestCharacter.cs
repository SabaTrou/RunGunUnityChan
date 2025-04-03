using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestCharacter : BasePlayerCharacter
{

    protected override void SubCharacterStart()
    {
        
    }
    public override void OnCollisionEvent(CollisionData2D collisionable)
    {
        Debug.Log("Hit"+collisionable);
    }
}
