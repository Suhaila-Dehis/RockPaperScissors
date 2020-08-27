using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public Text playerHand;
    public Text enemyHand;

    #region Set in the Inspector

    [SerializeField]
    private Text _nameLabel; // set in the isnpector instead of Find for better performance
    [SerializeField]
    private Text _moneyLabel; // set in the isnpector instead of Find for better performance
    #endregion

    [SerializeField]
    private InputField bettingAmount;


    private Player _player;
    private PlayerInfoLoader _playerInfoLoader;
    private int winsCount = 0;

    //void Awake()
    //{
        // instead of Find which hurts performance, i made the variables as SerializeField and set them in the inspector
        //_nameLabel = transform.Find ("Canvas/Name").GetComponent<Text>();
        //_moneyLabel = transform.Find ("Canvas/Money").GetComponent<Text>();
    //}

    void Start()
    {
        _playerInfoLoader = new PlayerInfoLoader();
        _playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
        _playerInfoLoader.Load();
        bettingAmount.text = "10";
    }

    // must remove the subscribed delegates when not needed anymore
    private void OnDestroy()
    {
        _playerInfoLoader.OnLoaded -= OnPlayerInfoLoaded;
        _player.OnCoinsChanged -= UpdateCoinsHud;
    }

    public void OnPlayerInfoLoaded(Hashtable playerData)
    {
        _player = new Player(playerData);
        _nameLabel.text = "Name: " + _player.GetName();
        _player.OnCoinsChanged += UpdateCoinsHud;
        UpdateCoinsHud();
    }

    public void UpdateCoinsHud()
    {
        // calculate the amount to add to player coins

        _moneyLabel.text = "Money: $" + _player.GetCoins().ToString();
    }

    public void HandlePlayerInput(int item)
    {
        UseableItem playerChoice = UseableItem.None;

        switch (item)
        {
            case 1:
                playerChoice = UseableItem.Rock;
                break;
            case 2:
                playerChoice = UseableItem.Paper;
                break;
            case 3:
                playerChoice = UseableItem.Scissors;
                break;
        }

        UpdateGame(playerChoice);
    }

    private void UpdateGame(UseableItem playerChoice)
    {
        UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice);
        updateGameLoader.OnLoaded += OnGameUpdated;
        updateGameLoader.Load();
    }

    public void OnGameUpdated(Hashtable gameUpdateData)
    {
        Color myColor, oponentColor;
        int amountToAddToCoins = 0;
        if (gameUpdateData[GameConstants.gameResult].Equals(Result.Won)) // i win 
        {
            amountToAddToCoins = int.Parse(bettingAmount.text);
            myColor = Color.green;
            oponentColor = Color.red;
            winsCount++;


        }
        else if (gameUpdateData[GameConstants.gameResult].Equals(Result.Lost))// i last round
        {
            amountToAddToCoins = -1 * int.Parse(bettingAmount.text);
            myColor = Color.red;
            oponentColor = Color.green;
            winsCount = 0;
        }
        else // its a draw
        {
            myColor = Color.white;
            oponentColor = Color.white;
        }
        playerHand.text = DisplayResultAsText((UseableItem)gameUpdateData[GameConstants.resultPlayer]);
        playerHand.color = myColor;

        enemyHand.text = DisplayResultAsText((UseableItem)gameUpdateData[GameConstants.resultOpponent]);
        enemyHand.color = oponentColor;
        if (winsCount == 3)
        {
            _player.ChangeCoinAmount(amountToAddToCoins * 2);
        }
        else
        {
            _player.ChangeCoinAmount(amountToAddToCoins);
        }
    }

    private string DisplayResultAsText(UseableItem result)
    {
        switch (result)
        {
            case UseableItem.Rock:
                return "Rock";
            case UseableItem.Paper:
                return "Paper";
            case UseableItem.Scissors:
                return "Scissors";
        }

        return "Nothing";
    }
}