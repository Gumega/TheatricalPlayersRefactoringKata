using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Abstract;

namespace TheatricalPlayersRefactoringKata.Calculus
{
	public static class PlayCalculatorManager
	{
		private static readonly Dictionary<string, PlayCalculator> _calculatorDictionary = new()
		{
			{ "tragedy", new TragedyCalculator() },
			{ "comedy", new ComedyCalculator()  },
			{ "history", new HistoryCalculator()  },
		};

		/// <summary>
		/// Get the Calculator class and should be modified when a new genre/type is created along with the new Calculator class
		/// </summary>
		/// <param name="playType">The genre/type of play should be informed here</param>
		/// <returns>Returns the class to calculate price and credits</returns>
		/// <exception cref="ArgumentException">Thrown when a unkown genre/type of play is informed</exception>
		public static PlayCalculator GetPlayCalculator(string playType)
		{
			if (_calculatorDictionary.TryGetValue(playType, out PlayCalculator playCalculator))
				return playCalculator;
			throw new ArgumentException($"Genre {playType} is not defined.");
		}
	}
}
