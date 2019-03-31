using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    //teleports the trees back to the top
    public class ProceduralBackground : MonoBehaviour
    {
        public float yOffset = 22;
        public BoxCollider2D bottomCollider;

        public void UpdatePosition(Mover trigger)
        {
            Vector3 newPos = trigger.transform.position;
            newPos.y += yOffset;
            trigger.transform.position = newPos;
        }
    }
}
