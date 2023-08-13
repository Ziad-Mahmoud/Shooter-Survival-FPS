using System.Collections;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
	[Header("Timing By")]
	public float DestroyTimes = 2f;

	private void Start()
	{
		StartCoroutine(LoadingDestroy());
	}

	private IEnumerator LoadingDestroy()
	{
		yield return new WaitForSeconds(DestroyTimes);
		Object.Destroy(base.gameObject);
	}
}
