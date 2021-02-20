using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimeManager : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float _currentTime;

    private bool _isStartGame= false;

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
            Debug.Log(_currentTime);
        }
    }

    private void BeginTickTime()
    {
        _isStartGame = true;
    } 
}
