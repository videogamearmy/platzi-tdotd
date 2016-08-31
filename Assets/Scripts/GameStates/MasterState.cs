using UnityEngine;
using System.Collections;

public class MasterState : BaseStateInstance<MasterState> {
	// Use this for initialization
	float m_timer = 0;
	float m_duration = 0.5f;
	public override void Start (MainGame game) { 
		game.m_MainCamera.transform.LookAt(game.m_Master.transform);
		// Añade un nuevo paso.
		game.StartSteps();
	}
	// Update is called once per frame
	public override void Update (MainGame game) { 
		// Presenta los paso que tiene que hacer.
		m_timer += Time.deltaTime;
		if(m_timer>m_duration){
			m_timer = 0;
			if(!game.ShowNextStep()){
				BaseState.Change(GotoPlayerState.Instance);
			}
		}
	}

	public override void End(MainGame game){
		game.HideStep();
	}
}
