using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCreator : NetworkBehaviour
{
    [SerializeField] private GameObject _playerNameInputField;
    [SerializeField] private GameObject _spawnReadyButton;

    private static PlayerCreator _playerCreator;
    private NetworkConnection _conn;
    private string _playerName;

    public string PlayerName => _playerName;

    public static PlayerCreator PlayerCreatorSingltone
    {
        get
        {
            return _playerCreator;
        }
    }
    private void Awake()
    {
        _playerCreator = this;
    }


    public void SetNamePlayer(string name)
    {
        _playerName = name;
    }


    public void SpawnObjectsTogle()
    {
        _playerNameInputField.SetActive(!_playerNameInputField.activeSelf);
        _spawnReadyButton.SetActive(!_spawnReadyButton.activeSelf);
    }
    public void SetNetworkConection(NetworkConnection conn)
    {
        _conn = conn;
    }

    public void SpawnPlayer()
    {
        ClientScene.AddPlayer(_conn);
        SpawnObjectsTogle();
    }
}
