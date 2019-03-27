using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    [CreateAssetMenu(menuName = "Patterns/Circular Pattern")]
    public class CircularPattern : Pattern
    {
        //[ReadOnly]
        public PatternType type = PatternType.Circular;

        public float width = 1f;
        public float height = 1f;
        [Tooltip("positive to move clockwise, negative to move counter-clockwise")]
        public int speed = 1;

        public override void InitializePattern(BulletObject objectPool)
        {
            if(aimWithRotation)
                objectPool.bullet.SetSpawnDirection();
        }

        public override Vector2 CalculateMovement(Enemy enemy)
        {
            return Circular(enemy);
        }

        public Vector2 Circular(Enemy enemy)
        {
            return new Vector2(CalculateX(enemy) , CalculateY(enemy));
        }

        private float CalculateX(Enemy enemy)
        {
            return Mathf.Cos(enemy.enemyBehaviour.LifeTime) * width * speed;
        }

        private float CalculateY(Enemy enemy)
        {
            return Mathf.Sin(enemy.enemyBehaviour.LifeTime) * height * speed;
        }
    }
}
