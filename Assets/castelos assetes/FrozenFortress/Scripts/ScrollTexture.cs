using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class ScrollTexture : MonoBehaviour 
{
	[SerializeField]
	private float m_XValue = 0.0f;
	[SerializeField]
	private float m_YValue = 0.0f;

	private MeshRenderer m_mesh = null;

	void Awake()
	{
		m_mesh = gameObject.GetComponent<MeshRenderer> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_mesh == null) 
		{
			return;
		}
		float x = Time.time * m_XValue;
		float y = Time.time * m_YValue;
		m_mesh.material.mainTextureOffset = new Vector2 (x, y);
	}
}
