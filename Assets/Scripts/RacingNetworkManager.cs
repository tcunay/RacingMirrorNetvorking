using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class RacingNetworkManager : NetworkManager
{
    [SerializeField] private GameObject _spawnGroupContanier;

    private string _playerName;

    public event UnityAction StartedGame;
    

    public string PlayerName { get; private set; }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Time.timeScale = 0;
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        base.OnServerAddPlayer(conn);
       // SpawnGroupToogle();

       // if (string.IsNullOrWhiteSpace(_playerName))

            if (numPlayers >= 1)
        {
            NetworkTime.Reset();
            Time.timeScale = 1;
            StartedGame?.Invoke();
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        //_playerCreator.SpawnPlayer()

        //base.OnClientConnect(conn);
        // OnClientConnect by default calls AddPlayer but it should not do
        // that when we have online/offline scenes. so we need the
        // clientLoadedScene flag to prevent it.
        if (!clientLoadedScene) //_playerCreator.SpawnPlayer()
        {
            // Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
            if (!ClientScene.ready) ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {
                ClientScene.AddPlayer(conn);
                //NetworkClient.connection.identity.GetComponent<Player>().CmdSetName("SDGFSDF");         //_playerCreator.GetName()
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

    public void SpawnGroupToogle()
    {
        _spawnGroupContanier.SetActive(!_spawnGroupContanier.activeSelf);
    }
}
