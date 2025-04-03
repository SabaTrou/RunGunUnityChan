using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public interface IInputSubscriber
{
    public void SubscribeWithGetkeyDown(KeyCode code, Action action);
    public void UnsubscribeWithGetkeyDown(KeyCode code, Action action);
}

public class KeyebordInputPublisher:IInputSubscriber
{
    private readonly Dictionary<string, List<Action>> _DownActions = new();
    private readonly Dictionary<string, List<Action>> _inputActions = new();
    private SynchronizationContext _synchronizationContext;
    private CancellationTokenSource _cancellationTokenSource;

    /// <summary>
    /// �N��
    /// </summary>
    public void Start(SynchronizationContext context)
    {
        // ���C���X���b�h��SynchronizationContext��ݒ�
        _synchronizationContext = context;

        // �L�����Z���p�̃g�[�N���\�[�X
        _cancellationTokenSource = new CancellationTokenSource();

        // �L�����Z���p�̃g�[�N��
        CancellationToken token = _cancellationTokenSource.Token;

        // �S�Ă�KeyCode��������
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            _DownActions[code.ToString()] = new List<Action>();
        }

        // ���͏������J�n
        MonitorInputAsync(token);
    }

    private async void MonitorInputAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            // Unity�̃��C���X���b�h�œ��삷��悤��SynchronizationContext���g�p
            await Task.Run(() =>
            {
                // �^�X�N�����C���X���b�h�ŃL���[�ɒǉ�
                _synchronizationContext.Post(obj =>
                {
                    // ���͏��������s
                    if (Input.anyKeyDown)
                    {
                        string keyPressed = GetKeyString();
                        if (!string.IsNullOrEmpty(keyPressed) && _DownActions.ContainsKey(keyPressed))
                        {
                            Debug.Log(keyPressed);
                            foreach (Action action in _DownActions[keyPressed])
                            {
                                Debug.Log($"Invoke action for key: {keyPressed}");
                                action.Invoke();
                            }
                        }
                    }
                    else if (Input.anyKey)
                    {
                        string keyPressed = GetKeyString();
                        if (!string.IsNullOrEmpty(keyPressed) && _DownActions.ContainsKey(keyPressed))
                        {
                            Debug.Log(keyPressed);
                            foreach (Action action in _inputActions[keyPressed])
                            {
                                Debug.Log($"Invoke action for key: {keyPressed}");
                                action.Invoke();
                            }
                        }
                    }
                    

                }, null);
            });
        }
    }

    /// <summary>
    /// �������ꂽ�L�[�̕�������擾
    /// </summary>
    private string GetKeyString()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return "Space";
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            return "Return";
        if (Input.GetKeyDown(KeyCode.Backspace))
            return "Backspace";
        // ���̑��̃L�[�R�[�h������
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(code))
                return code.ToString();
        }
        return null;
    }

    /// <summary>
    /// ��~����
    /// </summary>
    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
    }
    #region subDown
    /// <summary>
    /// �L�[�R�[�h�ɑΉ�����A�N�V������o�^
    /// </summary>
    public void SubscribeWithGetkeyDown(KeyCode code, Action action)
    {
        string key = code.ToString();

        Debug.Log(key);
        if (!_DownActions.ContainsKey(key))
        {
            _DownActions[key] = new List<Action>();
        }
        _DownActions[key].Add(action);
    }

    /// <summary>
    /// �L�[�R�[�h�ɑΉ�����A�N�V�������폜
    /// </summary>
    public void UnsubscribeWithGetkeyDown(KeyCode code, Action action)
    {
        string key = code.ToString();

        if (_DownActions.ContainsKey(key))
        {
            _DownActions[key].Remove(action);
            if (_DownActions[key].Count == 0)
            {
                _DownActions.Remove(key);
            }
        }
    }
    #endregion subDwon
    #region subStay
    /// <summary>
    /// �L�[�R�[�h�ɑΉ�����A�N�V������o�^
    /// </summary>
    public void SubscribeWithGetkey(KeyCode code, Action action)
    {
        string key = code.ToString();

        Debug.Log(key);
        if (!_inputActions.ContainsKey(key))
        {
            _inputActions[key] = new List<Action>();
        }
        _inputActions[key].Add(action);
    }

    /// <summary>
    /// �L�[�R�[�h�ɑΉ�����A�N�V�������폜
    /// </summary>
    public void UnsubscribeWithGetkey(KeyCode code, Action action)
    {
        string key = code.ToString();

        if (_inputActions.ContainsKey(key))
        {
            _inputActions[key].Remove(action);
            if (_inputActions[key].Count == 0)
            {
                _inputActions.Remove(key);
            }
        }
    }
    #endregion subStay
   
}
