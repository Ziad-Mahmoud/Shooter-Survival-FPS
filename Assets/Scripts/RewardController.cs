using UnityEngine;

public class RewardController : MonoBehaviour
{
	[Header("Claim Reward")]
	public GameManager Manager;

	public void Double()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		static void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 3000);
			}
		}
	}

	public void Cleam()
	{
		PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1500);
	}
}
