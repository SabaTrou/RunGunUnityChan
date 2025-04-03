using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;

public class CharacterHolder : MonoBehaviour
{
    [SerializeField]
    private BasePlayerCharacter _player1;
    [SerializeField]
    private Transform _player1Pos;
    [Injection]
    private IPublisher<CharacterCreateRequest> _characterAddRecestEvent;
    
    [Injection]
    private IResolver _resolver;
    // Start is called before the first frame update
    void Start()
    {
        if (_player1 != null)
        {
            _characterAddRecestEvent.Publish(new (_player1,_player1Pos.position,Quaternion.identity,null));
            
            
            _resolver.Resolve<BasePlayerCharacter>(_player1);
        }
    }

    
}
