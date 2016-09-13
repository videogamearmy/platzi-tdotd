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
			MainGame.PasoTipo paso = ControlTeclado();

			if(paso != MainGame.PasoTipo.None){
				m_timer = 0;
				if(game.CheckUserStep(game.m_PlayerAnimator, paso)){
					// Añadimos puntuacion.
					game.SumaPuntuacion(100);
					// BIEN!!!
					if(game.IsEnd() ) {
						game.PlayEffect(MainGame.Efectos.Bien1,MainGame.Efectos.Bien2,MainGame.Efectos.Bien3);
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

	MainGame.PasoTipo ControlTeclado(){
			if( Input.GetKeyDown(KeyCode.DownArrow) ) 		return MainGame.PasoTipo.Down;
			else if( Input.GetKeyDown(KeyCode.LeftArrow) )	return MainGame.PasoTipo.Left;
			else if( Input.GetKeyDown(KeyCode.RightArrow) ) return MainGame.PasoTipo.Right;
			else if( Input.GetKeyDown(KeyCode.UpArrow) ) 	return MainGame.PasoTipo.Up;
			//return MainGame.PasoTipo.None;
			return ControlGesto();
	}
	Vector2 m_Start;
	MainGame.PasoTipo ControlGesto(){
		if(Input.touchCount>0){
		Touch touch = Input.touches[0];
			if( touch.phase == TouchPhase.Began )
				m_Start = touch.position;
			if( touch.phase == TouchPhase.Ended ){
				Vector2 delta = touch.position - m_Start;
				if( Mathf.Abs(delta.x)>Mathf.Abs(delta.y) ){
				// Movimientos horizontales
					if( delta.x<0) return MainGame.PasoTipo.Left;
					else return MainGame.PasoTipo.Right;
				} else{
				// Movimientos verticales
					if( delta.y>0) return MainGame.PasoTipo.Up;
					else return MainGame.PasoTipo.Down;
				}
			}
		}
		return MainGame.PasoTipo.None;
	}	
}
