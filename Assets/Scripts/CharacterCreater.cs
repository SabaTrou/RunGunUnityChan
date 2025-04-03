using System;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using UnityEngine;
public class CharacterCreater:IStartable
{
    [Injection]
    private IResolver _resolver;
    [Injection]
    private IPublisher<PlayerCharacterAddEvent> _addEvent;
    private ISubscriber<CharacterCreateRequest> _requestEvent;
    [Injection]
    private IPublisher<SkillAddEvent> _skillAddEvent;
    #region
    [Injection]
    private void InjectRequestEvent(ISubscriber<CharacterCreateRequest> subscriber)
    {
        
        _requestEvent = subscriber;
        _requestEvent.Subscribe(OnRequest);
    }
    #endregion
    #region Event
    private void OnRequest(CharacterCreateRequest createRequest)
    {
        BasePlayerCharacter character;
        character = MonoBehaviour.Instantiate(createRequest.character, createRequest.position, createRequest.rotation, createRequest.parent);
        _resolver.Resolve<BasePlayerCharacter>(character);
        character.Init();
        _addEvent.Publish(new PlayerCharacterAddEvent(character));
        Debug.Log(character.MainSkill.SkillData.GetGCDProperty());
        _skillAddEvent.Publish(new(character.MainSkill.SkillData, SkillAddEvent.Skillcategory.main));
    }
    #endregion
    void IStartable.Start()
    {

    }
}