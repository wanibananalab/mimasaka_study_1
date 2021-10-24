using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameAssetCache<T> where T : MonoBehaviour
    {
        private List<T> Caches { get; } = new List<T>();
        private List<T> Uses { get; } = new List<T>();

        private T Target { get; }
        private Transform Parent { get; }

        public T[] GetUses()
        {
            return Uses.ToArray();
        }

        public GameAssetCache(T target, Transform parent)
        {
            Target = target;
            Parent = parent;
        }

        public T Create()
        {
            T instance = null;
            if (Caches.Count > 0)
            {
                instance = Caches.First();
                Caches.Remove(instance);
            }
            else
            {
                instance = Object.Instantiate(Target, Parent, false);
            }

            instance.gameObject.SetActive(true);
            Uses.Add(instance);
            return instance;
        }

        public void Remove(T instance)
        {
            Caches.Add(instance);
            Uses.Remove(instance);
            instance.gameObject.SetActive(false);
        }

        public void RemoveAll(IEnumerable<T> instances)
        {
            foreach (var instance in instances)
            {
                Remove(instance);
            }
        }
    }
}