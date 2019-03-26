using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class BulletPool : ObjectPool<BulletPool, BulletObject, Vector2, Vector2, Pattern>
    {

        [Header("Pool Settings")]
        public bool destroyWhenOutOfView = true;
        [Tooltip("If -1 never auto destroy, otherwise bullet is return to pool when that time is reached")]
        public float timeBeforeAutodestruct = -1.0f;

        static protected Dictionary<GameObject, BulletPool> s_PoolInstances = new Dictionary<GameObject, BulletPool>();

        private void Awake()
        {
            //This allow to make Pool manually added in the scene still automatically findable & usable
            if(prefab != null && !s_PoolInstances.ContainsKey(prefab))
                s_PoolInstances.Add(prefab, this);
        }

        private void OnDestroy()
        {
            s_PoolInstances.Remove(prefab);
        }

        //initialPoolCount is only used when the objectpool don't exist
        static public BulletPool GetObjectPool(GameObject prefab, int initialPoolCount = 10)
        {
            BulletPool objPool = null;
            if (!s_PoolInstances.TryGetValue(prefab, out objPool))
            {
                GameObject obj = new GameObject(prefab.name + "_Pool");
                objPool = obj.AddComponent<BulletPool>();
                objPool.prefab = prefab;
                objPool.initialPoolCount = initialPoolCount;

                s_PoolInstances[prefab] = objPool;
            }

            return objPool;
        }
    }

    public class BulletObject : PoolObject<BulletPool, BulletObject, Vector2, Vector2, Pattern>
    {
        public Transform transform;
        public Bullet bullet;

        protected override void SetReferences()
        {
            transform = instance.transform;
            bullet = instance.GetComponent<Bullet>();
            bullet.SetTimeToAutoDestroy(objectPool.timeBeforeAutodestruct);
            bullet.SetDestroyWhenOutScreen(objectPool.destroyWhenOutOfView);
            bullet.bulletPoolObject = this;
            bullet.mainCamera = Object.FindObjectOfType<Camera> ();
        }

        public override void WakeUp(Vector2 position, Vector2 shooterPosition, Pattern pattern)
        {
            transform.position = position;
            bullet.SetPattern(pattern);
            bullet.SetOriginPosition(shooterPosition);
            bullet.InitializePattern();
            instance.SetActive(true);
        }

        public override void Sleep()
        {
            instance.SetActive(false);
        }
    }

    public class BulletInfo
    {
        public BulletPool pool;
        public Pattern pattern;

        public BulletInfo(BulletPool pool, Pattern pattern)
        {
            this.pool = pool;
            this.pattern = pattern;
        }
    }
}