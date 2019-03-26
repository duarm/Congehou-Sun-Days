using UnityEngine;

public class RendererSortScript : MonoBehaviour 
{
    private SpriteRenderer m_TempRenderer;

	// Use this for initialization
	void Start ()
    {
        m_TempRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        m_TempRenderer.sortingOrder = (int)(m_TempRenderer.transform.position.y * -100);
    }
}
