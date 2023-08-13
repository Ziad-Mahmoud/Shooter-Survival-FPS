using UnityEngine;

public class AdsController : MonoBehaviour
{
	private void Awake()
	{
		//Advertisements.Instance.ShowInterstitial();
		Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
	}
}
