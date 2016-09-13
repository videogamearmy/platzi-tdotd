using UnityEngine;
using System.Collections;

public class GotoMasterState : BaseStateInstance<GotoMasterState> {
	float m_timer = 0;
	float m_duration = 2.0f;
	Vector3 m_currentTarget;
	// Use this for initialization
	public override void Start (MainGame game) { 
		m_timer = 0;
		float distance = (game.m_MainCamera.transform.position-game.m_Master.transform.position).magnitude;
		m_currentTarget =game.m_MainCamera.transform.position + game.m_MainCamera.transform.forward * distance;  
	}
	// Update is called once per frame
	public override void Update (MainGame game) { 
		m_timer += Time.deltaTime;
		if(m_timer>m_duration) {
			m_timer=m_duration;
			BaseState.Change(MasterState.Instance);
		}else{ 
			m_currentTarget = Vector3.Lerp(m_currentTarget, game.m_Master.transform.position, m_timer / m_duration);
			game.m_MainCamera.transform.LookAt(m_currentTarget);
		}
		
	}
}
