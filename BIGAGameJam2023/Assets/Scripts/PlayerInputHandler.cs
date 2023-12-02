using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private HeroStats heroStatsSO;
    //[SerializeField] private GameObject playerPrefab;
    private GameObject companionPrefab;
    private GameObject heroInfoPanel;
    private Transform spawnPos;
    private PlayerMovement playerMovement;
    private IMageAttack mageAttacks;
    private int playerNumber = 0;

	void Start()
    {
        if (heroStatsSO == null)
            return;

        if(heroStatsSO.heroPrefab != null)
		{
            playerMovement = Instantiate(heroStatsSO.heroPrefab, spawnPos).GetComponent<PlayerMovement>();

            Instantiate(companionPrefab, playerMovement.transform).GetComponent<CompanionBase>().SetThrownBy(playerMovement.transform);

            mageAttacks = playerMovement.transform.GetComponent<IMageAttack>();

            transform.parent = playerMovement.transform;
            transform.position = playerMovement.transform.position;

            heroInfoPanel.GetComponent<HealthBarUI>().SetPlayer(this);
            playerMovement.transform.GetComponentInChildren<TMPro.TextMeshPro>().text = playerNumber.ToString();
        }
    }

	public void OnPlayerMove(InputAction.CallbackContext ctx)
    {
        if (playerMovement == null)
            return;

        playerMovement.OnMove(ctx);
    }

    public void OnPlayerDash(InputAction.CallbackContext ctx)
    {
        if (playerMovement == null)
            return;

        playerMovement.OnDash(ctx);
    }

    public void OnPlayerMageAttack1()
	{
        if (playerMovement == null)
            return;

        mageAttacks.MageAttack1();
	}

    public void OnPlayerMageAttack2()
    {
        if (playerMovement == null)
            return;

        mageAttacks.MageAttack2();
    }

    public void OnPlayerMageAttack3()
    {
        if (playerMovement == null)
            return;

        mageAttacks.MageAttack3();
    }

    public void SetHeroStatsSO(HeroStats _heroStatsSO)
	{
        heroStatsSO = _heroStatsSO;
    }

    public HeroStats GetHeroStatsSO()
    {
        return heroStatsSO;
    }

    public GameObject GetHeroInfoPanel()
    {
        return heroInfoPanel;
    }

    public void SpawnPos(Transform pos)
	{
        spawnPos = pos;
	}

    public void SetCompanion(GameObject companion)
	{
        companionPrefab = companion;
	}

    public void SetHeroInfoPanel(GameObject infoPanel, int _playerNumber)
    {
        heroInfoPanel = infoPanel;
        playerNumber = _playerNumber;
    }

    public int GetPlayerNumber()
	{
        return playerNumber;
	}

}
