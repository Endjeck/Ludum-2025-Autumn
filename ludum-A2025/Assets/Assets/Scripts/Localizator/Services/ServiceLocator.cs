using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator _instance;
    public static ServiceLocator Container =>
        _instance ?? (_instance = new ServiceLocator());

    private readonly Dictionary<Type, IGameService> _gameServices =
        new Dictionary<Type, IGameService>();

    public void RegisterSingle<T>(T service) where T : IGameService
    {
        if (_gameServices.ContainsKey(typeof(T)))
            throw new InvalidOperationException();
        else
            _gameServices.Add(typeof(T), service);
    }

    public T Get<T>() where T : IGameService
    {
        if (!_gameServices.ContainsKey(typeof(T)))
            throw new InvalidOperationException(typeof(T).ToString());

        return (T)_gameServices[typeof(T)];
    }

    public void Unregister<T>() where T : IGameService
    {
        if (!_gameServices.ContainsKey(typeof(T)))
            throw new InvalidOperationException();

        _gameServices.Remove(typeof(T));
    }

    public void UnregisterAll()
    {
        _gameServices.Clear();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
