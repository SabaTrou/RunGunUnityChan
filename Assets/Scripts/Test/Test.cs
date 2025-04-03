using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer.Unity;

public class Test : MonoBehaviour
{
    [Injection]
    private IPublisher<string> _publisher;
    [Injection]
    private Test3 test3;
    [Injection]
    private void GetAllStartable(IStartable[] startables)
    {
        Debug.Log(startables.Length);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("InputSpace"+_publisher);
            
            _publisher.Publish(new string("message"));
        }
    }
}
