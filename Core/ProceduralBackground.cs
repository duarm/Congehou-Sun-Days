using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class ProceduralBackground : MonoBehaviour
    {
        public BoxCollider2D bottomCollider;

        public void UpdatePosition(Mover trigger)
        {
            Vector3 newPos = trigger.transform.position;
            newPos.y += 22;
            trigger.transform.position = newPos;
        }
    }
}
