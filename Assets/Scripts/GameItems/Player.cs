using UnityEngine;
using System.Collections;
using System;

public class Player
{
    private int _userId;
    private string _name;
    private int _coins;

    public delegate void OnCoinsAction();
    public event OnCoinsAction OnCoinsChanged;

    public Player(Hashtable playerData)
    {
        _userId = (int)playerData[GameConstants.userId];
        _name = playerData[GameConstants.name].ToString();
        _coins = (int)playerData[GameConstants.coins];
    }

    public int GetUserId()
    {
        return _userId;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void ChangeCoinAmount(int amount)
    {
        _coins += amount;
        OnCoinsChanged();
    }
}