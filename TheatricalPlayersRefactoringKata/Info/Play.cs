namespace TheatricalPlayersRefactoringKata.Info;

public class Play(string name, int lines, string type)
{
	private string _name = name;
	private int _lines = lines;
	private string _type = type;

	public string Name { get => _name; set => _name = value; }
	public int Lines { get => _lines; set => _lines = value; }
	public string Type { get => _type; set => _type = value; }
}
