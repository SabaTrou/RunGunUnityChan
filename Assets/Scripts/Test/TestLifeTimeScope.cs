using System.Collections;
using System.Collections.Generic;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using SabaSimpleDIContainer.Unity;
using UnityEngine;

public class TestLifeTimeScope : LifeTimeScope
{
    [SerializeField]
    private Test test;
    protected override void Configure(IContainer container)
    {
        container.RegisterComponent(test);
        
        container.RegisterEntryPoint<Test2>();
        container.Register<Test3>(LifeTime.singleton);
        container.RegisterBroker<string>();
    }
}
