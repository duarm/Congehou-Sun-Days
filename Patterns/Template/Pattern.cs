using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public enum PatternType
    {
        Circular,
        AimPlayer,
        Straight,
        Wave
    }

    /// <summary>
    /// Base pattern script, contains all the calculations that the bullet uses to move
    /// </summary>
    public class Pattern : ScriptableObject
    {
        [Header("Properties")]
        public bool autoRotate;
        public bool aimWithRotation;
        public float rotationSpeed;
        [Range(0,359)]
        public int startRotation;

        [Header("Speed")]
        public bool normalizedSpeed;

        [Header("Time")]
        public bool invertSpeed;
        public bool unstoppable;
        public bool moveOnlyOnStop;

        public virtual void InitializePattern(Enemy enemy){}

        public virtual void InitializePattern(BulletObject bulletObject){}
        
        public virtual Vector2 CalculateMovement(Enemy enemy)
        { 
            Debug.LogWarning("Pattern is not implemented for Enemy");
            return Vector2.zero; 
        }
        public virtual Vector2 CalculateMovement(BulletObject bulletObject)
        {
            Debug.LogWarning("Pattern is not implemented for Bullet");
            return Vector2.zero; 
        }
    }
}
