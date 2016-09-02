using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainGame : MonoBehaviour {
	public Camera m_MainCamera;
	public GameObject m_Master;
	public Animator m_MasterAnimator;
	public GameObject m_Player;
	public Animator m_PlayerAnimator;
	public GameObject m_Flecha;
	public GameObject[] m_Vidas;
	public Text m_Score;
	// Use this for initialization
	void Start () {
		HideStep();

		AddNewStep();
		AddNewStep();
		AddNewStep();
		
		BaseState.SetGame(this);
		BaseState.Change(MasterState.Instance);


	}
	
	// Update is called once per frame
	void Update () {
		BaseState.Update();
	}
	public enum PasoTipo{ Right, Up, Left, Down };
	static Quaternion[] PasosBase = new Quaternion[]{ Quaternion.Euler(0,0,0f), Quaternion.Euler(0,0,90f), Quaternion.Euler(0,0,180f), Quaternion.Euler(0,0,270f)};

	List<PasoTipo> m_Pasos = new List<PasoTipo>();
	List<PasoTipo>.Enumerator m_pasoActual;

	public void AddNewStep(){
		m_Pasos.Add( (PasoTipo)Random.Range(0, 4 ) );
	}

	public void StartSteps(){
		m_pasoActual = m_Pasos.GetEnumerator();
	}

	public bool ShowNextStep(Animator animator){
		if(m_pasoActual.MoveNext() ){
			m_Flecha.SetActive(true);
			int paso = (int)m_pasoActual.Current;
			animator.SetInteger("Paso", paso+1);
			m_Flecha.transform.rotation = PasosBase[paso];
			return true;
		}
		animator.SetInteger("Paso", 0);
		return false;
	}
	public void HideStep(){
		m_Flecha.SetActive(false);
	}
}
