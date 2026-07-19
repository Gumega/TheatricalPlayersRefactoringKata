namespace TheatricalPlayersRefactoringKata;

public class Performance(string playID, int audience)
{
	private string _playId = playID;
	private int _audience = audience;

	public string PlayId { get => _playId; set => _playId = value; }
	public int Audience { get => _audience; set => _audience = value; }
}
