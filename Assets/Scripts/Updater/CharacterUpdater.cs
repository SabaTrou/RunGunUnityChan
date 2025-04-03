using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;

public class CharacterUpdater 
{
    private ISubscriber<PlayerCharacterAddEvent> _subscriber;
    private List<BasePlayerCharacter> _characterList=new();
    [Injection]
    private void InjectCharacterAddEvent(ISubscriber<PlayerCharacterAddEvent> subscriber)
    {
        _subscriber = subscriber;
        _subscriber.Subscribe(OnCharacterAdded);
    }
    private void OnCharacterAdded(PlayerCharacterAddEvent addEvent)
    {
        _characterList.Add(addEvent.character);
       
    }
    public void UpdateCharacter()
    {
        foreach (BasePlayerCharacter character in _characterList)
        {
            character.CharacterUpdate();
        }
    }
}
