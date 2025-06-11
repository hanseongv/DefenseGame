using System.Collections.Generic;
using Managers;
using Managers.Events;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public static GameManager Instance { get; private set; }
    public static ResourcesManager Resources = new ResourcesManager();
    public static EnemyPoolManager EnemyPool = new EnemyPoolManager();
    public static DataManager Data = new DataManager();
    
    public static EventManager Event = new EventManager();
    private List<IManager> _managers;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Init();
    }

    public void Init()
    {
        _managers = new List<IManager>()
        {
            Resources,
            EnemyPool,
            Event,
            Data,
        };
       foreach (var manager in _managers)
        {
            manager.Init();
        }

    }
}