using System;
using System.Collections;
using UnityEngine;

public class AppOpenAdController : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(waiting());
	}

	private void Update()
	{
	}

	private IEnumerator waiting()
	{
		yield return new WaitForSeconds(2f);
		yield return new WaitForSeconds(0.5f);
	}

	public void LoadAd()
	{
		
	}

	public void ShowAdIfAvailable()
	{
		
	}


	public void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			ShowAdIfAvailable();
		}
	}

	public void ShowApOpenAd()
	{
		ShowAdIfAvailable();
	}
}
