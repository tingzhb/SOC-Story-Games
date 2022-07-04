using System.Threading.Tasks;

public interface ICommand{
	public Task ExecuteAsync();
	public void Undo();
}
