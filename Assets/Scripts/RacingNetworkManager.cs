using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class RacingNetworkManager : NetworkManager
{
    public event UnityAction StartedGame;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        Time.timeScale = 0;
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        

        //GameObject player = Instantiate(playerPrefab);
        //NetworkServer.AddPlayerForConnection(conn, player);
        //_players.Add(player.GetComponent<Player>());
        
        if (numPlayers >= 1)
        {
            NetworkTime.Reset();
            Time.timeScale = 1;
            StartedGame?.Invoke();
        }
    }
}
