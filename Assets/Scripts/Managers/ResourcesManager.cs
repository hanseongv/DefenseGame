using UnityEngine;

namespace Managers
{
    public class ResourcesManager:IManager
    {
        public T GetResources<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public T[] GetResourcesAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path); // 폴더 내의 모든 리소스를 로드
        }

        public GameObject GetResources(string path)
        {
            return Resources.Load<GameObject>(path);
        }

        public void Init()
        {
            
        }
    }
}