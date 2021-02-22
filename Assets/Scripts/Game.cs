using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Game : NetworkBehaviour
{
    [SerializeField] private int _requiredQuantityPlayers;

    [SyncVar]
    private int _currentQuantityPlayers;

    //public event UnityAction<int> GameStarted;

    [Server]
    public void ChangeQuantityPlayers()
    {
        _currentQuantityPlayers++;
        if(_currentQuantityPlayers == _requiredQuantityPlayers)
        {
            StartGame();
            //RpcStartGame();
        }
    }

    [Server]
    public void StartGame()
    {
        TimeManager.TimeManagerSingltone.BeginTickTime();
    }

    //[ClientRpc]
    //private void RpcStartGame()
    //{
    //    var timeScale = 1;
    //    //Time.timeScale = 1;
    //    GameStarted?.Invoke(timeScale);
    //}

}
