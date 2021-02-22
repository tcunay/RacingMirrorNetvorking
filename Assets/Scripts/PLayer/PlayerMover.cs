using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : NetworkBehaviour
{

    [SerializeField] private float _speedForce;

    [SerializeField] private float _rotateForce;

    private Rigidbody2D _rigidbody;

    [SyncVar]
    private bool _permissionToMove;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (!_permissionToMove) return;

        Move(GetDirectionToMove());
        Turn(GetDirectionToRotate());
    }

    [Client]
    private void Move(Vector2 direction)
    {

        _rigidbody.AddForce(direction.normalized * _speedForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    [Client]
    private void Turn(Vector2 direction)
    {
        _rigidbody.AddForce(direction.normalized * _rotateForce * Time.deltaTime);
    }

    [Client]
    private Vector2 GetDirectionToMove()
    {
        var direction = Vector2.zero;
        if (Input.GetKey(KeyCode.Space))
            direction = Vector2.up;

        return direction;
    }

    [Client]
    private Vector2 GetDirectionToRotate()
    {
        var directionX = Input.GetAxis("Horizontal");
        var direction = new Vector2(directionX, 0);
        return direction;
    }

    [Server]
    public void GivePermissionToMove()
    {
        _permissionToMove = true;
    }

}
