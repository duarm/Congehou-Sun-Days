using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class EnemyBehaviour : MonoBehaviour, ITimeObject
    {
        [Header("References")]

        public Transform bulletSpawnPoint;


        [Header("Enemy Settings")]
        [Tooltip("The time the enemy takes to shoot")]
        public bool shouldShoot = false;
        public bool burstShot = false;
        public int burstBullets = 5;
        public float burstGap = .4f;
        public float shootingGap = 2f;

        [HideInInspector]
        public Enemy enemyPoolObject; //Hide variables, set by the pool

        //Privates
        private Transform m_Transform;
        private WaitForSeconds m_BurstBulletGap;
        private BulletInfo m_BulletInfo;
        private float m_BulletRotationSpeed;
        private float m_NextShotTime;
        private bool m_TimeStopped = false;
        private bool m_ShouldMove = true;

        private TimeManager m_TimeManager;

        private Vector2 m_SpawnPointPosition;
        private float m_TimeBeforeAutodestruct;
        private float m_ReturnToPoolTimer;

        public delegate void ReturnToPoolCallBack();
        public ReturnToPoolCallBack onReturnToPool;

        //Animator Hashs
        static readonly int VFX_HASH = VFXController.StringToHash("EnemyDeath");

        public float LifeTime { get; set; }    //Starts to count when the object is enabled


        #region UnityCalls

        private void Awake()
        {
            m_TimeManager = PlayerCharacter.PlayerInstance.timeManager;
            m_SpawnPointPosition = bulletSpawnPoint.position;
        }

        private void Start()
        {
            m_Transform = transform;
            m_BurstBulletGap = new WaitForSeconds(burstGap);
            m_BulletRotationSpeed = m_BulletInfo.pattern.rotationSpeed;
        }

        private void OnEnable()
        {
            TimeManager.RegisterTimeAffectedObject(this);
            
            //technically we shoot, it will just take some years. Allow to avoid to add more test & special case.
            if(shouldShoot)
                m_NextShotTime = 1;
            else
                m_NextShotTime = float.MaxValue;

            LifeTime = 0.0f;
            m_ReturnToPoolTimer = 0.0f;
        }

        private void OnDisable()
        {
            TimeManager.UnregisterTimeAffectedObject(this);
        }

        void FixedUpdate()
        {
            m_ReturnToPoolTimer += Time.deltaTime;

            if(m_BulletInfo.pattern.moveOnlyOnStop)
            {
                if (m_TimeStopped)
                {
                    m_ShouldMove = true;
                    UpdateBehaviour();
                }
                else
                    m_ShouldMove = false;
            }
            else
            {
                if(!m_TimeStopped)
                {
                    m_ShouldMove = true;
                    UpdateBehaviour();
                }
                else
                    m_ShouldMove = false;
            }

            if (m_TimeBeforeAutodestruct > 0)
            {
                if (m_ReturnToPoolTimer > m_TimeBeforeAutodestruct)
                {
                    enemyPoolObject.ReturnToPool();
                    if(onReturnToPool != null)
                        onReturnToPool.Invoke();
                }
            }
        }
        #endregion

        public void UpdateBehaviour()
        {
            LifeTime += Time.deltaTime;

            UpdateBulletSpawnPoint();
            CheckShootingTimer();
        }

        public void SetBulletInfo(BulletInfo info)
        {
            m_BulletInfo = info;
        }

        public void SetTimeToAutoDestroy(float time)
        {
            m_TimeBeforeAutodestruct = time;
        }

        public void CheckShootingTimer()
        {
            if(LifeTime >= m_NextShotTime)
            {
                m_NextShotTime = LifeTime + shootingGap;
                if(burstShot)
                    StartCoroutine(Burst());
                else
                    Shoot();
            }
        }

        IEnumerator Burst()
        {
            int currentBullets = 0;
            while(currentBullets < burstBullets)
            {
                if(m_ShouldMove)
                {
                    currentBullets++;
                    Shoot();
                }
                yield return m_BurstBulletGap;
            }
        }


        private void Shoot()
        {
            m_BulletInfo.pool.Pop(bulletSpawnPoint.position, m_Transform.position, m_BulletInfo.pattern);
        }
        
        private void UpdateBulletSpawnPoint()
        {
            if(!m_BulletInfo.pattern.autoRotate)
                return;

            Vector3 rotation = m_Transform.position;
            rotation.x += Mathf.Cos(LifeTime * m_BulletRotationSpeed);
            rotation.y += Mathf.Sin(LifeTime * m_BulletRotationSpeed);
            bulletSpawnPoint.position = rotation;
        }

        public void OnTimeStop()
        {
            m_TimeStopped = true;
        }

        public void OnTimeFlow()
        {
            m_TimeStopped = false;
        }

        public void SectionCall()
        {}
    }
}
