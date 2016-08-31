public class BaseState{
	public virtual void Start (MainGame game) {
	
	}
	public virtual void Update (MainGame game) {
	
	}
	public virtual void End (MainGame game) {
	
	}

	static MainGame m_game;
	static BaseState m_current;
	public static void SetGame(MainGame mainGame){
		m_game = mainGame;
	}
	public static void Change(BaseState newState){
		if(m_current!=null) m_current.End(m_game);
		m_current = newState;
		if(m_current!=null) m_current.Start(m_game);
	}
	public static void Update(){
		if(m_current!=null) m_current.Update(m_game);
	}

}
public class BaseStateInstance<T>:BaseState where T:BaseState, new(){
	static T _Instance;
	public static T Instance{ get{ if(_Instance==null) _Instance= new T(); return _Instance; } }
}
