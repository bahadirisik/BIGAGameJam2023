using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	[SerializeField] private HeroStats[] selectedHeroesSO;

	[SerializeField] private GameObject[] spawnPoints;
	[SerializeField] private List<PlayerInput> playerList = new List<PlayerInput>();

	int playerOneSelectedHeroIndex = 0;
	int playerTwoSelectedHeroIndex = 0;

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

	}
	void Start()
    {
		playerOneSelectedHeroIndex = 0;
		playerTwoSelectedHeroIndex = 0;
    }

	public void SetJoinedPlayer()
	{
		PlayerInputManager.instance.JoinPlayer(0, -1, null);
		PlayerInputManager.instance.JoinPlayer(1, -1, null);
	}

	void OnPlayerJoined(PlayerInput playerInput)
	{
		if(playerInput.playerIndex == 0)
		{
			playerInput.gameObject.SetActive(true);
			playerInput.GetComponent<PlayerInputHandler>().SpawnPos(spawnPoints[0].transform);
			playerInput.GetComponent<PlayerInputHandler>().SetHeroStatsSO(selectedHeroesSO[playerOneSelectedHeroIndex]);
			playerList.Add(playerInput);
		}else if(playerInput.playerIndex == 1)
		{
			playerInput.gameObject.SetActive(true);
			playerInput.GetComponent<PlayerInputHandler>().SpawnPos(spawnPoints[1].transform);
			playerInput.GetComponent<PlayerInputHandler>().SetHeroStatsSO(selectedHeroesSO[playerTwoSelectedHeroIndex]);
			playerList.Add(playerInput);
		}

	}

	void OnPlayerLeft(PlayerInput playerInput)
	{
		playerList.Remove(playerInput);
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
}
