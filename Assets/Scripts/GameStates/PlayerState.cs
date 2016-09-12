using UnityEngine;
using System.Collections;

public class PlayerState : BaseStateInstance<PlayerState> {
	// Use this for initialization
	float m_timer = 0;
	float m_duration = 2.0f;
	public override void Start (MainGame game) { 		
		m_timer = 0;
		game.StartSteps();
		game.MuestraEmpezar();
		game.m_MainCamera.transform.LookAt(game.m_Player.transform);
	}
	// Update is called once per frame
	public override void Update (MainGame game) { 
// Presenta los paso que tiene que hacer.
		m_timer += Time.deltaTime;
		if(m_timer<m_duration ){
			MainGame.PasoTipo paso = MainGame.PasoTipo.None;

			if( Input.GetKeyDown(KeyCode.DownArrow) ) 		paso = MainGame.PasoTipo.Down;
			else if( Input.GetKeyDown(KeyCode.LeftArrow) )	paso = MainGame.PasoTipo.Left;
			else if( Input.GetKeyDown(KeyCode.RightArrow) ) paso = MainGame.PasoTipo.Right;
			else if( Input.GetKeyDown(KeyCode.UpArrow) ) 	paso = MainGame.PasoTipo.Up;

			if(paso!=MainGame.PasoTipo.None){
				m_timer = 0;
				if(game.CheckUserStep(game.m_PlayerAnimator, paso)){
					// Añadimos puntuacion.
					game.SumaPuntuacion(100);
					game.PlayEffect(MainGame.Efectos.Bien1,MainGame.Efectos.Bien2,MainGame.Efectos.Bien3);
					// BIEN!!!
					if(game.IsEnd() ) {
						// Mete un paso mas y...
						game.AddNewStep();
						// Va al maestro.
						BaseState.Change(EndPlayerState.Instance);
					}					
					return;
				}
				else{
					game.MalPaso();
				}
			}else
				return;
		}
		else{
			game.MalPaso();
		}
	}
}
