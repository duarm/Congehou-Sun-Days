using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Events;

namespace Congehou
{
    public class PlayerCharacter : MonoBehaviour, IGameplayActions
    {
        #region Singleton

        static protected PlayerCharacter s_PlayerInstance;
        static public PlayerCharacter PlayerInstance { get { return s_PlayerInstance; } }
        #endregion

        #region Properties and Fields

        public DamageEvent onHomuraUse;
        
        //References Settings
        public Transform cachedTransform;
        public SpriteRenderer spriteRenderer;
        public GameObject hitboxRenderer;
        public CircleCollider2D hitCollider;
        public CircleCollider2D pointCollider;
        public Animator homuraAnimator;
        public Damageable damageable;
        public TimeManager timeManager;
        
        //Hurt Settings
        public float flickeringDuration = 0.1f;
        public Transform respawnLocation;

        //Movement Settings
        public LayerMask hittableLayers;
        public bool canHitTrigger;
        public float acceleration = 100f;
        public float deceleration = 100f;
        public int chainDashFrame = 20;
        public float runSpeed = 5;
        public float walkSpeed = 3;
        public float dashSpeed = 5f;
        public float dashPressTime = .5f;

        //Audio Settings
        public AudioSource gameOverAudioPlayer;
        public AudioSource hurtAudioPlayer;
        public AudioSource homuraAudioPlayer;
        public AudioSource unityHomuraAudioPlayer;

        //Particle Setings
        public ParticleSystem deathParticle;
        public ParticleSystem spawnParticle;

        //Misc Settings
        public bool spriteOriginallyFacesLeft;

        //Animator Hashs
        private readonly int m_HashHorizontalSpeedPara = Animator.StringToHash("HorizontalSpeed");
        private readonly int m_HashVerticalSpeedPara = Animator.StringToHash("VerticalSpeed");

        private bool m_IsWalking;
        private Vector2 m_MoveVector;

        private Vector2 m_InputVector;
        private CharacterController2D m_CharacterController2D;
        private Animator m_Animator;
        private Coroutine m_FlickerCoroutine;
        private WaitForSeconds m_FlickeringWait;
        private int m_BombsLeft = 3;

        private ContactFilter2D m_HitContactFilter;
        private Collider2D[] m_HitOverlapResults = new Collider2D[5];
        private Collider2D[] m_PointOverlapResults = new Collider2D[5];

        private const int k_FrameInterval = 10;

        #endregion

        #region Unity Calls
        private void Awake()
        {
            s_PlayerInstance = this;

            m_HitContactFilter.layerMask = hittableLayers;
            m_HitContactFilter.useLayerMask = true;
            m_HitContactFilter.useTriggers = canHitTrigger;
            SceneController.Instance.inputController.Gameplay.SetCallbacks(this);

            m_CharacterController2D = GetComponent<CharacterController2D>();
            m_Animator = GetComponent<Animator>();
        }

        private void Start() 
        {
            EnableInput();
            m_FlickeringWait = new WaitForSeconds(flickeringDuration);
        }

        void FixedUpdate()
        {
            CalculateMovement();

            CheckForCollision();

            if(Time.frameCount % k_FrameInterval == 0)
                CheckForPointCollision();

            m_Animator.SetFloat(m_HashHorizontalSpeedPara, m_MoveVector.x);
            m_Animator.SetFloat(m_HashVerticalSpeedPara, m_MoveVector.y);

            UpdateFacing();
        }
        #endregion

        #region InputActions

