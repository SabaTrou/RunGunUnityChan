using System;
using System.Collections.Generic;
using SabaSimpleDIContainer;
using SabaSimpleDIContainer.Pipe;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionableLib
{
    #region　変数
    private ISubscriber<CollisionableAddEvent> _collisionableAddEvent;
    private Dictionary<GameObject, ICollisionable2D> _library=new();
    public static CollisionableLib collisionableLib;
    public CollisionableLib()
    {
        collisionableLib = this;
        GetAllCollisionable();
    }
    #endregion
    [Injection]
    private void OnCollisionAdded(ISubscriber<CollisionableAddEvent> subscriber)
    {
        _collisionableAddEvent = subscriber;
        _collisionableAddEvent.Subscribe(RegisterLibrary);
    }
    private void RegisterLibrary(CollisionableAddEvent addEvent)
    {
        _library[addEvent.collisionable.gameObject] = addEvent.collisionable;
       
    }

    public bool TryGetCollisionable(GameObject gameObject,out ICollisionable2D collisionable)
    {
        bool ret= _library.TryGetValue(gameObject, out collisionable);
        if (!ret)
        {
            Debug.Log(gameObject.transform.name);   
        }
        return ret;
    }
    /// <summary>
    /// 最初に使う
    /// </summary>
    private void GetAllCollisionable()
    {
        foreach (ICollisionable2D collisionable in FindObjectsByInterface<ICollisionable2D>())
        {
            _library[collisionable.gameObject] = collisionable;

        }
    }
    public static List<T> FindObjectsByInterface<T>() where T : class
    {
        List<T> results = new();
        MonoBehaviour[] monoBehaviours = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is T targetInterface)
            {
                results.Add(targetInterface);

            }
        }

        return results;
    }

}