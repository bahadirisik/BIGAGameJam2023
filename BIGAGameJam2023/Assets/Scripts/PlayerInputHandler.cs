using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private HeroStats heroStatsSO;
    [SerializeField] private GameObject playerPrefab;
    private Transform spawnPos;
    private PlayerMovement playerMovement;
    private IMageAttack mageAttacks;

	void Start()
    {
        if (heroStatsSO == null)
            return;

        if(heroStatsSO.heroPrefab != null)
		{
            playerMovement = Instantiate(heroStatsSO.heroPrefab, spawnPos).GetComponent<PlayerMovement>();
            mageAttacks = playerMovement.transform.GetComponent<IMageAttack>();

            transform.parent = playerMovement.transform;
            transform.position = playerMovement.transform.position;
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
        playerMovement.OnDash(ctx);
    }

    public void OnPlayerMageAttack1()
	{
        mageAttacks.MageAttack1();
	}

    public void OnPlayerMageAttack2()
    {
        mageAttacks.MageAttack2();
    }

    public void OnPlayerMageAttack3()
    {
        mageAttacks.MageAttack3();
    }

    public void SetHeroStatsSO(HeroStats _heroStatsSO)
	{
        heroStatsSO = _heroStatsSO;
    }

    public void SpawnPos(Transform pos)
	{
        spawnPos = pos;
	}

}
