using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private HeroStats heroStatsSO;
    [SerializeField] private GameObject playerPrefab;
    private PlayerMovement playerMovement;

	void Start()
    {
        if (heroStatsSO == null)
            return;

        if(heroStatsSO.heroPrefab != null)
		{
            playerMovement = Instantiate(heroStatsSO.heroPrefab,
                GameManager.instance.GetSpawnPoints()[0].transform).GetComponent<PlayerMovement>();

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

    public void SetHeroStatsSO(HeroStats _heroStatsSO)
	{
        heroStatsSO = _heroStatsSO;
    }

}
