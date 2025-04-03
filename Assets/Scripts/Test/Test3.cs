using System;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Unity;
using SabaSimpleDIContainer.Pipe;
using UnityEngine;
public class Test3
{
    
    private ISubscriber<string> _subscriber;
    [Injection]
    private void Sub(ISubscriber<string> subscriber)
    {
        _subscriber = subscriber;
        _subscriber.Subscribe(hoge);
    }
    private void hoge(string mes)
    {
        Debug.Log(mes);
    }
}