using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Mirror;

public class CheckPoint : NetworkBehaviour
{
    [SerializeField] private TMP_Text _text; 
    public event UnityAction<Player> Reached;

    private List<Player> _players;

    [Server]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _players.Add(player);

            //player.CmdSetTimeCheckpoint(Time.realtimeSinceStartup);
            //Reached?.Invoke(player);
        }
    }

    private void RenderTopPlayers()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _text.text += (_players[i].Name + ":" + _players[i].TimeCheckPoint.ToString() + "\n");
        }

       
    }

    [Command]
    private void CmdRenderTopPlayers()
    {
        RpcRenderTopPlayers();
    }

    [ClientRpc]
    private void RpcRenderTopPlayers()
    {

    }
}
