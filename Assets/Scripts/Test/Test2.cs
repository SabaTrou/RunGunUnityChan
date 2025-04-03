using UnityEngine;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Unity;

public class Test2:IStartable
{
    void IStartable.Start()
    {
        Debug.Log("start test2");
    }
}