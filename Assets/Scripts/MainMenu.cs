using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject m_Creditos;
	public GameObject m_Menu;
	void Start(){
		OcultarCreditos();
	}
	public void OnPlay(){
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
