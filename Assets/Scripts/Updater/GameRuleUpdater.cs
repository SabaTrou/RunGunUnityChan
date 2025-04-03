using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer;
using System.Runtime.CompilerServices;

public class GameRuleUpdater
{
    private List<BasePlayerCharacter> _playerCharacters = new();
    private List<BaseEnemyCharacter> _enemyCharacters = new();
    private List<ReactiveProperty<int>> _lifeProperties = new();
    private ISubscriber<PlayerCharacterAddEvent> _playerAddEvent;
    private ISubscriber<EnemyCharacterAddEvent> _enemyAddEvent;
    [Injection]
    private SceneChanger _sceneChanger;
    [Injection]
    private void InjectPlayerCharacterAddEvent(ISubscriber<PlayerCharacterAddEvent> subscriber)
    {
        _playerAddEvent = subscriber;
        _playerAddEvent.Subscribe(OnPlayerCharacterAdded);
    }
    private void OnPlayerCharacterAdded(PlayerCharacterAddEvent addEvent)
    {
        _playerCharacters.Add(addEvent.character);

    }
    [Injection]
    private void InjectEnemyCharacterAddEvent(ISubscriber<EnemyCharacterAddEvent> subscriber)
    {
        _enemyAddEvent = subscriber;
        _enemyAddEvent.Subscribe(OnEnemyCharacterAdded);
    }
    private void OnEnemyCharacterAdded(EnemyCharacterAddEvent addEvent)
    {
        _enemyCharacters.Add(addEvent.character);
    }

    public void UpdateRule()
    {
        CheckPlayers();
        CheckEnemys();

    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckPlayers()
    {
        int deadPlayerCount = 0;
        foreach (BasePlayerCharacter character in _playerCharacters)
        {
            if (character.Status.HitPoint <= 0)
            {
                deadPlayerCount++;
            }
        }
        if (deadPlayerCount >= _enemyCharacters.Count)
        {
            _sceneChanger?.LoadGameOverScene();
        }
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckEnemys()
    {
        int deadEnemyCount = 0;
        foreach (BaseEnemyCharacter character in _enemyCharacters)
        {
            if (character.Status.HitPoint <= 0)
            {
                deadEnemyCount++;
            }
        }
        if(deadEnemyCount>=_enemyCharacters.Count)
        {
            _sceneChanger?.LoadClearScene();
        }
    }
}
