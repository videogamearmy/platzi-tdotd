using UnityEngine;
using UnityEngine.UI;

public class Creditos : MonoBehaviour {
	public ScrollRect m_AreaDeScroll;
	float m_posicion;
	public MainMenu	m_MainMenu;
	public void OnEnable(){
		m_posicion=-0.1f;
	}
	public void Update(){
		m_posicion += Time.deltaTime / 10;
		if(m_posicion<0) m_AreaDeScroll.verticalNormalizedPosition = 1;
		else if(m_posicion>1) m_AreaDeScroll.verticalNormalizedPosition = 0;
		else m_AreaDeScroll.verticalNormalizedPosition = 1.0f - m_posicion;

		if( Input.anyKeyDown) m_MainMenu.OcultarCreditos();
	}
}
