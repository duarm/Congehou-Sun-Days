using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Sort a sprite based on his Y position
/// </summary>
public class RendererSortScript : MonoBehaviour 
{
    private SortingGroup m_SortingGroup;

	void Start ()
    {
        m_SortingGroup = gameObject.GetComponent<SortingGroup>();
    }
	
	void LateUpdate ()
    {
        m_SortingGroup.sortingOrder = (int)(m_SortingGroup.transform.position.y * -100);
    }
}
