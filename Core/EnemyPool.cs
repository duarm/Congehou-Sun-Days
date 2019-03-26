using System.Collections;
using UnityEngine;

namespace Congehou
{
    public class EnemyPool : ObjectPool<EnemyPool, Enemy, Vector2, BulletInfo>
    {
        [Header("Pool Settings")]
        public bool destroyWhenOutOfView = true;
        [Tooltip("If -1 never auto destroy, otherwise bullet is return to pool when that time is reached")]
        public float timeBeforeAutodestruct = -1.0f;

        public float removalDelay = .5f;

        void Start()
        {
            for (int i = 0; i < initialPoolCount; i++)
            {
                Enemy newEnemy = CreateNewPoolObject();
                pool.Add(newEnemy);
            }
        }

        public override void Push(Enemy poolObject)
        {
            poolObject.inPool = true;
            poolObject.Sleep();
        }
    }

    public class Enemy : PoolObject<EnemyPool, Enemy, Vector2, BulletInfo>
    {
        public EnemyBehaviour enemyBehaviour;

        protected WaitForSeconds m_RemoveWait;

        protected override void SetReferences()
        {
            enemyBehaviour = instance.GetComponent<EnemyBehaviour>();
            enemyBehaviour.SetTimeToAutoDestroy(objectPool.timeBeforeAutodestruct);
            enemyBehaviour.enemyPoolObject = this;

            m_RemoveWait = new WaitForSeconds(objectPool.removalDelay);
        }

        public override void WakeUp(Vector2 position, BulletInfo bulletInfo)
        {
            enemyBehaviour.SetBulletInfo(bulletInfo);
            instance.transform.position = position;
            instance.SetActive(true);
        }

        public override void Sleep()
        {
            instance.SetActive(false);
        }

        protected void ReturnToPoolEvent()
        {
            objectPool.StartCoroutine(ReturnToPoolAfterDelay());
        }

        protected IEnumerator ReturnToPoolAfterDelay()
        {
            yield return m_RemoveWait;
            ReturnToPool();
        }
    }
}
