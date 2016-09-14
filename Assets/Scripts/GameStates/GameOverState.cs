using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverState : BaseStateInstance<GameOverState> {
	float m_timer = 0;
	float m_duration = 5.0f;

	// Use this for initialization
	public override void Start (MainGame game) { 
		m_timer = 0;
		game.GameOver();
	}
	// Update is called once per frame
	public override void Update (MainGame game) { 
		m_timer += Time.deltaTime;
		if(m_timer>m_duration) {
			m_timer=m_duration;
			SceneManager.LoadScene("MainMenu");
		}
	}
}
