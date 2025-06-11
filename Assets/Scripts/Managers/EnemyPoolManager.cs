using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemyPoolManager : IManager
    {
        private GameObject enemyPrefab;
        private int poolSize = 10;
        private Queue<Enemy> pool = new Queue<Enemy>();
        private Transform poolParent;

        public void Init()
        {
            poolParent = new GameObject("EnemyPool").transform;
            enemyPrefab = GameManager.Resources.GetResources<GameObject>("Enemy");
            for (int i = 0; i < poolSize; i++)
            {
                InstantiateEnemy();
            }
        }

        private void InstantiateEnemy()
        {
            var obj = Object.Instantiate(enemyPrefab,poolParent);
            obj.SetActive(false);
            pool.Enqueue(obj.GetComponent<Enemy>());
        }

        public Enemy Get()
        {
            if (pool.Count == 0)
            {
                InstantiateEnemy();
            }

            var enemy = pool.Dequeue();
            enemy.gameObject.SetActive(true);
            return enemy;
        }

        public void Return(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            pool.Enqueue(enemy);
        }
    }
}