using UnityEngine.Events;

namespace Managers.Events
{
    public class EventManager : IManager
    {
        public event UnityAction<Enemy> OnDieEnemy;
        public void OnDieEnemyHandle(Enemy enemy) => OnDieEnemy?.Invoke(enemy);

        public void Init()
        {
        }
    }
}