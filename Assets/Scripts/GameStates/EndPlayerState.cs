using UnityEngine;
using System.Collections;

public class EndPlayerState : BaseStateInstance<EndPlayerState> {
	// Use this for initialization
	float m_timer = 0;
	float m_duration = 1.0f;
	public override void Start (MainGame game) { 
		m_timer = 0;
	}
	// Update is called once per frame
	public override void Update (MainGame game) { 
// Presenta los paso que tiene que hacer.
		m_timer += Time.deltaTime;
		if(m_timer>m_duration )
			BaseState.Change(GotoMasterState.Instance);
	}
	public override void End(MainGame game){
		game.ReiniciaPaso();
	}

}
