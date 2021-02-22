using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Player :  NetworkBehaviour
{
    [SerializeField] private SpritesContanier _spritesContanier;

    [SerializeField] private TMP_Text _nameText; 

    [SyncVar]
    [SerializeField] private string _name;

    [SyncVar]
    private float _timeCheckPoint;
    private bool _isFirstedCheckPoint = true;

    public bool IsFirstCheckPoint => _isFirstedCheckPoint;

    
    public float TimeCheckPoint => _timeCheckPoint;

    public string Name => _name;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        if (isLocalPlayer)
        {
            SetName();
            Camera.main.GetComponent<CameraFollow>().SetPlayer(this);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _spritesContanier.GetPlayerSprite();
        }
    }

    public void SetTimeCheckpoint()
    {
        _timeCheckPoint = TimeManager.TimeManagerSingltone.GetTime();
    }

    [Client]
    private void SetName()
    {
        var name = PlayerCreator.PlayerCreatorSingltone.PlayerName;
        CmdSetName(name);
    }

    [Command]
    private void CmdSetName(string name)
    {
        _name = name;
    }

}
