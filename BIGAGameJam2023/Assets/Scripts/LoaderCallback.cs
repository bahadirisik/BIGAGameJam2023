using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
	private bool isFirstUpdate = true;
	[SerializeField] private float startTimer;
	private float timer;

	private void Start()
	{
		timer = startTimer;
	}

	private void Update()
	{
		if (isFirstUpdate && timer < 0f)
		{
			isFirstUpdate = false;

			Loader.LoaderCallback();
		}

		timer -= Time.deltaTime;
	}
}
