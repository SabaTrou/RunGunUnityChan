using System;
using System.Collections.Generic;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using UnityEngine;

public class EnemyUpdater
{
    private ISubscriber<EnemyCharacterAddEvent> _enemyAddevent;

    private List<BaseEnemyCharacter> _enemyCharacterList=new();
    [Injection]
    private void InjectEnemyCharacterAddEvent(ISubscriber<EnemyCharacterAddEvent> subscriber)
    {
        _enemyAddevent = subscriber;
        _enemyAddevent.Subscribe(OnCharacterAdded);
    }
    private void OnCharacterAdded(EnemyCharacterAddEvent addEvent)
    {
        Debug.Log("EnemyCharacterAdded");
        _enemyCharacterList.Add(addEvent.character);
    }
    public void UpdateEnemys()
    {
        foreach(BaseEnemyCharacter character in _enemyCharacterList)
        {
            character.CharacterUpdate();
        }
    }
}