        //Input Actions
        public void OnShoot(InputAction.CallbackContext context)
        {
            if(m_BombsLeft > 0)
            {
                CameraShaker.Shake(.2f,1);
                Homura();
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            m_InputVector = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            Dash();
        }

        public void OnWalk(InputAction.CallbackContext context)
        {
            m_IsWalking = !m_IsWalking;
        }

        public void OnSkip(InputAction.CallbackContext context)
        {
            LevelManager.Instance.StopCurrentTimeline();
        }

        public void OnCheatMode(InputAction.CallbackContext context)
        {
            damageable.SetHealth(10000);
        }
        
        public void OnStopTime(InputAction.CallbackContext context)
        {
            if(timeManager.TimeFlow == 0)
            {
                timeManager.FlowTime();
            }
            else
            {
                timeManager.StopTime();
            }
        }
        #endregion

        private void CheckForCollision()
        {
            int hitCount = hitCollider.OverlapCollider(m_HitContactFilter, m_HitOverlapResults);
            if(hitCount > 0)
                damageable.TakeDamage();
        }

        public void CheckForPointCollision()
        {
            int pointHitCount = pointCollider.OverlapCollider(m_HitContactFilter, m_PointOverlapResults);
            if(pointHitCount > 0)
                ScoreUI.GainScore(200 * pointHitCount);
        }

        public void SetMoveVector(Vector2 newMoveVector)
        {
            m_MoveVector = newMoveVector;
        }

        public void SetHorizontalMovement(float newHorizontalMovement)
        {
            m_MoveVector.x = newHorizontalMovement;
        }

        public void SetVerticalMovement(float newVerticalMovement)
        {
            m_MoveVector.y = newVerticalMovement;
        }

        public void IncrementMovement(Vector2 additionalMovement)
        {
            m_MoveVector += additionalMovement;
        }

        public void IncrementHorizontalMovement(float additionalHorizontalMovement)
        {
            m_MoveVector.x += additionalHorizontalMovement;
        }

        public void IncrementVerticalMovement(float additionalVerticalMovement)
        {
            m_MoveVector.y += additionalVerticalMovement;
        }

        private void CalculateMovement()
        {
            if(m_IsWalking)
            {
                CalculateHorizontal(walkSpeed);
                CalculateVertical(walkSpeed);
                hitboxRenderer.SetActive(true);
            }
            else
            {
                CalculateHorizontal(runSpeed);
                CalculateVertical(runSpeed);
                hitboxRenderer.SetActive(false);
            }

            m_CharacterController2D.Move(m_MoveVector * Time.unscaledDeltaTime);
        }

        private void CalculateHorizontal(float speed)
        {
            float desiredSpeed = m_InputVector.x != 0 ? m_InputVector.x * speed : 0f;
            float speedAcceleration = m_InputVector.x != 0 ? acceleration : deceleration;
            m_MoveVector.x = Mathf.MoveTowards(m_MoveVector.x, desiredSpeed, speedAcceleration * Time.deltaTime);
        }
        
        private void CalculateVertical(float speed)
        {
            float desiredSpeed = m_InputVector.y != 0 ? m_InputVector.y * speed : 0f;
            float speedAcceleration = m_InputVector.y != 0 ? acceleration : deceleration;
            m_MoveVector.y = Mathf.MoveTowards(m_MoveVector.y, desiredSpeed, speedAcceleration * Time.deltaTime);
        }

        public void EnableInput()
        {
            SceneController.Instance.inputController.Gameplay.Enable();
        }

        public void DisableInput()
        {
            SceneController.Instance.inputController.Gameplay.Disable();
        }

        public void OnHurt()
        {
            StartCoroutine(HurtCoroutine()); 
        }

        IEnumerator HurtCoroutine()
        {
            DisableInput();
            m_InputVector = Vector2.zero;

            hurtAudioPlayer.Play();
            deathParticle.Play();
            CameraShaker.Shake(.2f,.5f);

            spriteRenderer.enabled = false;
            hitboxRenderer.SetActive(false);
            gameObject.layer = 13;
            
            timeManager.FlowTime();

            damageable.EnableInvulnerability(true);
            yield return new WaitForSeconds(2.0f); //wait one second before respawing
            spawnParticle.Play();
            GameObjectTeleporter.Teleport(gameObject, respawnLocation.position);

            spriteRenderer.enabled = true;

            damageable.EnableInvulnerability();
            StartFlickering();
            EnableInput();
            yield return new WaitForEndOfFrame();
        }

        public void OnDie()
        {
            StartCoroutine(DieRespawnCoroutine());
        }

        IEnumerator DieRespawnCoroutine()
        {
            ScoreUI.SaveScore();
            BackgroundMusicPlayer.Instance.MuteJustMusic();
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.GameOver));
            yield return new WaitForSeconds(1f);
            SceneController.TransitionToScene("Menu");
        }

        public void OnEndGame()
        {
            StartCoroutine(EndCoroutine());
        }

        IEnumerator EndCoroutine()
        {
            ScoreUI.SaveScore();
            BackgroundMusicPlayer.Instance.MuteJustMusic();
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.GameOver));
            yield return new WaitForSeconds(1f);
            SceneController.TransitionToScene("Menu");
        }

        public void StartFlickering()
        {
            m_FlickerCoroutine = StartCoroutine(Flicker());
        }

        public void StopFlickering()
        {
            StopCoroutine(m_FlickerCoroutine);
            spriteRenderer.enabled = true;
        }

        protected IEnumerator Flicker()
        {
            float timer = 0f;

            while (timer < damageable.invulnerabilityDuration)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return m_FlickeringWait;
                timer += flickeringDuration;
            }

            spriteRenderer.enabled = true;
            gameObject.layer = 11;
        }

        public void UpdateFacing()
        {
            bool faceLeft = m_MoveVector.x < 0f;
            bool faceRight = m_MoveVector.x > 0f;

            if (faceLeft)
            {
                spriteRenderer.flipX = !spriteOriginallyFacesLeft;
            }
            else if (faceRight)
            {
                spriteRenderer.flipX = spriteOriginallyFacesLeft;
            }
        }

        public void UpdateFacing(bool faceLeft)
        {
            if (faceLeft)
            {
                spriteRenderer.flipX = !spriteOriginallyFacesLeft;
            }
            else
            {
                spriteRenderer.flipX = spriteOriginallyFacesLeft;
            }
        }

        public void Dash()
        {
            SetMoveVector(dashSpeed * m_InputVector);
        }

        public void Homura()
        {
            m_BombsLeft--;
            if(onHomuraUse != null)
                onHomuraUse.Invoke(m_BombsLeft);
            
            homuraAudioPlayer.Play();
            unityHomuraAudioPlayer.Play();
            homuraAnimator.SetTrigger("Active");

            Collider2D[] m_HomuraOverlapResults = Physics2D.OverlapCircleAll(transform.position, 3f, hittableLayers);
            int destroyed = 0;

            for(int i = 0; i < m_HomuraOverlapResults.Length; i++)
            {
                if(m_HomuraOverlapResults[i].gameObject.activeInHierarchy)
                {
                    m_HomuraOverlapResults[i]?.GetComponent<Bullet>().DestroyByHomura();
                    destroyed++;
                }
            }

            ScoreUI.GainScore(destroyed * 2000);
        }
    }
}