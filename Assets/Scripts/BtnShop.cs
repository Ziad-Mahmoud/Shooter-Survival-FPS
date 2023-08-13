using UnityEngine;
using UnityEngine.UI;

public class BtnShop : MonoBehaviour
{
	[Header("Controller Manager")]
	public GameManager ManagerController;

	[Header("Controller Object")]
	public GameObject CoinBtn;

	public GameObject VideoBtn;

	public GameObject UseBtn;

	public GameObject RemoveBtn;

	[Header("UI Controller")]
	public Text PriceUI;

	[Header("Controller Image")]
	public Image CharacterBg;

	public Sprite AvatarSpire;

	[Header("integer Controller")]
	public int Price = 100;

	[Header("Boolean Controller")]
	public bool CheckCoins;

	public bool CheckVideo;

	private void Awake()
	{
		if (CheckCoins)
		{
			CoinBtn.SetActive(value: true);
			VideoBtn.SetActive(value: false);
		}
		if (CheckVideo)
		{
			CoinBtn.SetActive(value: false);
			VideoBtn.SetActive(value: true);
		}
		if (!(PlayerPrefs.GetString(base.name) == ""))
		{
			if (CheckCoins)
			{
				CoinBtn.gameObject.SetActive(value: false);
				UseBtn.gameObject.SetActive(value: true);
			}
			if (CheckVideo)
			{
				VideoBtn.gameObject.SetActive(value: false);
				UseBtn.gameObject.SetActive(value: true);
			}
		}
		PriceUI.text = Price.ToString() ?? "";
		VideoBtn.gameObject.GetComponent<Button>().onClick.AddListener(BtnReward);
		CoinBtn.gameObject.GetComponent<Button>().onClick.AddListener(BtnClicked);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void BtnReward()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				PlayerPrefs.SetString(base.name, "Done");
				if (CheckVideo)
				{
					VideoBtn.gameObject.SetActive(value: false);
					UseBtn.gameObject.SetActive(value: true);
				}
			}
		}
	}

	private void BtnClicked()
	{
		if (ManagerController.CurrentCoins > 500)
		{
			if (CheckCoins)
			{
				CoinBtn.gameObject.SetActive(value: false);
				UseBtn.gameObject.SetActive(value: true);
			}
			PlayerPrefs.SetString(base.name, "Done");
			PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 500);
		}
	}
}
