using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TopPlayersPanel : NetworkBehaviour
{
    [SerializeField] private RacingNetworkManager _racingNetworkManager;
    [SerializeField] private PlayerStatsPanel _playerStatsPanel;
    [SerializeField] private CheckPoint[] _checkPoints;

    private List<PlayerStatsPanel> _playerStatsPanels;

    private void OnEnable()
    {
        foreach (var item in _checkPoints)
        {
            item.Reached += RenderTopPlayers;
        }
        
    }
    private void OnDisable()
    {
        foreach (var item in _checkPoints)
        {
            item.Reached -= RenderTopPlayers;
        }
    }


    private void RenderTopPlayers(Player player)
    {
        if (player.IsFirstCheckPoint)
        {
            InitStatsPanel(player);
        }
        else
        {
            player.PlayerStatsPanel.RenderPlayerStats(player);
        }
        
    }
    private void InitStatsPanel(Player player)
    {
        var panel = Instantiate(_playerStatsPanel, transform);
        player.OvercomeFirstCheckPoint();
        player.SetPlayerStatsPanel(panel);
        panel.RenderPlayerStats(player);
    }
}
