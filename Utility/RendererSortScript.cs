using UnityEngine;
using UnityEngine.Rendering;

public class RendererSortScript : MonoBehaviour 
{
    private SortingGroup m_SortingGroup;

	// Use this for initialization
	void Start ()
    {
        m_SortingGroup = gameObject.GetComponent<SortingGroup>();
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        m_SortingGroup.sortingOrder = (int)(m_SortingGroup.transform.position.y * -100);
    }
}
