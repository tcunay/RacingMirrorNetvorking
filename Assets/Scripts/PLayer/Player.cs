using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Player :  NetworkBehaviour
{
    [SerializeField] private SpritesContanier _spritesContanier;
    [SerializeField] private TMP_Text _nameText; 

    [SyncVar(hook = nameof(OnNameChanged))]
    private string _name;

    private SpriteRenderer _spriteRenderer;

    [SyncVar]
    private float _timeCheckPoint;
    
    public float TimeCheckPoint => _timeCheckPoint;

    public string Name => _name;

    private void Start()
    {
        if (isLocalPlayer)
        {
            SetName();

            Camera.main.GetComponent<CameraFollow>().SetPlayer(this);

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _spritesContanier.GetPlayerSprite();

        }       
        else
        {
           // _nameText.text = _name;
        }
    }

    private void OnNameChanged(string oldValue, string newValue)
    {
        _nameText.text = newValue;
    }

    private void OnEnable()
    {
        TimeManager.TimeManagerSingltone.GameStarted += BeginGame;
    }
    private void OnDisable()
    {
        TimeManager.TimeManagerSingltone.GameStarted -= BeginGame;
    }

    [Server]
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
        RpcSetName(name);
    }

    [ClientRpc]
    private void RpcSetName(string name)
    {
        _nameText.text = name;
        Debug.LogError("_nameText.text = " + _nameText.text);
    }

    [Server]
    private void BeginGame()
    {
        var playerMover = GetComponent<PlayerMover>();
        playerMover.GivePermissionToMove();
    }

    //[Server]
    //private void ServerSetName(string name)
    //{
    //    _name = name;
    //    _nameText.text = _name;
    //    RpcSetIconNameValue();
    //}

    //[ClientRpc]
    //private void RpcSetIconNameValue()
    //{
    //    Debug.Log("RpcSetName");
    //    _nameText.text = _name;
    //}

}
