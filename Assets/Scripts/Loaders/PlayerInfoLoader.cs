using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
	public delegate void OnLoadedAction(Hashtable playerData);
	public event OnLoadedAction OnLoaded;

	public void Load()
	{
		Hashtable mockPlayerData = new Hashtable();
		mockPlayerData[GameConstants.userId] = 1;
		mockPlayerData[GameConstants.name] = "Player 1";
		mockPlayerData[GameConstants.coins] = 50;

		OnLoaded(mockPlayerData);
	}
}