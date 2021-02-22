using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class TimeManager : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float _currentTime;

    private static TimeManager _timeManager;

    public event UnityAction GameStarted;

    private bool _isStartGame = false;

    public static TimeManager TimeManagerSingltone
    {
        get
        {
            return _timeManager;
        }
    }

    private void Awake()
    {
        _timeManager = this;
    }

    private void Update()
    {
        TickTime(_isStartGame);
    }

    [Server]
    private void TickTime(bool startGame)
    {
        if (_isStartGame)
        {
            _currentTime += Time.deltaTime;
        }
    }

    public float GetTime()
    {
        return _currentTime;
    }

    [Server]
    public void BeginTickTime()
    {
        _isStartGame = true;
        GameStarted?.Invoke();
    }

}
