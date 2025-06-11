using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<Tile> tiles = new List<Tile>();

    [SerializeField]
    private Button btnSpawnEgg;

    private GameObject eggPrefab;

    private void Start()
    {
        btnSpawnEgg.onClick.AddListener(BtnSpawnEggClick);
        eggPrefab = GameManager.Resources.GetResources("Egg");
    }

    public Tile GetRandomEmptyTile()
    {
        var emptyTiles = tiles.Where(t => !t.IsOccupied).ToList();

        if (emptyTiles.Count == 0)
        {
            Debug.LogWarning("빈 타일이 없습니다!");
            return null;
        }

        return emptyTiles[Random.Range(0, emptyTiles.Count)];
    }

    private void BtnSpawnEggClick()
    {
        var tile = GetRandomEmptyTile();
        if (tile == null) return;
        Digimon digimon = GameManager.Data.GetRandomDigimon();

        Debug.Log(digimon.name_kor);
        var egg = Instantiate(eggPrefab, tile.GetPosition, Quaternion.identity);
        tile.SetOccupant(egg);
        egg.GetComponent<Egg>().StartHatch();
    }
}