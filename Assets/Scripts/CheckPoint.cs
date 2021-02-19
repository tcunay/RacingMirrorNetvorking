using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class CheckPoint : NetworkBehaviour
{
    public event UnityAction<Player> Reached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.SetTimeCheckpoint(Time.realtimeSinceStartup);
            Reached?.Invoke(player);
        }
    }
}
