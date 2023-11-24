using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnPlayerMove(InputAction.CallbackContext ctx)
    {
        playerMovement.SetMovementVector(ctx.ReadValue<Vector2>());
    }
}
