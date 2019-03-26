using UnityEngine;
using UnityEngine.Tilemaps;

namespace Congehou
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        [HideInInspector]
        public BulletObject bulletPoolObject;
        [HideInInspector]
        public Camera mainCamera;

        private Pattern m_CurrentPattern;
        private CharacterController2D m_CharacterController2D;
        private PlayerCharacter m_PlayerCharacter;
        private TimeManager m_TimeManager;
        private Vector2 m_MoveVector;

        private Vector2 m_Direction;
        private Vector2 m_TargetPosition;
        private Vector3 m_OriginPosition;

        private float m_TimeBeforeAutodestruct;
        private bool m_DestroyWhenOutOfView;

        private float m_RotationVelocity;

        static readonly int VFX_HASH = VFXController.StringToHash("BulletDestroy");

        public float LifeTime {get; set;}    //Starts to count when the object is enabled

        //Constants
        const float k_OffScreenError = 0.1f;

        #region UnityCalls
        
        //Setted when the bullet is created
        private void Awake()
        {
            m_CharacterController2D = GetComponent<CharacterController2D>();
            m_PlayerCharacter = PlayerCharacter.PlayerInstance;
            m_TimeManager = m_PlayerCharacter.timeManager;
        }

        //Everytime that the bullet leaves the pool, the OnEnable is triggered
        private void OnEnable()
        {
            LifeTime = 0.0f;
            m_MoveVector = Vector2.zero;
        }

        void FixedUpdate ()
        {
            LifeTime += Time.deltaTime;

            CalculateMovement();

            m_CharacterController2D.Move(m_MoveVector);

            if (m_DestroyWhenOutOfView)
            {
                Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > -k_OffScreenError &&
                                screenPoint.x < 1 + k_OffScreenError && screenPoint.y > -k_OffScreenError &&
                                screenPoint.y < 1 + k_OffScreenError;
                if (!onScreen)
                    bulletPoolObject.ReturnToPool();
            }

            if (m_TimeBeforeAutodestruct > 0)
            {
                if (LifeTime > m_TimeBeforeAutodestruct)
                {
                    bulletPoolObject.ReturnToPool();
                }
            }
        }
        #endregion

        public void DestroyByHomura()
        {
            VFXController.Instance.Trigger(VFX_HASH, transform.position, 0, false, null);
            bulletPoolObject.ReturnToPool();
        }

        public Vector2 GetDirection()
        {
            return m_Direction;
        }

        public float GetTimeFlow()
        {
            float timeFlow = m_TimeManager.TimeFlow;
            if(m_CurrentPattern.unstoppable)
                return 1;

            if(timeFlow > 0)
            {
                return timeFlow;
            }
            else
            {
                return m_CurrentPattern.invertSpeed ? -1 : 0;
            }
        }

        public void SetDegreeDirection(float degree)
        {
            m_Direction = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
        }

        public void SetTargetDirection(Vector3 target)
        {
            m_Direction = target - transform.position;
        }

        public void SetSpawnDirection()
        {
            m_Direction = transform.position - m_OriginPosition;
        }

        //non-normalized means that the value will increase as far as the target is
        //and a normalized value will only express the direction no matter the distance
        public void SetPlayerAsTarget(bool normalize = false)
        {
            if(m_CurrentPattern.normalizedSpeed)
                m_Direction = (m_PlayerCharacter.transform.position - transform.position).normalized;
            else
                m_Direction = m_PlayerCharacter.transform.position - transform.position;
        }

        public void SetTimeToAutoDestroy(float time)
        {
            m_TimeBeforeAutodestruct = time;
        }
        
        public void SetDestroyWhenOutScreen(bool value)
        {
            m_DestroyWhenOutOfView = value;
        }

        //updates the sprite rotation
        public void UpdateRendererRotation()
        {
            spriteRenderer.transform.up = m_Direction;
        }

        //Called by the Damage Events
        public void OnHitDamageable()
        {
            //VFXController.Instance.Trigger(VFX_HASH, transform.position, 0, m_SpriteRenderer.flipX, null);
        }

        public void OnHitNonDamageable()
        {
            //VFXController.Instance.Trigger(VFX_HASH, transform.position, 0, m_SpriteRenderer.flipX, null);
        }

        //Pool methods
        public void SetPattern(Pattern pattern)
        {
            m_CurrentPattern = pattern;
        }

        //subtracts the bullet spawn position with the shooter position to find out which direction he shooted
        public void SetOriginPosition(Vector3 origin)
        {
            m_OriginPosition = origin;
        }

        public void InitializePattern()
        {
            m_CurrentPattern.InitializePattern(bulletPoolObject);
        }

        public void ReturnToPool ()
        {
            bulletPoolObject.ReturnToPool ();
        }

        //Calculations based on the pattern
        private void CalculateMovement()
        {
            m_MoveVector = m_CurrentPattern.CalculateMovement(bulletPoolObject);
        }
    }
}
