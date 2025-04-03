using System;
using System.Collections.Generic;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;

public class CollisionableUpdater
{
    [Injection]
    private ICollisionChecker _collisionChecker;
    [Injection]
    private CharacterUpdater _characterUpdater;
   
    public void UpdateCollisionable()
    {
        
        
        _characterUpdater.UpdateCharacter();
        //_collisionChecker.CheckCollisions();
    }
}