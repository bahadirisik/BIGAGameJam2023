using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndedUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI playerOnePointText;
	[SerializeField] private TextMeshProUGUI playerTwoPointText;

	private void Start()
	{
		GameManager.instance.OnLevelEnd += GameManager_OnLevelEnd;
	}

	private void GameManager_OnLevelEnd()
	{
		playerOnePointText.text = GameManager.playerPoints[0].ToString();
		playerTwoPointText.text = GameManager.playerPoints[1].ToString();
	}
}
