using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player :  NetworkBehaviour
{
    [SerializeField] private SpritesContanier _spritesContanier;
    [SerializeField] private string _name;

    private PlayerStatsPanel _playerStatsPanel;
    private float _timeCheckPoint;
    private bool _isFirstedCheckPoint = true;

    public PlayerStatsPanel PlayerStatsPanel => _playerStatsPanel;

    public bool IsFirstCheckPoint => _isFirstedCheckPoint;

    public float TimeCheckPoint => _timeCheckPoint;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.GetComponent<CameraFollow>().SetPlayer(this);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _spritesContanier.GetPlayerSprite();
        }
    }

    public void OvercomeFirstCheckPoint()
    {
        _isFirstedCheckPoint = false;
    }


    public void SetTimeCheckpoint(float time)
    {
        _timeCheckPoint = time;
    }

    public void SetPlayerStatsPanel(PlayerStatsPanel panel)
    {
        _playerStatsPanel = panel;
    }


}
