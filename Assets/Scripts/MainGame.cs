using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainGame : MonoBehaviour {
	public Camera m_MainCamera;
	public GameObject m_Master;
	public Animator m_MasterAnimator;
	public GameObject m_Player;
	public Animator m_PlayerAnimator;
	public GameObject m_Flecha;
	public GameObject m_Mal;
	int m_nVidas;
	public GameObject[] m_Vidas;
	public Text m_Puntuacion;
	public Image m_Empezar;
	public AudioSource MyAudioSource;
	// Use this for initialization
	void Start () {
		ReiniciaJuego();

		AddNewStep();
		AddNewStep();
		AddNewStep();
		
		BaseState.SetGame(this);
		BaseState.Change(MasterState.Instance);

//		BaseState.Change(GotoMasterState.Instance);
	}
	
	// Update is called once per frame
	void Update () {
		BaseState.Update();
		UpdateEmpezar();
	}
	public enum PasoTipo{ Right, Up, Left, Down, None };
	static Quaternion[] PasosBase = new Quaternion[]{ Quaternion.Euler(0,0,0f), Quaternion.Euler(0,0,90f), Quaternion.Euler(0,0,180f), Quaternion.Euler(0,0,270f)};
	public AudioClip[] PasosSonidos;
	List<PasoTipo> m_Pasos = new List<PasoTipo>();
	List<PasoTipo>.Enumerator m_pasoActual;

	public void AddNewStep(){
		m_Pasos.Add( (PasoTipo)Random.Range(0, 4 ) );
	}

	public void StartSteps(){
		m_pasoActual = m_Pasos.GetEnumerator();
	}
	public bool ShowNextStep(Animator _animator){
		if(m_pasoActual.MoveNext() ){
			m_Flecha.SetActive(true);
			int paso = (int)m_pasoActual.Current;
			_animator.SetInteger("Paso", paso+1);			
			MyAudioSource.PlayOneShot(PasosSonidos[paso]);
			m_Flecha.transform.rotation = PasosBase[paso];
			return true;
		}
		_animator.SetInteger("Paso", 0);
		return false;
	}
	public void ReiniciaJuego(){
		m_nVidas = 3;
		ActualizaVidas();
		m_misPuntos = 0;
		m_Pasos.Clear();
		ReiniciaPaso();
	}
	public void ReiniciaPaso(){
		m_Mal.SetActive(false);
		m_Flecha.SetActive(false);
		m_Empezar.enabled=false;	
	}
	float m_timerEmpezar;
	public void MuestraEmpezar(){
		PlayEffect(MainGame.Efectos.Empezar1,MainGame.Efectos.Empezar2);
		m_Empezar.enabled=true;
		m_Empezar.color = Color.white;
		m_timerEmpezar = 2; 
	}
	public void UpdateEmpezar(){
		if(m_Empezar.enabled){
			m_timerEmpezar-=Time.deltaTime*2;
			if(m_timerEmpezar<0) { m_timerEmpezar=0; m_Empezar.enabled=false; } 
			Color tmp = m_Empezar.color;
			tmp.a = m_timerEmpezar;  
			m_Empezar.color = tmp;
		}
	}
	// Interacciones de usuario.
	List<PasoTipo> m_UserSteps = new List<PasoTipo>();
	public void AddUserStep(PasoTipo _tipo){
		m_UserSteps.Add(_tipo);
	}
	public bool CheckUserStep(Animator _animator, PasoTipo _tipo){
		if(m_pasoActual.MoveNext() ){
			m_Flecha.SetActive(true);
			int paso = (int)_tipo;
			_animator.SetInteger("Paso", paso+1);
			m_Flecha.transform.rotation = PasosBase[paso];
			if(m_pasoActual.Current==_tipo){
				_animator.Update(0);
				_animator.SetInteger("Paso", 0);
				MyAudioSource.PlayOneShot(PasosSonidos[paso]);
				return true;
			}
			else{
				_animator.SetInteger("Paso", 0);
				return false;
			}
		}
		_animator.SetInteger("Paso", 0);
		return false;
	}
	public bool IsEnd(){
		var copia = m_pasoActual;
		return !copia.MoveNext();
	}

	public enum Efectos{
		Empezar1, Empezar2, Bien1, Bien2, Bien3, Mal1, Mal2, Ganar, Perder
	};
	public AudioClip[] EfectosSonidos;
	public void PlayEffect(params Efectos[] efectos){
	int indice = Random.Range(0,efectos.Length);
		MyAudioSource.PlayOneShot(EfectosSonidos[indice]);
	}

	int m_misPuntos=0;
	public void SumaPuntuacion(int puntos){
		m_misPuntos+=puntos;
		m_Puntuacion.text =m_misPuntos.ToString("D6"); 
	}

	void ActualizaVidas(){
		int i=0;
		for(i=0;i<m_nVidas;++i) m_Vidas[i].SetActive(true);
		for( ;i<3;++i) m_Vidas[i].SetActive(false);
	}

	public void MalPaso(){
		m_Mal.SetActive(true);
		m_PlayerAnimator.SetInteger("Paso", 5);
		m_PlayerAnimator.Update(0);
		m_PlayerAnimator.SetInteger("Paso", 0);
		
		// Quita vida!
		m_nVidas--;
		ActualizaVidas();
		if(m_nVidas==0){
			// Fin juego.
			SceneManager.LoadScene("MainMenu");
		}
		else
			BaseState.Change(EndPlayerState.Instance);
		
	}
}
