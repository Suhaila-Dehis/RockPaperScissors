using UnityEngine;
using System.Collections;

public enum Result
{
	Won,
	Lost,
	Draw
}

public class ResultAnalyzer
{
	public static Result GetResultState(UseableItem playerHand, UseableItem enemyHand)
	{
		if (IsStronger(playerHand, enemyHand))
		{
			return Result.Won;
		}
		else if (IsStronger(enemyHand, playerHand))
		{
			return Result.Lost;
		}
		else
		{
			return Result.Draw;
		}
	}

	private static bool IsStronger (UseableItem firstHand, UseableItem secondHand)
	{
		switch (firstHand)
		{
			case UseableItem.Rock:
			{
				switch (secondHand)
				{
					case UseableItem.Scissors:
						return true;
					case UseableItem.Paper:
						return false;
				}
				break;
			}
			case UseableItem.Paper:
			{
				switch (secondHand)
				{
					case UseableItem.Rock:
						return true;
					case UseableItem.Scissors:
						return false;
				}
				break;
			}
			case UseableItem.Scissors:
			{
				switch (secondHand)
				{
					case UseableItem.Paper:
						return true;
					case UseableItem.Rock:
						return false;
				}
				break;
			}
		}

		return false;
	}
}