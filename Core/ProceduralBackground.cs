using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBackground : MonoBehaviour
{
    public GameObject upperCollider;
    public BoxCollider2D bottomCollider;
    
    private float spawnArea;

    private void Start() 
    {
        spawnArea = upperCollider.transform.localScale.x;
    }

    public void UpdatePosition(TriggerCall trigger)
    {
        trigger.transform.position = upperCollider.transform.position + transform.right * Random.Range(-spawnArea * 0.5f, spawnArea * 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(upperCollider.transform.position, new Vector3(spawnArea, 0.4f, 0));
    }
}
