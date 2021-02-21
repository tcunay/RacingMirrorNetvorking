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
        if(isServer)
        CmdSetName();
        if (isLocalPlayer)
        {
            Camera.main.GetComponent<CameraFollow>().SetPlayer(this);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _spritesContanier.GetPlayerSprite();
        }
    }

    [Command]
    public void CmdOvercomeFirstCheckPoint()
    {
        _isFirstedCheckPoint = false;
    }

    
    public void SetTimeCheckpoint()
    {
        _timeCheckPoint = TimeManager.TimeManagerSingltone.GetTime();
    }

    [Command]
    private void CmdSetName()
    {
        _name = PlayerCreator.PlayerCreatorSingltone.PlayerName;
        //RpcSetName();
        //_nameText.text = _name;
    }

    //[ClientRpc]
    //private void RpcSetName()
    //{
    //    _name = PlayerCreator.PlayerCreatorSingltone.PlayerName;
    //}


}
