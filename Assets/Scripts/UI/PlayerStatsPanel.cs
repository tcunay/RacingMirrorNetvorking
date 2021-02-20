using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PlayerStatsPanel : NetworkBehaviour
{
    [SerializeField] private TMP_Text _playerStats;

    
    public void RenderPlayerStats(Player player)
    {
        _playerStats.text = player.Name + " : " + player.TimeCheckPoint.ToString();
    }
}
