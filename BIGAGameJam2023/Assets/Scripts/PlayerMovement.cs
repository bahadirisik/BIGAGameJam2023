using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;

    float playerSpeed = 5f;
    float startPlayerSpeed = 5f;

    //[SerializeField] private TrailRenderer tr;
    //[SerializeField] private GameObject walkingEffect;
    //private string[] dashSounds = { "Dash1", "Dash2" };

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;
    void Start()
    {
        playerSpeed = startPlayerSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
            return;

       /* movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");*/


        if (movement.magnitude != 0f)
        {
            //walkingEffect.SetActive(true);
        }
        else
        {
            //walkingEffect.SetActive(false);
        }

        /*if (canDash)
        {
            StartCoroutine(Dash());
        }*/
    }

    public void OnMove(InputAction.CallbackContext ctx)
	{
        movement = ctx.ReadValue<Vector2>();
	}

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (!canDash)
            return;
        StartCoroutine(Dash());
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;

        rb.velocity = movement * playerSpeed;
        //rb.MovePosition(rb.position + movement * startPlayerSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        //FindObjectOfType<AudioManager>().Play(dashSounds[Random.Range(0, dashSounds.Length)]);
        canDash = false;
        isDashing = true;
        Vector2 dashDir = movement;
        rb.velocity = dashDir * dashingPower;
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
