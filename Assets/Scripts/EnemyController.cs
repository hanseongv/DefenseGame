using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints;

    public List<Enemy> enemies;
    private List<Enemy> removeEnemies = new List<Enemy>();

    private void OnEnable()
    {
        GameManager.Event.OnDieEnemy += OnDieEnemy;
    }

    private void OnDisable()
    {
        GameManager.Event.OnDieEnemy -= OnDieEnemy;
    }

    private void OnDieEnemy(Enemy enemy)
    {
        removeEnemies.Add(enemy);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var info = new Digimon()
            {
                id = "koromon",
            };
            SpawnEnemy(info);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Enemy randomEnemy = (enemies.Count > 0) ? enemies[Random.Range(0, enemies.Count)] : null;

            if (randomEnemy != null)
            {
                randomEnemy.Die();
            }
        }
        foreach (var enemy in enemies)
        {
            enemy.Move();
        }

        CleanupEnemies();
    }

    private void CleanupEnemies()
    {
        if (removeEnemies.Count == 0) return;
        foreach (var enemy in removeEnemies)
        {
            enemies.Remove(enemy);
            GameManager.EnemyPool.Return(enemy);
        }

        removeEnemies.Clear();
    }

    private void SpawnEnemy(Digimon info)
    {
        var enemy = GameManager.EnemyPool.Get();
        enemy.Spawn(waypoints, info);
        enemies.Add(enemy);
    }
}