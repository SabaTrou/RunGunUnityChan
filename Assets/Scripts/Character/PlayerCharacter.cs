using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayerCharacter : BasePlayerCharacter
{
   private Vector2 _moveVec= Vector2.zero;
   
    protected override void SubCharacterStart()
    {
        _characterUpdateDel+= MoveUpdate;
        
        
    }
    public override void MoveCharacter(Vector2 vector)
    {
        _moveVec = vector;
    }
    private void MoveUpdate()
    {
        _move.CharacterMove(_moveVec,_status.MoveSpeed);
    }

    public override void OnCollisionEvent(CollisionData2D collisionable)
    {
        switch (collisionable.collisionable)
        {
            case Wall wall:
                {
                    
                    transform.position += (Vector3)collisionable.depth;
                    return;
                }
        }
        


    }
    
}
