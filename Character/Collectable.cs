using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class Collectable : MonoBehaviour
    {
        public float gravity = 10;
        public float horizontalAcceleration = 100;
        
        [HideInInspector]
        public CollectableObject collectablePoolObject;

        private float m_TimeBeforeAutodestruct;
        private CharacterController2D m_CharacterController2D;
        private Vector3 m_MoveVector;

        private void Awake()
        {
            m_CharacterController2D = GetComponent<CharacterController2D>();
        }

        private void OnEnable() 
        {
            m_MoveVector = new Vector2(Random.Range(-7,7),Random.Range(5,7));
        }

        // Update is called once per frame
        void Update()
        {
            CalculateMovement();
            m_CharacterController2D.Move(m_MoveVector * Time.deltaTime);

            if (m_TimeBeforeAutodestruct > 0)
            {
                if (Time.time > m_TimeBeforeAutodestruct)
                {
                    collectablePoolObject.ReturnToPool();
                }
            }
        }

        private void CalculateMovement()
        {
            float desiredSpeed = 0;
            float speedAcceleration = horizontalAcceleration;
            m_MoveVector.x = Mathf.MoveTowards(m_MoveVector.x, desiredSpeed, speedAcceleration * Time.deltaTime);
            m_MoveVector.y -= gravity * Time.deltaTime;
        }
    }   
}
