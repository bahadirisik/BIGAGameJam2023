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

	int selectedHeroIndex = 0;

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
		selectedHeroIndex = 0;
		PlayerInputManager.instance.JoinPlayer(0, -1, null);
		PlayerInputManager.instance.JoinPlayer(1, -1, null);
    }

    void OnPlayerJoined(PlayerInput playerInput)
	{
		if (selectedHeroIndex >= 1)
			selectedHeroIndex = 1;

		playerInput.GetComponent<PlayerInputHandler>().SetHeroStatsSO(selectedHeroesSO[selectedHeroIndex]);
		selectedHeroIndex++;
		playerList.Add(playerInput);
		Debug.Log("aaaa");
	}

	void OnPlayerLeft(PlayerInput playerInput)
	{
		playerList.Remove(playerInput);
	}

	public GameObject[] GetSpawnPoints()
	{
		return spawnPoints;
	}
}
