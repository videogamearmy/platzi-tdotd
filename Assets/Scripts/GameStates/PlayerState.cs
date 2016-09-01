using UnityEngine;
using System.Collections;

public class PlayerState : BaseStateInstance<PlayerState> {
	// Use this for initialization
	public override void Start (MainGame game) { 
		game.m_MainCamera.transform.LookAt(game.m_Player.transform);
	}
	// Update is called once per frame
	public override void Update (MainGame game) { 

	}
}
