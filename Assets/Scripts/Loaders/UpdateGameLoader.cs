using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
    public delegate void OnLoadedAction(Hashtable gameUpdateData);
    public event OnLoadedAction OnLoaded;

    private UseableItem _choice;

    public UpdateGameLoader(UseableItem playerChoice)
    {
        _choice = playerChoice;
    }

    public void Load()
    {
        UseableItem opponentHand = (UseableItem)Enum.GetValues(typeof(UseableItem)).GetValue(UnityEngine.Random.Range(1, 4));

        Hashtable mockGameUpdate = new Hashtable();
        mockGameUpdate[GameConstants.resultPlayer] = _choice;
        mockGameUpdate[GameConstants.resultOpponent] = opponentHand;
        //mockGameUpdate[GameConstants.coinsAmountChange] = GetCoinsAmount(_choice, opponentHand);
        mockGameUpdate[GameConstants.gameResult] = ResultAnalyzer.GetResultState(_choice, opponentHand);

        OnLoaded(mockGameUpdate);
    }

    // its a good behaviour to have only one return in a function
    private int GetCoinsAmount(UseableItem playerHand, UseableItem opponentHand)
    {
        Result drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);
        int amountToReturn = 0;

        if (drawResult.Equals(Result.Won))
        {
            amountToReturn = 10;
        }
        else if (drawResult.Equals(Result.Lost))
        {
            amountToReturn = -10;
        }
        else
        {
            amountToReturn = 0;
        }

        return amountToReturn;
    }
}