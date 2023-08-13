using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
	[Header("Controller Manager")]
	public Image FillingBar;

	[Header("Object Controller")]
	public GameObject GDPR;

	[Header("Floating Controller")]
	internal float TimeMoving = 4f;

	[Header("Boolean Controller")]
	internal bool ShowAd = true;

	internal bool TimeFinished = true;

	private void Awake()
	{
		Advertisements.Instance.Initialize();
	}

	private void Update()
	{
		if (!TimeFinished)
		{
			if (FillingBar.fillAmount > 0.9f && ShowAd)
			{
				ShowAd = false;
			}
			if (FillingBar.fillAmount == 1f)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				TimeFinished = true;
			}
			else
			{
				FillingBar.fillAmount += Time.deltaTime / TimeMoving;
			}
		}
		else if (TimeFinished)
		{
			if (PlayerPrefs.GetString("FirstTime") == "")
			{
				PlayerPrefs.SetInt("Sound", 1);
				PlayerPrefs.SetInt("Music", 1);
				GDPR.gameObject.SetActive(value: true);
			}
			else
			{
				GDPR.gameObject.SetActive(value: false);
				TimeFinished = false;
			}
		}
	}

	public void BtnAgree()
	{
		PlayerPrefs.SetString("FirstTime", "Done");
	}
}
