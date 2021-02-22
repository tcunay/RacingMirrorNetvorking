using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class RacingNetworkManager : NetworkManager
{
    [SerializeField] private PlayerCreator _playerCreator;
    [SerializeField] private Game _game;

    private string _playerName;

    public event UnityAction StartedGame;
    
    public string PlayerName { get; private set; }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        base.OnServerAddPlayer(conn);
        _game.ChangeQuantityPlayers();
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        if (!clientLoadedScene)
        {
            // Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
            if (!ClientScene.ready) ClientScene.Ready(conn);
            {
                _playerCreator.SetNetworkConection(conn);
                _playerCreator.SpawnObjectsTogle();
            }
        }
    }

    public void SetPlayerName(string name)
    {
        _playerName = name;
    }

    public string GetName()
    {
        return _playerName;
    }
}
