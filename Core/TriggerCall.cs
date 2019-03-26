using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCall : MonoBehaviour
{
    public ProceduralBackground proceduralBackground;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("BottomCollider"))
        {
            proceduralBackground.UpdatePosition(this);
        }
    }
}
