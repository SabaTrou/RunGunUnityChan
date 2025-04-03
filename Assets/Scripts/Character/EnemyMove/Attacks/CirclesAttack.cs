using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.UIElements;

public class CirclesAttack : BaseEnemyAttack
{
    private CircleEnAt[] _circles = new CircleEnAt[] { };
    [SerializeField]
    GameObject _position;
    Vector3 _attackPos = default;
    [SerializeField]
    private float _waitTime = 1.5f;
    private float _countTime = 0f;
    BasePlayerCharacter[] _players;
    
    private bool _isFinished = false;
    private int _circleIndex = 0;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;
    private void Start()
    {
        _circles = GetComponentsInChildren<CircleEnAt>();
        if (_position == null)
        {
            return;
        }
        _attackPos = _position.transform.position;
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;


    }
    public override void Fire()
    {
        _isActive = true;
        transform.root.position = _attackPos;
        _players = CharacterLib.instance.GetPlayers();



    }
    public override bool UpdateAttack()
    {
        _countTime += Time.deltaTime;
        Debug.Log("fire circle");
        if (_countTime < _waitTime)
        {
            return _isFinished;
        }
        
        int index = Random.Range(0, _players.Length);
        _circles[_circleIndex].FireCircle(_players[index].transform.position);
        _countTime = 0;
        _circleIndex++;
        if(_circleIndex>=_circles.Length)
        {
            _isFinished = true;
            _circleIndex = 0;
        }
        return _isFinished;
    }
    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}
