using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    [CreateAssetMenu(menuName = "Patterns/Aim Player Pattern")]
    public class AimPlayerPattern : Pattern
    {
        //[ReadOnly]
        public PatternType type = PatternType.AimPlayer;

        public float speed = 1f;

        public override void InitializePattern(BulletObject objectPool)
        {
            objectPool.bullet.SetPlayerAsTarget(true);
            objectPool.bullet.UpdateRendererRotation();
        }

        public override Vector2 CalculateMovement(BulletObject bulletObject)
        {
            return AimPlayer(bulletObject);
        }

        public Vector2 AimPlayer(BulletObject objectPool)
        {
            return objectPool.bullet.GetDirection() * speed * Time.deltaTime * objectPool.bullet.GetTimeFlow();
        }
    }
}
