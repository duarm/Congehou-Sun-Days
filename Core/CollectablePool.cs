using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class CollectablePool : ObjectPool<CollectablePool, CollectableObject, Vector2>
    {

        [Header("Pool Settings")]
        public bool destroyWhenOutOfView = true;
        [Tooltip("If -1 never auto destroy, otherwise bullet is return to pool when that time is reached")]
        public float timeBeforeAutodestruct = -1.0f;

    }

    public class CollectableObject : PoolObject<CollectablePool, CollectableObject, Vector2>
    {
        public Transform transform;
        public Collectable collectable;

        protected override void SetReferences()
        {
            transform = instance.transform;
            collectable = instance.GetComponent<Collectable>();
            collectable.collectablePoolObject = this;
        }

        public override void WakeUp(Vector2 position)
        {
            transform.position = position;
            instance.SetActive(true);
        }

        public override void Sleep()
        {
            instance.SetActive(false);
        }
    }
}