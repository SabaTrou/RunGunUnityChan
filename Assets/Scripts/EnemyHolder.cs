using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField]
    private BaseEnemyCharacter[] _enemyCharacters=new BaseEnemyCharacter[] { };
    [Injection]
    IPublisher<EnemyCharacterAddEvent> _characterAddEventPub;
    private void Start()
    {
        
        foreach (BaseEnemyCharacter character in _enemyCharacters)
        {
            _characterAddEventPub?.Publish(new EnemyCharacterAddEvent(character));
        }
    }
}
