using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
	PlayerInputHandler playerInput;
	DamageableBase damageable;
	[SerializeField] private Image skillOneImage;
	[SerializeField] private Image skillTwoImage;
	[SerializeField] private Image skillThreeImage;

	public void SetPlayer(PlayerInputHandler player)
	{
		playerInput = player;

		damageable = player.transform.parent.GetComponentInChildren<DamageableBase>();
		damageable.OnHealthChange += SetHealth;

		SetSkillOneImage();
		SetSkillTwoImage();
		SetSkillThreeImage();
	}
    
    public void SetMaxHealth(int health)
	{
		healthSlider.maxValue = health;
		healthSlider.value = health;
	}

	public void SetHealth(int health)
	{
		healthSlider.value = health;
	}

	public void SetSkillOneImage()
	{
		skillOneImage.sprite = playerInput.GetHeroStatsSO().heroSkillOneSprite;
	}

	public void SetSkillOneImage(float timer)
	{
		StartCoroutine(SetAlpha(skillOneImage, timer));
	}

	public void SetSkillTwoImage()
	{
		skillTwoImage.sprite = playerInput.GetHeroStatsSO().heroSkillTwoSprite;
	}

	public void SetSkillTwoImage(float timer)
	{
		StartCoroutine(SetAlpha(skillTwoImage, timer));
	}

	public void SetSkillThreeImage()
	{
		skillThreeImage.sprite = playerInput.GetHeroStatsSO().heroSkillThreeSprite;
	}

	public void SetSkillThreeImage(float timer)
	{
		StartCoroutine(SetAlpha(skillThreeImage, timer));
	}

	IEnumerator SetAlpha(Image image, float timer)
	{
		var tempColor = image.color;
		tempColor.a = 0.6f;
		image.color = tempColor;

		yield return new WaitForSeconds(timer);

		tempColor = image.color;
		tempColor.a = 1f;
		image.color = tempColor;
	}
}
