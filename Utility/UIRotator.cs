using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotator : MonoBehaviour
{
    public float rotateRate = .03f;
    private WaitForSeconds m_RotateRate;
    private RectTransform m_RectTransform;
    private int m_CurrentRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_CurrentRotation = (int)m_RectTransform.eulerAngles.z;

        m_RotateRate = new WaitForSeconds(rotateRate);
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while(true)
        {
            m_RectTransform.eulerAngles = new Vector3(0, 0, m_CurrentRotation);
            m_CurrentRotation--;
            if(m_CurrentRotation == 0)
                m_CurrentRotation = 360;
            yield return m_RotateRate;
        }
    }
}
