using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Unity;
using SabaSimpleDIContainer.Pipe;

public class GameLifeTimeScope : LifeTimeScope
{
    [SerializeField]
    private CharacterHolder _characterHolder;
    [SerializeField]
    private EnemyHolder _enemyHolder;
    [SerializeField]
    private UISender _uiSender;
    [SerializeField]
    private SceneChanger _sceneChanger;
    
    protected override void Configure(IContainer container)
    {
        #region RregisterComponent
        if(_characterHolder!=null)
        {
            container.RegisterComponent(_characterHolder);
        }
        if(_enemyHolder!=null)
        {
            container.RegisterComponent(_enemyHolder);
        }
        if(_uiSender!=null)
        {
            container.RegisterComponent(_uiSender);
        }
        if(_sceneChanger!=null)
        {
            container.RegisterComponent(_sceneChanger);
        }
        
        #endregion
        #region register
        //
        container.Register<CharacterUpdater>(LifeTime.singleton);
        //
        container.Register<ControllerUpdater>(LifeTime.singleton);
        container.Register<CollisionableUpdater>(LifeTime.singleton);
        container.Register<EnemyUpdater>(LifeTime.singleton);
        container.Register<GameRuleUpdater>(LifeTime.singleton);
        //
        container.Register<ICollisionChecker, NewCollisionChecker>();

        #endregion
        #region entry
        container.RegisterEntryPoint<CharacterSetService>();
        container.RegisterEntryPoint<ControllerProvider>();
       
        container.RegisterEntryPoint<GameUpdater>();
        container.RegisterEntryPoint<CollisionableProvider>();
        container.RegisterEntryPoint<CharacterCreater>();
        container.RegisterEntryPoint<CollisionableLib>();
        container.RegisterEntryPoint<CharacterLib>();
        #endregion
        #region pipe
        container.RegisterBroker<ControllerAddEvent>();
        container.RegisterBroker<PlayerCharacterAddEvent>();
        container.RegisterBroker<CharacterCreateRequest>();
        container.RegisterBroker<CollisionableAddEvent>();
        container.RegisterBroker<EnemyCharacterAddEvent>();
        container.RegisterBroker<SkillAddEvent>();
        #endregion
    }
}
