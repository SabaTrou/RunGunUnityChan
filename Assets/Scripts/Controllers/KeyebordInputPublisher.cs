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
    /// 起動
    /// </summary>
    public void Start(SynchronizationContext context)
    {
        // メインスレッドのSynchronizationContextを設定
        _synchronizationContext = context;

        // キャンセル用のトークンソース
        _cancellationTokenSource = new CancellationTokenSource();

        // キャンセル用のトークン
        CancellationToken token = _cancellationTokenSource.Token;

        // 全てのKeyCodeを初期化
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            _DownActions[code.ToString()] = new List<Action>();
        }

        // 入力処理を開始
        MonitorInputAsync(token);
    }

    private async void MonitorInputAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            // Unityのメインスレッドで動作するようにSynchronizationContextを使用
            await Task.Run(() =>
            {
                // タスクをメインスレッドでキューに追加
                _synchronizationContext.Post(obj =>
                {
                    // 入力処理を実行
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
    /// 押下されたキーの文字列を取得
    /// </summary>
    private string GetKeyString()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return "Space";
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            return "Return";
        if (Input.GetKeyDown(KeyCode.Backspace))
            return "Backspace";
        // その他のキーコードを処理
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(code))
                return code.ToString();
        }
        return null;
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
    }
    #region subDown
    /// <summary>
    /// キーコードに対応するアクションを登録
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
    /// キーコードに対応するアクションを削除
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
    /// キーコードに対応するアクションを登録
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
    /// キーコードに対応するアクションを削除
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
