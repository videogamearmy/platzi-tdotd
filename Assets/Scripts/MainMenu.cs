using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject m_Creditos;
	public GameObject m_Menu;
	public GameObject m_BotonJugar;
	public GameObject m_TextoCargando;
	void Start(){
		m_TextoCargando.SetActive(false);
		OcultarCreditos();
	}
	public void OnPlay(){
		m_BotonJugar.SetActive(false);
		m_TextoCargando.SetActive(true);
		SceneManager.LoadScene("Game");
	}

	public void MostarCreditos(){
		m_Creditos.SetActive(true);
		m_Menu.SetActive(false);
		
	}
	public void OcultarCreditos(){
		m_Creditos.SetActive(false);
		m_Menu.SetActive(true);
		
	}
}
