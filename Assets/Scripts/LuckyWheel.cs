using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheel : MonoBehaviour
{
	[Header("GameObjects Controller")]
	public GameObject WheelObj;

	[Header("Floating Controller")]
	public float speed = 100f;

	[Header("Controller Objects")]
	public GameObject[] ListPrices;

	[Header("Boolean Controller")]
	private bool isSpinning;

	private bool RewardLoaded;

	[Header("Integer Controller")]
	public int[] rewards;

	private int currentReward = -1;

	public int score;

	[Header("ui Controller")]
	public string rewardText;

	private void Start()
	{
		ListPrices[0].gameObject.GetComponent<Text>().text = rewards[0].ToString() ?? "";
		ListPrices[1].gameObject.GetComponent<Text>().text = rewards[1].ToString() ?? "";
		ListPrices[2].gameObject.GetComponent<Text>().text = rewards[2].ToString() ?? "";
		ListPrices[3].gameObject.GetComponent<Text>().text = rewards[3].ToString() ?? "";
		ListPrices[4].gameObject.GetComponent<Text>().text = rewards[4].ToString() ?? "";
		ListPrices[5].gameObject.GetComponent<Text>().text = rewards[5].ToString() ?? "";
		ListPrices[6].gameObject.GetComponent<Text>().text = rewards[6].ToString() ?? "";
		ListPrices[7].gameObject.GetComponent<Text>().text = rewards[7].ToString() ?? "";
	}

	private void Update()
	{
		GameObject[] listPrices = ListPrices;
		for (int i = 0; i < listPrices.Length; i++)
		{
			listPrices[i].transform.rotation = Quaternion.identity;
		}
	}

	public void SpinReward()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				Spin();
			}
		}
	}

	public void Spin()
	{
		if (!isSpinning)
		{
			currentReward = UnityEngine.Random.Range(0, rewards.Length);
			float num = 360f - (float)currentReward * (360f / (float)rewards.Length);
			num += UnityEngine.Random.Range(-10f, 10f);
			StartCoroutine(SpinWheelCoroutine(num, EaseOutSine, EaseInSine));
		}
	}

	private IEnumerator SpinWheelCoroutine(float targetAngle, Func<float, float> easeOutFunc, Func<float, float> easeInFunc)
	{
		isSpinning = true;
		float initialSpeed = speed / 2f;
		float finalSpeed = speed * 2f;
		float elapsedTime2 = 0f;
		while (elapsedTime2 < 1f)
		{
			float t = easeOutFunc(elapsedTime2);
			float num = Mathf.Lerp(initialSpeed, finalSpeed, t);
			WheelObj.transform.Rotate(Vector3.back * num * Time.deltaTime);
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		while (WheelObj.transform.eulerAngles.z < targetAngle)
		{
			WheelObj.transform.Rotate(Vector3.back * speed * Time.deltaTime);
			yield return null;
		}
		elapsedTime2 = 0f;
		while (elapsedTime2 < 1f)
		{
			float t2 = easeInFunc(elapsedTime2);
			float num = Mathf.Lerp(finalSpeed, initialSpeed, t2);
			WheelObj.transform.Rotate(Vector3.back * num * Time.deltaTime);
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		float z = Mathf.Round(WheelObj.transform.eulerAngles.z / 90f) * 90f;
		WheelObj.transform.rotation = Quaternion.Euler(0f, 0f, z);
		rewardText = "Reward: " + rewards[currentReward];
		PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + rewards[currentReward]);
		score += rewards[currentReward];
		isSpinning = false;
	}

	private float EaseOutSine(float t)
	{
		return Mathf.Sin(t * MathF.PI / 2f);
	}

	private float EaseInSine(float t)
	{
		return 1f - Mathf.Cos(t * MathF.PI / 2f);
	}
}
