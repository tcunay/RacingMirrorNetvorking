using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Mirror;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckPoint : NetworkBehaviour
{

    [SerializeField] private TMP_Text _text; 
    [SerializeField] private List<string> _playersName = new List<string>();
    [SerializeField] private List<float> _playersCheckPointTime = new List<float>();


    [Server]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Debug.Log("Checkpoint");
            player.SetTimeCheckpoint();

            _playersName.Add(player.Name);
            _playersCheckPointTime.Add(player.TimeCheckPoint);

            RenderTopPlayers(player);
        }
    }

    [Server]
    private void RenderTopPlayers(Player player)
    {
        NetworkIdentity playerIdentity = player.GetComponent<NetworkIdentity>();

        var text = "";
        RpcRenderTopPlayers(playerIdentity.connectionToClient, text);
        for (int i = 0; i < _playersName.Count; i++)
        {
            if (_playersName[i] != null && i == 0)
            {
                text += ((i + 1).ToString() + "- " + _playersName[i] + " : " + _playersCheckPointTime[i].ToString() + "\n");
            }
            else
            {
                text += ((i + 1).ToString() + "- " + _playersName[i] + " :  +" + (_playersCheckPointTime[i] - _playersCheckPointTime[0]).ToString() + "\n");
            }

            RpcRenderTopPlayers(playerIdentity.connectionToClient, text);
        }
    }

    [TargetRpc]
    private void RpcRenderTopPlayers(NetworkConnection target, string text)
    {
        _text.text = text;
    }
}
