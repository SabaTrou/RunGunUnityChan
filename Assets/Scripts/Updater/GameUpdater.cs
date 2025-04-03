using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer.Unity;
using SabaSimpleDIContainer;

public class GameUpdater : ITickable,IStartable
{
    [Injection]
    private ControllerUpdater _controllerUpdater;
    [Injection]
    private CollisionableUpdater _collisionUpdater;
    [Injection]
    private EnemyUpdater _enemyUpdater;
    [Injection]
    private GameRuleUpdater _gameRuleUpdater;
    void IStartable.Start()
    {

    }
    void ITickable.Tick()
    {
        _controllerUpdater.UpdateController();

        _collisionUpdater.UpdateCollisionable();
        _enemyUpdater.UpdateEnemys();
        _gameRuleUpdater.UpdateRule();
    }
}
