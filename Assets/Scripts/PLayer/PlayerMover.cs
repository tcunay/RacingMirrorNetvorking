using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float _speedForce;
    [SyncVar]
    [SerializeField] private float _rotateForce;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CmdMove(GetDirectionToMove());
        CmdTurn(GetDirectionToRotate());
    }

    [Command]
    private void CmdMove(Vector2 direction)
    {
        if (isClient)
        {
            _rigidbody.AddForce(direction.normalized * _speedForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    [Command]
    private void CmdTurn(Vector2 direction)
    {
        if(isClient)
        _rigidbody.AddForce(direction.normalized * _rotateForce * Time.deltaTime);
    }

    private Vector2 GetDirectionToMove()
    {
        var direction = Vector2.zero;
        if (Input.GetKey(KeyCode.Space))
            direction = Vector2.up;

        return direction;
    }

    private Vector2 GetDirectionToRotate()
    {
        var directionX = Input.GetAxis("Horizontal");
        var direction = new Vector2(directionX, 0);
        return direction;
    }
}
