using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesContanier : MonoBehaviour
{
    [SerializeField] private List<Sprite> _playerSprites;

    public Sprite GetPlayerSprite()
    {
        var playerSprite = _playerSprites[Random.Range(0, _playerSprites.Count)];

        return playerSprite;
    }
}
