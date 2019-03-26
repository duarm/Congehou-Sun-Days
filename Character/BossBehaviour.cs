using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    //the boss will randomly or not move to a waypoint, in which he'll randomly or not cast a pattern, and the process repeat
    public class BossBehaviour : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("The lower the value, the faster it moves")]
        public float smoothTime = 1f;
        [Tooltip("Counted as soon as the attack pattern end")]
        public float nextWaypointDelay = 2f;
        public bool randomizeWaypoints;
        public bool randomizePatterns;
        public List<WaypointWrapper> waypoints;
        public List<Collider2D> hitboxes;
        public Animator animator;

        private Vector2 m_SmoothSpeed;
        private Transform m_WaypointTarget;
        private float m_WaypointTimer;
        private float m_NextWaypointTime;
        private bool m_Moving;
        private int m_CurrentWaypointIndex;

        private void Awake() 
        {
            m_NextWaypointTime = nextWaypointDelay;
            m_CurrentWaypointIndex = 0;
        }

        private void FixedUpdate()
        {
            UpdateTimers();
            if(waypoints != null)
                UpdateMovement();
        }

        private void UpdateTimers()
        {
            if (m_WaypointTimer > 0.0f)
                m_WaypointTimer -= Time.deltaTime;
        }

        private void UpdateMovement()
        {
            if(m_WaypointTimer > 0)
                return;
            
            if(m_Moving)
            {
                transform.position = Vector2.SmoothDamp(transform.position, m_WaypointTarget.position, ref m_SmoothSpeed, smoothTime);
                if(transform.position == m_WaypointTarget.position)
                {
                    m_Moving = false;
                    m_WaypointTimer = nextWaypointDelay;
                }
            }
            else
            {
                if(randomizeWaypoints)
                {
                    int choice = Random.Range(0, waypoints.Count);
                    m_WaypointTarget = waypoints[choice].waypoint;
                }

                m_Moving = true;
            }
        }

        public void Die()
        {

        }

    }

    [System.Serializable]
    public class WaypointWrapper
    {
        public Transform waypoint;
        public List<Pattern> patterns;
    }
}
