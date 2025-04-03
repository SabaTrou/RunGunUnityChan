using System.Collections;
using System.Collections.Generic;
using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer;
using UnityEngine;

public class UISender : MonoBehaviour
{
    [SerializeField]
    private BaseEnemyCharacter _enemyCharacter;
    [SerializeField]
    private EnemyLifeBar _enemyLifebar;
    [SerializeField]
    private PlayerStatusUI[] _playerUiArray=new PlayerStatusUI[] { };
    private int _playerCount = -1;

    private ISubscriber<PlayerCharacterAddEvent> _characterSubscriber;
    private ISubscriber<SkillAddEvent> _skillSubscriber;
    private void Start()
    {
        _enemyLifebar.SetDefault(_enemyCharacter.Status.HitPoint);
        _enemyLifebar.SetProperty(_enemyCharacter.Status.GetHpProperty());
        foreach(PlayerStatusUI lifeBar in _playerUiArray)
        {
            if(lifeBar==null)
            {
                continue;
            }
            lifeBar.gameObject.SetActive(false);
        }
    }

    [Injection]
    private void InjectCharacterSubscriber(ISubscriber<PlayerCharacterAddEvent> subscriber)
    {
        
        _characterSubscriber = subscriber;
        _characterSubscriber.Subscribe(OnCharacterAdded);
    }
    private void OnCharacterAdded(PlayerCharacterAddEvent addEvent)
    {
        Debug.Log("CharacterAdded");
       if(_playerUiArray.Length<_playerCount)
        {
            return;
        }
        _playerCount++;
        _playerUiArray[_playerCount].gameObject.SetActive(true);
        Debug.Log(addEvent.character.Status.GetHpProperty());
        _playerUiArray[_playerCount].LifeBar?.SetProperty(addEvent.character.Status.GetHpProperty());
        
    }
    [Injection]
    private void InjectSkillAddEvet(ISubscriber<SkillAddEvent> subscriber)
    {
        _skillSubscriber = subscriber;
        _skillSubscriber.Subscribe(OnSkillAdded);
    }
    private void OnSkillAdded(SkillAddEvent addEvent)
    {
        _playerUiArray[_playerCount].MainIcon?.SetProperty(addEvent.skillData.GetGCDProperty());
    }
}
