using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    [CreateAssetMenu(menuName = "Patterns/Wave Pattern")]
    public class WavePattern : Pattern
    {
        //[ReadOnly]
        public PatternType type = PatternType.Wave;

        public float frequency = 10f;
        public float magnitude = 0.5f;
        public float speed = 2f;

        public override void InitializePattern(BulletObject objectPool)
        {
            if(aimWithRotation)
                objectPool.bullet.SetSpawnDirection();
            else
                objectPool.bullet.SetDegreeDirection(startRotation);
        }

        public override Vector2 CalculateMovement(Enemy enemy)
        {
            return Wave(enemy);
        }

        public Vector2 Wave(Enemy objectPool)
        {
            Vector2 direction = objectPool.enemyBehaviour.transform.right * Mathf.Sin(objectPool.enemyBehaviour.LifeTime * frequency) * magnitude;
            Vector2 movement = -objectPool.enemyBehaviour.transform.up * Time.deltaTime * speed;
            return direction + movement;
        }

        public override Vector2 CalculateMovement(BulletObject bullet)
        {
            return Wave(bullet);
        }

        public Vector2 Wave(BulletObject objectPool)
        {
            Vector2 direction = objectPool.bullet.GetDirection();
            Vector2 crossDirection = new Vector2(direction.normalized.y, -direction.normalized.x);
            Vector2 movement = (direction * Mathf.Sin(objectPool.bullet.LifeTime * frequency) * magnitude) + (crossDirection * Time.deltaTime * speed);
            return movement;
        }
    }
}
