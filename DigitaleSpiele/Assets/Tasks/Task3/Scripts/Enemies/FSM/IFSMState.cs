
public interface IFSMState<T> {
	void Enter(T entity);
	void Reason(T entity);
	void Update(T entity);
	void Exit(T entity);
}