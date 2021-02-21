using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimeManager : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float _currentTime;

    private static TimeManager _timeManager;

    private bool _isStartGame= true;

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
            //Debug.Log(_currentTime);
        }
    }

    public float GetTime()
    {
        return _currentTime;
    }

    public void BeginTickTime()
    {
        _isStartGame = true;
    }
}
