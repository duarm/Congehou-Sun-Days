using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public Transform iconParent;

    private GameObject[] images;
    
    private void Awake() 
    {
        images = new GameObject[iconParent.childCount];
        for(int i = 0; i < images.Length; i++)
        {
            images[i] = iconParent.GetChild(i).gameObject;
        }
    }

    public void ChangeHitPointUI (int currentValue)
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(currentValue >= i + 1);
        }
    }
}
