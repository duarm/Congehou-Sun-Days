using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    [CreateAssetMenu(menuName = "Patterns/Straight Pattern")]
    public class StraightPattern : Pattern
    {
        //[ReadOnly]
        public PatternType type = PatternType.Straight;

        public float speed = 5f;

        public override void InitializePattern(BulletObject objectPool)
        {
            if(aimWithRotation)
                objectPool.bullet.SetSpawnDirection();
            else
                objectPool.bullet.SetDegreeDirection(startRotation);
                
            objectPool.bullet.UpdateRendererRotation();
        }

        public override Vector2 CalculateMovement(BulletObject objectPool)
        {
            return Straight(objectPool);
        }

        public Vector2 Straight(BulletObject objectPool)
        {
            if(!moveOnlyOnStop)
                return objectPool.bullet.GetDirection() * (speed * Time.deltaTime * objectPool.bullet.GetTimeFlow());
            else
            {
                if(objectPool.bullet.GetTimeFlow() > 0)
                    return Vector2.zero;
                else
                    return objectPool.bullet.GetDirection() * (speed * Time.deltaTime);
            }
        }
    }
}
