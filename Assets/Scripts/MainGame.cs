using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainGame : MonoBehaviour {
	public Camera m_MainCamera;
	public GameObject m_Master;
	public GameObject m_Player;

	public GameObject m_Arrow;
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
	public enum StepType{ Right, Up, Left, Down };
	static Quaternion[] BaseSteps = new Quaternion[]{ Quaternion.Euler(0,0,0f), Quaternion.Euler(0,0,90f), Quaternion.Euler(0,0,180f), Quaternion.Euler(0,0,270f)};

	List<StepType> m_Steps = new List<StepType>();
	List<StepType>.Enumerator m_currentStep;

	public void AddNewStep(){
		m_Steps.Add( (StepType)Random.Range(0, 4 ) );
	}

	public void StartSteps(){
		m_currentStep = m_Steps.GetEnumerator();
	}

	public bool ShowNextStep(){
		if(m_currentStep.MoveNext() ){
			m_Arrow.SetActive(true);		
			m_Arrow.transform.rotation = BaseSteps[(int)m_currentStep.Current];
			return true;
		}
		return false;
	}
	public void HideStep(){
		m_Arrow.SetActive(false);
	}
}
