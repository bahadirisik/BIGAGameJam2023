using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public event Action OnLevelEnd;

    public static GameManager instance;

	[SerializeField] private HeroStats[] selectedHeroesSO;
	[SerializeField] private GameObject[] companions;
	[SerializeField] private GameObject[] spawnPoints;
	[SerializeField] private GameObject[] playerInfoPanels;
	[SerializeField] private GameObject endGamePanel;

	[SerializeField] private List<PlayerInput> playerList = new List<PlayerInput>();
	public static int[] playerPoints = { 0, 0 };

	int playerOneSelectedHeroIndex = 0;
	int playerTwoSelectedHeroIndex = 0;

	public enum GameState
	{
		Starting,
		Playing,
		GameEnded,
	}

	private GameState gameState;

	private void Awake()
	{
		if(instance == null)
		{
            instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		gameState = GameState.Starting;
	}
	void Start()
    {
		endGamePanel.SetActive(false);
		playerOneSelectedHeroIndex = 0;
		playerTwoSelectedHeroIndex = 0;
    }

	public void SetJoinedPlayer()
	{
		gameState = GameState.Playing;
		PlayerInputManager.instance.JoinPlayer(0, -1, null);
		PlayerInputManager.instance.JoinPlayer(1, -1, null);
	}

	void OnPlayerJoined(PlayerInput playerInput)
	{
		if(gameState == GameState.GameEnded)
		{
			return;
		}

		if(playerInput.playerIndex == 0)
		{
			playerInput.gameObject.SetActive(true);
			playerInput.GetComponent<PlayerInputHandler>().SpawnPos(spawnPoints[0].transform);
			playerInput.GetComponent<PlayerInputHandler>().SetHeroStatsSO(selectedHeroesSO[playerOneSelectedHeroIndex]);
			playerInput.GetComponent<PlayerInputHandler>().SetCompanion(companions[Random.Range(0,companions.Length)]);
			playerInput.GetComponent<PlayerInputHandler>().SetHeroInfoPanel(playerInfoPanels[0], 1);
			playerList.Add(playerInput);
		}else if(playerInput.playerIndex == 1)
		{
			playerInput.gameObject.SetActive(true);
			playerInput.GetComponent<PlayerInputHandler>().SpawnPos(spawnPoints[1].transform);
			playerInput.GetComponent<PlayerInputHandler>().SetHeroStatsSO(selectedHeroesSO[playerTwoSelectedHeroIndex]);
			playerInput.GetComponent<PlayerInputHandler>().SetCompanion(companions[Random.Range(0, companions.Length)]);
			playerInput.GetComponent<PlayerInputHandler>().SetHeroInfoPanel(playerInfoPanels[1], 2);
			playerList.Add(playerInput);
		}

	}

	void OnPlayerLeft(PlayerInput playerInput)
	{
		if(gameState == GameState.Playing)
		{
			playerList.Remove(playerInput);
			OnGameEnded();
		}
	}


	public GameObject[] GetSpawnPoints()
	{
		return spawnPoints;
	}

	public void SetPlayerOneHeroIndex(int index)
	{
		playerOneSelectedHeroIndex = index;
	}

	public void SetPlayerTwoHeroIndex(int index)
	{
		playerTwoSelectedHeroIndex = index;
	}

	/*public void SetGameState(GameState state)
	{
		gameState = state;
	}*/

	public GameState GetGameState()
	{
		return gameState;
	}

	public void OnGameEnded()
	{
		PlayerInput lastPlayer;
		if (playerList.Count > 0)
		{
			lastPlayer = playerList[0];
			int playerNumber = lastPlayer.GetComponent<PlayerInputHandler>().GetPlayerNumber();
			if (playerNumber == 1)
				playerPoints[0]++;
			else if (playerNumber == 2)
				playerPoints[1]++;
		}

		OnLevelEnd?.Invoke();

		if(endGamePanel != null)
			endGamePanel.SetActive(true);

		gameState = GameState.GameEnded;
	}
}
