using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraFollow : MonoBehaviour
{
    private Player _player;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_player != null)
        transform.position = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
