using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
	[Header("Controller Manager")]
	public SoundManager SoundManager;

	[Header("Canvas Controller")]
	public GameObject UICanvas;

	public GameObject CanvasShowPosition;

	public GameObject CanvasControl;

	public GameObject CanvasLoading;

	public GameObject CanvasGun;

	public GameObject CanvasFront;

	[Header("UI Controller Gun")]
	public GameObject ReloadBtn;

	public GameObject GhostMode;

	public GameObject UpgradeAmmo;

	[Header("Controller UICanvas")]
	public GameObject SettingPanel;

	public GameObject[] SoundObjects;

	public GameObject[] MusicObjects;

	public GameObject LuckyWheel;

	public GameObject ShopPanel;

	public GameObject RewardKey;

	[Header("Controller UI GamePlay")]
	public GameObject[] ListObjets;

	public GameObject[] ListInactiveObjects;

	[Header("Object UI GamePlay")]
	public GameObject UIRoleGame;

	public GameObject AutoSelectRandom;

	public GameObject UIMainLobby;

	[Header("UI Pause Controller")]
	public GameObject PausePanel;

	[Header("Controller GamePlay")]
	public GameObject[] ListWeaponRoblox;

	[Header("Controller Viewers")]
	public GameObject MonsterTracker;

	public GameObject RobloxTracker;

	[Header("Controller Finish Panel")]
	public GameObject LoseScreen;

	public GameObject WinScreen;

	[Header("Boolean Manager")]
	internal bool IsOpenSetting;

	internal bool MusicBtnBool = true;

	internal bool SoundBtnBool = true;

	internal bool LuckyWheelBool;

	internal bool ShopBool;

	internal bool RewaredBool;

	internal bool CheckedBtn = true;

	private void Awake()
	{
		CheckingSong();
	}

	public void OpenRewared(int Active)
	{
		if (CheckedBtn && Active == 1)
		{
			RewaredBool = !RewaredBool;
			SoundManager.FxSound.Play();
			if (RewaredBool)
			{
				RewardKey.gameObject.SetActive(value: true);
			}
			else
			{
				RewardKey.gameObject.SetActive(value: false);
			}
		}
	}

	public void ShopBtn()
	{
		ShopBool = !ShopBool;
		SoundManager.FxSound.Play();
		if (ShopBool)
		{
			ShopPanel.gameObject.SetActive(value: true);
		}
		else if (!ShopBool)
		{
			ShopPanel.gameObject.SetActive(value: false);
		}
	}

	public void LuckyWheelBtn()
	{
		LuckyWheelBool = !LuckyWheelBool;
		SoundManager.FxSound.Play();
		if (LuckyWheelBool)
		{
			LuckyWheel.gameObject.SetActive(value: true);
		}
		else if (!LuckyWheelBool)
		{
			LuckyWheel.gameObject.SetActive(value: false);
		}
	}

	public void OpenSetting()
	{
		IsOpenSetting = !IsOpenSetting;
		SoundManager.FxSound.Play();
		if (IsOpenSetting)
		{
			SettingPanel.gameObject.SetActive(value: true);
		}
		else if (!IsOpenSetting)
		{
			SettingPanel.gameObject.SetActive(value: false);
		}
	}

	public void MusicBtn()
	{
		MusicBtnBool = !MusicBtnBool;
		SoundManager.FxSound.Play();
		if (MusicBtnBool)
		{
			PlayerPrefs.SetInt("Music", 1);
			SoundManager.BackgroundMusic.gameObject.SetActive(value: true);
			MusicObjects[0].gameObject.GetComponent<Image>().color = Color.white;
			MusicObjects[1].gameObject.GetComponent<Text>().color = Color.white;
		}
		else if (!MusicBtnBool)
		{
			PlayerPrefs.SetInt("Music", 0);
			SoundManager.BackgroundMusic.gameObject.SetActive(value: false);
			MusicObjects[0].gameObject.GetComponent<Image>().color = Color.black;
			MusicObjects[1].gameObject.GetComponent<Text>().color = Color.black;
		}
	}

	public void SoundBtn()
	{
		SoundBtnBool = !SoundBtnBool;
		SoundManager.FxSound.Play();
		if (SoundBtnBool)
		{
			PlayerPrefs.SetInt("Sound", 1);
			SoundManager.FxSound.gameObject.SetActive(value: true);
			SoundObjects[0].gameObject.GetComponent<Image>().color = Color.white;
			SoundObjects[1].gameObject.GetComponent<Text>().color = Color.white;
		}
		else if (!SoundBtnBool)
		{
			PlayerPrefs.SetInt("Sound", 0);
			SoundObjects[0].gameObject.GetComponent<Image>().color = Color.black;
			SoundObjects[1].gameObject.GetComponent<Text>().color = Color.black;
			SoundManager.FxSound.gameObject.SetActive(value: false);
		}
	}

	private void CheckingSong()
	{
		if (PlayerPrefs.GetInt("Music") == 1)
		{
			SoundManager.BackgroundMusic.gameObject.SetActive(value: true);
			SoundObjects[0].gameObject.GetComponent<Image>().color = Color.white;
			SoundObjects[1].gameObject.GetComponent<Text>().color = Color.white;
		}
		else
		{
			SoundManager.BackgroundMusic.gameObject.SetActive(value: false);
			SoundObjects[0].gameObject.GetComponent<Image>().color = Color.black;
			SoundObjects[1].gameObject.GetComponent<Text>().color = Color.black;
		}
		if (PlayerPrefs.GetInt("Sound") == 1)
		{
			SoundManager.FxSound.gameObject.SetActive(value: true);
			MusicObjects[0].gameObject.GetComponent<Image>().color = Color.white;
			MusicObjects[1].gameObject.GetComponent<Text>().color = Color.white;
		}
		else
		{
			SoundManager.FxSound.gameObject.SetActive(value: false);
			MusicObjects[0].gameObject.GetComponent<Image>().color = Color.black;
			MusicObjects[1].gameObject.GetComponent<Text>().color = Color.black;
		}
	}
}
