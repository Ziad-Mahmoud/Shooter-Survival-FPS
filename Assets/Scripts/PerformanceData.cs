using UnityEngine;
using UnityEngine.UI;

public class PerformanceData : MonoBehaviour
{
	[Header("GameManager")]
	public GameManager ManagerGame;

	[Header("Data Controller")]
	internal int DataHp;

	internal int DataSpeed;

	[Header("UI Controller")]
	public Text SpeedValueUI;

	public Text HpValueUI;

	private void Awake()
	{
		if (PlayerPrefs.GetString("TimedLaunch") == "")
		{
			PlayerPrefs.SetInt("Coins", 100);
			PlayerPrefs.SetInt("DataHp", 1);
			PlayerPrefs.SetInt("SpeedData", 1);
			PlayerPrefs.SetString("TimedLaunch", "Done");
		}
	}

	private void Update()
	{
		SpeedValueUI.text = "Lv." + DataSpeed;
		HpValueUI.text = "Lv." + DataHp;
		ManagerController();
	}

	private void ManagerController()
	{
		DataHp = PlayerPrefs.GetInt("DataHp");
		DataSpeed = PlayerPrefs.GetInt("SpeedData");
	}

	public void Upgrade(string CurrentData)
	{
		_ = CurrentData == "hp";
		if (CurrentData == "speed" && ManagerGame.CurrentCoins >= 100)
		{
			ManagerGame.CurrentCoins -= 100;
			PlayerPrefs.SetFloat("AdditionSpeed", PlayerPrefs.GetFloat("AdditionSpeed") + 0.35f);
			PlayerPrefs.SetInt("SpeedData", PlayerPrefs.GetInt("SpeedData") + 1);
		}
	}
}
