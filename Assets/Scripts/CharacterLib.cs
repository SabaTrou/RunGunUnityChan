using System;
using System.Collections.Generic;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer.Unity;

public class CharacterLib:IStartable
{
    private ISubscriber<PlayerCharacterAddEvent> _playerAddSub;
    private ISubscriber<EnemyCharacterAddEvent> _enemyAddSub;
    private List<BasePlayerCharacter> _players=new();
    private List<BaseEnemyCharacter> _enemys=new();
    public static CharacterLib instance;
    void IStartable.Start()
    {
        instance = this;
    }
    [Injection]
    private void InjectPlayerCharacterAddEvent(ISubscriber<PlayerCharacterAddEvent> subscriber)
    {
        _playerAddSub = subscriber;
        _playerAddSub.Subscribe(OnPlayerCharacterAdded);
    }
    private void OnPlayerCharacterAdded(PlayerCharacterAddEvent addEvent)
    {
        _players.Add(addEvent.character);
    }
    [Injection]
    private void InjectEnemyCharacterAddEvent(ISubscriber<EnemyCharacterAddEvent> subscriber)
    {
        _enemyAddSub = subscriber;
        _enemyAddSub.Subscribe(OnEnemyAdded);
    }
    private void OnEnemyAdded(EnemyCharacterAddEvent addEvent)
    {
        _enemys.Add(addEvent.character);
    }
    public BasePlayerCharacter[] GetPlayers()
    {
        return _players.ToArray();
    }
    public BaseEnemyCharacter[] GetEnemys()
    {
        return _enemys.ToArray();
    }
}