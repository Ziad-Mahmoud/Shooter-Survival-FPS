using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[Header("Controller Manager")]
	public UiController uiController;

	public IapController iapController;

	public EventBroker eventBroker;

	public SoundManager soundManager;

	public PrefabeStage StageController;

	public CharacterList SpawningManager;

	public MusicController MusicManager;

	public LevelManager Level;

	public MissionController MissionManager;

	[Header("Transform Position")]
	public Text ValueAdsMoving;

	public GameObject InterOpenAds;

	public GameObject FloatingBar;

	[Header("Controller UI")]
	public Image SpriteManagerControllerCurrentMode;

	public Text NameTextManagerControllerCurrentMode;

	[Header("Controller Camera Position")]
	public GameObject CameraObj;

	public GameObject CameraFPS;

	public GameObject FpsContainer;

	public GameObject[] PositionCamera;

	[Header("Over GamePosition")]
	public GameObject TransformOriginalPosition;

	[Header("Controller Health")]
	public Image HealthBar;

	public Text HealthAmount;

	[Header("Controller Ammo")]
	public Image BarAmmo;

	public Text AmmoText;

	[Header("Controller UI")]
	public Text CoinsUI;

	[Header("Layers Controller")]
	public LayerMask HiddenObjects;

	[Header("Controller Level Player")]
	public Text CurrentRoblox;

	public Text CurrentMonster;

	[Header("Texting Controller")]
	public Text RewardMultiple;

	public Text RewardNormal;

	[Header("Compoenet Object")]
	public GameObject ContainerObstacle;

	[Header("Vectors Controller")]
	internal Vector3 VectorPosition = Vector3.zero;

	[Header("Integer Controller")]
	internal int CurrentCoins;

	internal int CurrentModePlay;

	internal int CurrentAutoSelectplayNoThanks;

	internal int CurrentMultipler;

	public int ActiveCurrentMonsters;

	public int ActiveCurrentRoblox;

	[Header("Floating Manager")]
	internal float TimeShowAds = 4f;

	internal float SmoothMovement = 0.4f;

	internal float TimeAdsToStart = 15f;

	public float PositionX;

	[Header("Boolean Manager")]
	internal bool CameraInCharacter;

	internal bool CameraInsideObject = true;

	internal bool BtnPlay;

	internal bool SkipSceneNoThanks;

	internal bool RoundMonster;

	internal bool RoundRoblox;

	internal bool GamePaused;

	internal bool GameOver;

	internal bool GameWin;

	internal bool StartChecking;

	internal bool StartmoveFloater;

	internal bool GameStart;

	internal bool AutoSelectNoThanks = true;

	internal bool DetectSoundStopTime = true;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		ManagerControllerLogic();
		ManagerAmmo();
		ManagerPlayerPosition();
		ManagerCurrentGame();
		ManagerData();
	}

	public void GhostMode()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				eventBroker.PlayerActive.tag = "Player";
				uiController.GhostMode.SetActive(value: false);
			}
		}
	}

	public void UpgradeWeapon()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				_ = RoundRoblox;
				uiController.UpgradeAmmo.SetActive(value: false);
			}
		}
	}

	public void ViewCharacters()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				if (RoundMonster)
				{
					uiController.RobloxTracker.SetActive(value: true);
					eventBroker.HiddenView.SetActive(value: false);
				}
				if (RoundRoblox)
				{
					uiController.MonsterTracker.SetActive(value: true);
					eventBroker.HiddenView.SetActive(value: false);
				}
			}
		}
	}

	public void JumpBtn()
	{
		if (RoundRoblox)
		{
			eventBroker.PlayerActive.GetComponent<CharacterManager>().Jump();
		}
		if (RoundMonster)
		{
			eventBroker.PlayerActive.GetComponent<CharacterManagerMonster>().Jump();
		}
	}

	public void HomeBtn(string CurrentType)
	{
		Advertisements.Instance.ShowInterstitial();
		MissionManager.ResetValue();
		MissionManager.CheckText = true;
		MissionManager.MotOne = false;
		MissionManager.MotTwo = false;
		MissionManager.MotThree = false;
		MissionManager.MotFour = false;
		MissionManager.MotFive = false;
		MissionManager.MotSix = false;
		MissionManager.MotSeven = false;
		MissionManager.FixedWin = true;
		uiController.GhostMode.SetActive(value: true);
		if (CurrentType == "")
		{
			GameOver = false;
			GameWin = false;
			AutoSelectNoThanks = true;
			GameStart = false;
			Time.timeScale = 1f;
			CameraInCharacter = false;
			CameraInsideObject = true;
			BtnPlay = false;
			SkipSceneNoThanks = false;
			RoundMonster = false;
			RoundRoblox = false;
			GamePaused = false;
			StartChecking = false;
			CameraObj.transform.parent = null;
			Object.Destroy(eventBroker.PlayerActive.gameObject);
			GameObject[] listObjets = uiController.ListObjets;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: false);
			}
			listObjets = uiController.ListInactiveObjects;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: true);
			}
			uiController.MonsterTracker.SetActive(value: false);
			uiController.RobloxTracker.SetActive(value: false);
			eventBroker.HiddenView.SetActive(value: true);
			CameraObj.gameObject.GetComponent<Camera>().enabled = true;
			eventBroker.RoleRoblox.SetActive(value: false);
			eventBroker.RoleMonster.SetActive(value: false);
			CameraFPS.gameObject.SetActive(value: false);
			uiController.CanvasGun.gameObject.SetActive(value: false);
			uiController.CanvasControl.gameObject.SetActive(value: false);
			uiController.UICanvas.gameObject.SetActive(value: true);
			uiController.UIMainLobby.gameObject.SetActive(value: true);
			uiController.PausePanel.gameObject.SetActive(value: false);
			soundManager.BackgroundMusic.clip = MusicManager.MusicHome;
			soundManager.BackgroundMusic.Play();
			GameObject[] array = GameObject.FindGameObjectsWithTag("RobloxCh");
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("MonsterCh");
			listObjets = GameObject.FindGameObjectsWithTag("Dead");
			for (int i = 0; i < listObjets.Length; i++)
			{
				Object.Destroy(listObjets[i]);
			}
			listObjets = array2;
			for (int i = 0; i < listObjets.Length; i++)
			{
				Object.Destroy(listObjets[i]);
			}
			listObjets = array;
			for (int i = 0; i < listObjets.Length; i++)
			{
				Object.Destroy(listObjets[i]);
			}
		}
		if (CurrentType == "CollectBtn")
		{
			PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
			GameOver = false;
			GameWin = false;
			DetectSoundStopTime = true;
			AutoSelectNoThanks = true;
			GameStart = false;
			Time.timeScale = 1f;
			CameraInCharacter = false;
			CameraInsideObject = true;
			BtnPlay = false;
			SkipSceneNoThanks = false;
			RoundMonster = false;
			RoundRoblox = false;
			GamePaused = false;
			StartChecking = false;
			CameraObj.transform.parent = null;
			Object.Destroy(eventBroker.PlayerActive.gameObject);
			GameObject[] listObjets = uiController.ListObjets;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: false);
			}
			listObjets = uiController.ListInactiveObjects;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: true);
			}
			uiController.MonsterTracker.SetActive(value: false);
			uiController.RobloxTracker.SetActive(value: false);
			eventBroker.HiddenView.SetActive(value: true);
			CameraObj.gameObject.GetComponent<Camera>().enabled = true;
			eventBroker.RoleRoblox.SetActive(value: false);
			eventBroker.RoleMonster.SetActive(value: false);
			CameraFPS.gameObject.SetActive(value: false);
			uiController.CanvasGun.gameObject.SetActive(value: false);
			uiController.CanvasControl.gameObject.SetActive(value: false);
			uiController.UICanvas.gameObject.SetActive(value: true);
			uiController.UIMainLobby.gameObject.SetActive(value: true);
			uiController.PausePanel.gameObject.SetActive(value: false);
			soundManager.BackgroundMusic.clip = MusicManager.MusicHome;
			soundManager.BackgroundMusic.Play();
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("RobloxCh");
			GameObject[] array4 = GameObject.FindGameObjectsWithTag("MonsterCh");
			listObjets = GameObject.FindGameObjectsWithTag("Dead");
			for (int i = 0; i < listObjets.Length; i++)
			{
				Object.Destroy(listObjets[i]);
			}
			listObjets = array4;
			for (int i = 0; i < listObjets.Length; i++)
			{
				Object.Destroy(listObjets[i]);
			}
			listObjets = array3;
			for (int i = 0; i < listObjets.Length; i++)
			{
				Object.Destroy(listObjets[i]);
			}
		}
	}

	public void NoThanksLose()
	{
		uiController.LoseScreen.SetActive(value: false);
		HomeBtn("CollectBtn");
	}

	public void PauseBtn()
	{
		GamePaused = !GamePaused;
		if (GamePaused)
		{
			Time.timeScale = 0f;
			uiController.PausePanel.gameObject.SetActive(value: true);
		}
		else if (!GamePaused)
		{
			Time.timeScale = 1f;
			uiController.PausePanel.gameObject.SetActive(value: false);
		}
	}

	public void NoThankRole()
	{
		if (AutoSelectNoThanks)
		{
			StartCoroutine(StartCheckingGame());
			SkipSceneNoThanks = true;
			if (!SkipSceneNoThanks || !BtnPlay)
			{
				return;
			}
			StartCoroutine(LoadingGamePlayClicked());
			switch (Random.Range(0, 2))
			{
			case 0:
			{
				RoundRoblox = false;
				RoundMonster = true;
				ManagerlevelPosition();
				Object.Instantiate(SpawningManager.MonsterPerf, Level.SpawenPoint.transform.position, base.transform.rotation);
				eventBroker.PlayerActive = GameObject.FindGameObjectWithTag("Player");
				eventBroker.ColorTypeModeAuto.color = eventBroker.MonsterColor;
				uiController.CanvasGun.gameObject.SetActive(value: true);
				uiController.CanvasControl.gameObject.SetActive(value: true);
				if (RoundMonster)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().ControllerTransformPositionCamera.gameObject;
				}
				else if (RoundRoblox)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerTransformPositionCamera.gameObject;
				}
				GameObject[] listObjets = uiController.ListObjets;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: true);
				}
				listObjets = uiController.ListInactiveObjects;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: false);
				}
				CameraInCharacter = true;
				CameraInsideObject = true;
				if (PositionCamera[0] != null)
				{
					CameraObj.transform.parent = PositionCamera[0].transform;
				}
				SpawningRobloxToMap();
				SpawningMonsterToMap();
				ManagerTypes();
				uiController.UIRoleGame.gameObject.SetActive(value: false);
				eventBroker.RoleMonster.SetActive(value: true);
				BtnPlay = false;
				SkipSceneNoThanks = false;
				break;
			}
			case 1:
			{
				StartCoroutine(LoadingGamePlayClicked());
				RoundRoblox = true;
				RoundMonster = false;
				ManagerlevelPosition();
				Object.Instantiate(SpawningManager.PlayerPerf, Level.SpawenPoint.transform.position, base.transform.rotation);
				eventBroker.PlayerActive = GameObject.FindGameObjectWithTag("Player");
				eventBroker.ColorTypeModeAuto.color = eventBroker.RobloxColor;
				uiController.CanvasGun.gameObject.SetActive(value: true);
				uiController.CanvasControl.gameObject.SetActive(value: true);
				if (RoundMonster)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().ControllerTransformPositionCamera.gameObject;
				}
				else if (RoundRoblox)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerTransformPositionCamera.gameObject;
				}
				GameObject[] listObjets = uiController.ListObjets;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: true);
				}
				listObjets = uiController.ListInactiveObjects;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: false);
				}
				ManagerTypes();
				CameraInCharacter = true;
				CameraInsideObject = true;
				SpawningRobloxToMap();
				SpawningMonsterToMap();
				CameraObj.transform.parent = PositionCamera[0].transform;
				uiController.UIRoleGame.gameObject.SetActive(value: false);
				eventBroker.RoleRoblox.SetActive(value: true);
				BtnPlay = false;
				SkipSceneNoThanks = false;
				break;
			}
			}
		}
		else
		{
			if (AutoSelectNoThanks)
			{
				return;
			}
			StartCoroutine(StartCheckingGame());
			SkipSceneNoThanks = true;
			if (!SkipSceneNoThanks || !BtnPlay)
			{
				return;
			}
			StartCoroutine(LoadingGamePlayClicked());
			if (CurrentAutoSelectplayNoThanks == 0)
			{
				RoundRoblox = false;
				RoundMonster = true;
				ManagerlevelPosition();
				Object.Instantiate(SpawningManager.MonsterPerf, Level.SpawenPoint.transform.position, base.transform.rotation);
				eventBroker.PlayerActive = GameObject.FindGameObjectWithTag("Player");
				eventBroker.ColorTypeModeAuto.color = eventBroker.MonsterColor;
				uiController.CanvasGun.gameObject.SetActive(value: true);
				uiController.CanvasControl.gameObject.SetActive(value: true);
				if (RoundMonster)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().ControllerTransformPositionCamera.gameObject;
				}
				else if (RoundRoblox)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerTransformPositionCamera.gameObject;
				}
				GameObject[] listObjets = uiController.ListObjets;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: true);
				}
				listObjets = uiController.ListInactiveObjects;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: false);
				}
				CameraInCharacter = true;
				CameraInsideObject = true;
				if (PositionCamera[0] != null)
				{
					CameraObj.transform.parent = PositionCamera[0].transform;
				}
				SpawningRobloxToMap();
				SpawningMonsterToMap();
				ManagerTypes();
				uiController.UIRoleGame.gameObject.SetActive(value: false);
				eventBroker.RoleMonster.SetActive(value: true);
				BtnPlay = false;
				SkipSceneNoThanks = false;
			}
			else if (CurrentAutoSelectplayNoThanks == 1)
			{
				StartCoroutine(LoadingGamePlayClicked());
				RoundRoblox = true;
				RoundMonster = false;
				ManagerlevelPosition();
				Object.Instantiate(SpawningManager.PlayerPerf, Level.SpawenPoint.transform.position, base.transform.rotation);
				eventBroker.PlayerActive = GameObject.FindGameObjectWithTag("Player");
				eventBroker.ColorTypeModeAuto.color = eventBroker.RobloxColor;
				uiController.CanvasGun.gameObject.SetActive(value: true);
				uiController.CanvasControl.gameObject.SetActive(value: true);
				if (RoundMonster)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().ControllerTransformPositionCamera.gameObject;
				}
				else if (RoundRoblox)
				{
					PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerTransformPositionCamera.gameObject;
				}
				GameObject[] listObjets = uiController.ListObjets;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: true);
				}
				listObjets = uiController.ListInactiveObjects;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].SetActive(value: false);
				}
				ManagerTypes();
				CameraInCharacter = true;
				CameraInsideObject = true;
				SpawningRobloxToMap();
				SpawningMonsterToMap();
				CameraObj.transform.parent = PositionCamera[0].transform;
				uiController.UIRoleGame.gameObject.SetActive(value: false);
				eventBroker.RoleRoblox.SetActive(value: true);
				BtnPlay = false;
				SkipSceneNoThanks = false;
			}
		}
	}

	public void RewaredVideoLose()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				HomeBtn("");
			}
		}
	}

	public void PlayBtn()
	{
		Time.timeScale = 1f;
		GameStart = true;
		soundManager.BackgroundMusic.clip = MusicManager.MusicGamePlay;
		soundManager.BackgroundMusic.Play();
		int num = Random.Range(0, 2);
		if (num == 0)
		{
			StartCoroutine(LoadingGame());
			BtnPlay = true;
		}
		if (num == 1)
		{
			uiController.UICanvas.gameObject.SetActive(value: false);
			uiController.UIRoleGame.gameObject.SetActive(value: true);
			BtnPlay = true;
		}
	}

	public void ReloadWeapon()
	{
		eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().Reload();
	}

	public void CollectLevel(string Type)
	{
		if (Type == "reward")
		{
			Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		}
		if (Type == "normal")
		{
			PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50);
			uiController.WinScreen.SetActive(value: false);
			HomeBtn("CollectBtn");
		}
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50 * CurrentMultipler);
				uiController.WinScreen.SetActive(value: false);
				DetectSoundStopTime = true;
				HomeBtn("CollectBtn");
			}
		}
	}

	private IEnumerator LoadingGamePlayClicked()
	{
		yield return new WaitForSeconds(2f);
	}

	private IEnumerator LoadingGame()
	{
		int num = Random.Range(0, StageController.ListTypes.Length);
		if (StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().IsMonster)
		{
			CurrentModePlay = 1;
		}
		else if (StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().IsGun)
		{
			CurrentModePlay = 0;
		}
		if (CurrentModePlay == 0)
		{
			RoundRoblox = true;
			RoundMonster = false;
			SpriteManagerControllerCurrentMode.sprite = StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().CurrentSprite;
			NameTextManagerControllerCurrentMode.text = StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().CurrentName;
			PlayerPrefs.SetString("CurrentUsed", StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().CurrentName);
			NameTextManagerControllerCurrentMode.color = Color.blue;
			ManagerlevelPosition();
			Object.Instantiate(SpawningManager.PlayerPerf, Level.SpawenPoint.transform.position, base.transform.rotation);
			eventBroker.PlayerActive = GameObject.FindGameObjectWithTag("Player");
			eventBroker.ColorTypeModeAuto.color = eventBroker.RobloxColor;
		}
		else if (CurrentModePlay == 1)
		{
			RoundRoblox = false;
			RoundMonster = true;
			SpriteManagerControllerCurrentMode.sprite = StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().CurrentSprite;
			NameTextManagerControllerCurrentMode.text = StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().CurrentName;
			PlayerPrefs.SetString("CurrentUsed", StageController.ListTypes[num].gameObject.GetComponent<CurrentTypePlay>().CurrentName);
			NameTextManagerControllerCurrentMode.color = Color.red;
			ManagerlevelPosition();
			Object.Instantiate(SpawningManager.MonsterPerf, Level.SpawenPoint.transform.position, base.transform.rotation);
			eventBroker.PlayerActive = GameObject.FindGameObjectWithTag("Player");
			eventBroker.ColorTypeModeAuto.color = eventBroker.MonsterColor;
		}
		uiController.UIMainLobby.gameObject.SetActive(value: false);
		uiController.AutoSelectRandom.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		uiController.UIMainLobby.gameObject.SetActive(value: true);
		uiController.AutoSelectRandom.gameObject.SetActive(value: false);
		yield return new WaitForEndOfFrame();
		if (!BtnPlay)
		{
			yield break;
		}
		if (RoundMonster)
		{
			eventBroker.RoleMonster.SetActive(value: true);
			uiController.CanvasGun.gameObject.SetActive(value: true);
			uiController.CanvasControl.gameObject.SetActive(value: true);
			uiController.UICanvas.gameObject.SetActive(value: false);
			PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().ControllerTransformPositionCamera.gameObject;
			GameObject[] listObjets = uiController.ListObjets;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: true);
			}
			listObjets = uiController.ListInactiveObjects;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: false);
			}
			SpawningRobloxToMap();
			SpawningMonsterToMap();
			ManagerTypes();
		}
		if (RoundRoblox)
		{
			eventBroker.RoleRoblox.SetActive(value: true);
			uiController.CanvasGun.gameObject.SetActive(value: true);
			uiController.CanvasControl.gameObject.SetActive(value: true);
			uiController.UICanvas.gameObject.SetActive(value: false);
			PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerTransformPositionCamera.gameObject;
			GameObject[] listObjets = uiController.ListObjets;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: true);
			}
			listObjets = uiController.ListInactiveObjects;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].SetActive(value: false);
			}
			SpawningMonsterToMap();
			SpawningRobloxToMap();
			ManagerTypes();
		}
		CameraInCharacter = true;
		CameraInsideObject = true;
		CameraObj.transform.parent = PositionCamera[0].transform;
		StartCoroutine(StartCheckingGame());
		BtnPlay = false;
	}

	private IEnumerator StartCheckingGame()
	{
		yield return new WaitForSeconds(2f);
		StartChecking = true;
	}

	private void ControllerFinishScreen()
	{
		if (GameOver)
		{
			if (DetectSoundStopTime)
			{
				Time.timeScale = 0f;
				soundManager.BackgroundMusic.clip = MusicManager.MusicLose;
				soundManager.BackgroundMusic.loop = false;
				soundManager.BackgroundMusic.Play();
				GameObject[] listObjets = uiController.ListObjets;
				for (int i = 0; i < listObjets.Length; i++)
				{
					listObjets[i].gameObject.SetActive(value: false);
				}
				eventBroker.RoleMonster.SetActive(value: false);
				eventBroker.RoleRoblox.SetActive(value: false);
				uiController.CanvasControl.SetActive(value: false);
				uiController.CanvasGun.SetActive(value: false);
				uiController.WinScreen.SetActive(value: false);
				uiController.LoseScreen.SetActive(value: true);
				DetectSoundStopTime = false;
			}
			Time.timeScale = 0.1f;
		}
		if (!GameWin)
		{
			return;
		}
		if (DetectSoundStopTime)
		{
			soundManager.BackgroundMusic.clip = MusicManager.MusicWin;
			soundManager.BackgroundMusic.loop = false;
			soundManager.BackgroundMusic.Play();
			GameObject[] listObjets = uiController.ListObjets;
			for (int i = 0; i < listObjets.Length; i++)
			{
				listObjets[i].gameObject.SetActive(value: false);
			}
			eventBroker.RoleMonster.SetActive(value: false);
			eventBroker.RoleRoblox.SetActive(value: false);
			uiController.CanvasControl.SetActive(value: false);
			uiController.CanvasGun.SetActive(value: false);
			uiController.WinScreen.SetActive(value: true);
			DetectSoundStopTime = false;
		}
		if (FloatingBar.transform.position.x >= 881.5f)
		{
			StartmoveFloater = false;
		}
		if (FloatingBar.transform.position.x <= 388.75f)
		{
			StartmoveFloater = true;
		}
		ManagerReward();
		Time.timeScale = 0.1f;
		RewardNormal.text = "50";
		if (StartmoveFloater)
		{
			FloatingBar.transform.position = new Vector3(FloatingBar.transform.position.x + 500f * Time.unscaledDeltaTime, FloatingBar.transform.position.y, FloatingBar.transform.position.z);
		}
		if (!StartmoveFloater)
		{
			FloatingBar.transform.position = new Vector3(FloatingBar.transform.position.x - 500f * Time.unscaledDeltaTime, FloatingBar.transform.position.y, FloatingBar.transform.position.z);
		}
	}

	private void ManagerReward()
	{
		if ((double)FloatingBar.transform.position.x >= 388.75 && (double)FloatingBar.transform.position.x < 486.1)
		{
			CurrentMultipler = 2;
			RewardMultiple.text = (50 * CurrentMultipler).ToString() ?? "";
		}
		if ((double)FloatingBar.transform.position.x >= 486.1 && FloatingBar.transform.position.x < 591.4f)
		{
			CurrentMultipler = 4;
			RewardMultiple.text = (50 * CurrentMultipler).ToString() ?? "";
		}
		if (FloatingBar.transform.position.x >= 591.4f && FloatingBar.transform.position.x < 691f)
		{
			CurrentMultipler = 6;
			RewardMultiple.text = (50 * CurrentMultipler).ToString() ?? "";
		}
		if (FloatingBar.transform.position.x >= 691f && FloatingBar.transform.position.x < 794.35f)
		{
			CurrentMultipler = 4;
			RewardMultiple.text = (50 * CurrentMultipler).ToString() ?? "";
		}
		if (FloatingBar.transform.position.x >= 794.35f && FloatingBar.transform.position.x < 881.5f)
		{
			CurrentMultipler = 2;
			RewardMultiple.text = (50 * CurrentMultipler).ToString() ?? "";
		}
	}

	private void ManagerGameFinish()
	{
		if (ActiveCurrentRoblox == 0 && GameStart)
		{
			if (RoundMonster)
			{
				GameWin = true;
			}
			if (RoundRoblox)
			{
				GameOver = true;
			}
		}
		if (ActiveCurrentMonsters == 0 && GameStart)
		{
			if (RoundMonster)
			{
				GameOver = true;
			}
			if (RoundRoblox)
			{
				GameWin = true;
			}
		}
	}

	private void ManagerPlayerPosition()
	{
		ManagerTypes();
		if (RoundRoblox)
		{
			GameObject[] listWeaponRoblox = uiController.ListWeaponRoblox;
			for (int i = 0; i < listWeaponRoblox.Length; i++)
			{
				listWeaponRoblox[i].SetActive(value: true);
			}
		}
		if (RoundMonster)
		{
			GameObject[] listWeaponRoblox = uiController.ListWeaponRoblox;
			for (int i = 0; i < listWeaponRoblox.Length; i++)
			{
				listWeaponRoblox[i].SetActive(value: false);
			}
		}
		CoinsUI.text = CurrentCoins.ToString() ?? "";
	}

	private void ManagerTypes()
	{
		if (RoundMonster)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("RobloxCh");
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("MonsterCh");
			GameObject[] array3;
			if (array2 != null)
			{
				array3 = array2;
				foreach (GameObject gameObject in array3)
				{
					if (!(gameObject.gameObject.GetComponent<MonsterAI>() == null))
					{
						gameObject.gameObject.GetComponent<MonsterAI>().RoundMonster = true;
						gameObject.gameObject.GetComponent<MonsterAI>().CurrentType.color = Color.blue;
					}
				}
			}
			if (array == null)
			{
				return;
			}
			array3 = array;
			foreach (GameObject gameObject2 in array3)
			{
				if (!(gameObject2.gameObject.GetComponent<RobloxController>() == null))
				{
					gameObject2.gameObject.GetComponent<RobloxController>().RoundRoblox = true;
					gameObject2.gameObject.GetComponent<RobloxController>().ControllerCurrentMode.color = Color.red;
				}
			}
		}
		else
		{
			if (!RoundRoblox)
			{
				return;
			}
			GameObject[] array4 = GameObject.FindGameObjectsWithTag("RobloxCh");
			GameObject[] array5 = GameObject.FindGameObjectsWithTag("MonsterCh");
			GameObject[] array3;
			if (array5 != null)
			{
				array3 = array5;
				foreach (GameObject gameObject3 in array3)
				{
					if (!(gameObject3.gameObject.GetComponent<MonsterAI>() == null))
					{
						gameObject3.gameObject.GetComponent<MonsterAI>().RoundMonster = true;
						gameObject3.gameObject.GetComponent<MonsterAI>().CurrentType.color = Color.red;
					}
				}
			}
			if (array4 == null)
			{
				return;
			}
			array3 = array4;
			foreach (GameObject gameObject4 in array3)
			{
				if (!(gameObject4.gameObject.GetComponent<RobloxController>() == null))
				{
					gameObject4.gameObject.GetComponent<RobloxController>().RoundRoblox = true;
					gameObject4.gameObject.GetComponent<RobloxController>().ControllerCurrentMode.color = Color.blue;
				}
			}
		}
	}

	private void ManagerlevelPosition()
	{
		if (RoundMonster)
		{
			if (Level.LevelOne)
			{
				Level.SpawenPoint = SpawningManager.ListPositionMonster[0].gameObject;
			}
			if (Level.LevelTwo)
			{
				Level.SpawenPoint = SpawningManager.ListPositionMonster[1].gameObject;
			}
			if (Level.LevelThree)
			{
				Level.SpawenPoint = SpawningManager.ListPositionMonster[2].gameObject;
			}
			if (Level.LevelFour)
			{
				Level.SpawenPoint = SpawningManager.ListPositionMonster[3].gameObject;
			}
			if (Level.LevelFive)
			{
				Level.SpawenPoint = SpawningManager.ListPositionMonster[4].gameObject;
			}
		}
		if (RoundRoblox)
		{
			if (Level.LevelOne)
			{
				Level.SpawenPoint = SpawningManager.ListPositionRoblox[0].gameObject;
			}
			if (Level.LevelTwo)
			{
				Level.SpawenPoint = SpawningManager.ListPositionRoblox[1].gameObject;
			}
			if (Level.LevelThree)
			{
				Level.SpawenPoint = SpawningManager.ListPositionRoblox[2].gameObject;
			}
			if (Level.LevelFour)
			{
				Level.SpawenPoint = SpawningManager.ListPositionRoblox[3].gameObject;
			}
			if (Level.LevelFive)
			{
				Level.SpawenPoint = SpawningManager.ListPositionRoblox[4].gameObject;
			}
		}
	}

	private void SpawningMonsterToMap()
	{
		if (RoundMonster)
		{
			if (StageController.ListPositionEnemys[0].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[0].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[1].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[1].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[2].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[2].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[3].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[3].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[4].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[4].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[5].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[5].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[6].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[6].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[7].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[7].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[8].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[8].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[9].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[9].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[10].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[10].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[11].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[11].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionEnemys[12].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[12].transform.position, base.transform.rotation);
			}
			return;
		}
		if (StageController.ListPositionEnemys[0].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[0].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[1].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[1].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[2].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[2].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[3].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[3].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[4].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[4].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[5].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[5].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[6].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[6].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[7].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[7].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[8].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[8].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[9].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[9].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[10].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[10].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[11].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[11].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[12].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[12].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionEnemys[13].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListMonsters[Random.Range(0, SpawningManager.ListMonsters.Length)], StageController.ListPositionEnemys[13].transform.position, base.transform.rotation);
		}
	}

	private void SpawningRobloxToMap()
	{
		if (RoundRoblox)
		{
			if (StageController.ListPositionRoblox[0].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[0].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[1].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[1].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[2].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[2].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[3].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[3].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[4].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[4].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[5].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[5].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[6].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[6].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[7].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[7].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[8].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[8].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[9].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[9].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[10].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[10].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[11].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[11].transform.position, base.transform.rotation);
			}
			if (StageController.ListPositionRoblox[12].gameObject.activeSelf)
			{
				Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[12].transform.position, base.transform.rotation);
			}
			return;
		}
		if (StageController.ListPositionRoblox[0].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[0].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[1].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[1].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[2].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[2].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[3].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[3].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[4].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[4].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[5].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[5].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[6].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[6].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[7].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[7].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[8].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[8].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[9].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[9].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[10].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[10].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[11].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[11].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[12].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[12].transform.position, base.transform.rotation);
		}
		if (StageController.ListPositionRoblox[13].gameObject.activeSelf)
		{
			Object.Instantiate(SpawningManager.ListRoblox, StageController.ListPositionRoblox[13].transform.position, base.transform.rotation);
		}
	}

	private void ManagerPosition()
	{
		if (!CameraInCharacter)
		{
			return;
		}
		if (RoundMonster)
		{
			FpsContainer = eventBroker.PlayerActive.GetComponent<CharacterManagerMonster>().FPSCamera.gameObject;
			PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().ControllerTransformPositionCamera.gameObject;
		}
		if (RoundRoblox)
		{
			FpsContainer = eventBroker.PlayerActive.GetComponent<CharacterManager>().FPSCamera.gameObject;
			PositionCamera[0] = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerTransformPositionCamera.gameObject;
		}
		CameraObj.transform.position = Vector3.SmoothDamp(CameraObj.transform.position, PositionCamera[0].transform.position, ref VectorPosition, SmoothMovement);
		CameraObj.transform.rotation = Quaternion.Lerp(CameraObj.transform.rotation, PositionCamera[0].transform.rotation, 0.1f);
		float axis = SimpleInput.GetAxis("Horizontal2");
		float axis2 = SimpleInput.GetAxis("Vertical2");
		float y = eventBroker.PlayerActive.transform.localEulerAngles.y + axis * 5f;
		float x = FpsContainer.transform.localEulerAngles.x - axis2 * 5f;
		FpsContainer.transform.localEulerAngles = new Vector3(x, 0f, 0f);
		eventBroker.PlayerActive.transform.localEulerAngles = new Vector3(0f, y, 0f);
		if (CameraInsideObject)
		{
			SmoothMovement -= Time.deltaTime;
			if (SmoothMovement < 0f)
			{
				CameraObj.gameObject.GetComponent<Camera>().enabled = false;
				CameraFPS.gameObject.SetActive(value: true);
				CameraInsideObject = false;
			}
		}
		else
		{
			_ = CameraInCharacter;
		}
	}

	private void ManagerCurrentGame()
	{
		if (!CameraInCharacter)
		{
			CameraObj.transform.position = Vector3.SmoothDamp(CameraObj.transform.position, TransformOriginalPosition.transform.position, ref VectorPosition, 0.5f);
			CameraObj.transform.rotation = Quaternion.Lerp(CameraObj.transform.rotation, TransformOriginalPosition.transform.rotation, 0.01f);
		}
		if (eventBroker.PlayerActive != null)
		{
			if (RoundMonster)
			{
				HealthBar.fillAmount = (float)eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().Health / 100f;
				HealthAmount.text = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManagerMonster>().Health.ToString() ?? "";
				ManagerPosition();
			}
			if (RoundRoblox)
			{
				HealthBar.fillAmount = (float)eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().Health / 100f;
				HealthAmount.text = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().Health.ToString() ?? "";
				ManagerPosition();
			}
		}
	}

	private void ManagerData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Roblox");
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("Monster");
		if (array != null)
		{
			CurrentRoblox.text = array.Length.ToString() ?? "";
			ActiveCurrentRoblox = array.Length;
		}
		if (array2 != null)
		{
			CurrentMonster.text = array2.Length.ToString() ?? "";
			ActiveCurrentMonsters = array2.Length;
		}
	}

	private void ManagerAmmo()
	{
		if (RoundRoblox && eventBroker.PlayerActive != null)
		{
			AmmoText.text = eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().Ammo;
			BarAmmo.fillAmount = (float)eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().CurrentAmmo / 100f;
		}
	}

	private void ManagerControllerLogic()
	{
		PositionX = FloatingBar.transform.position.x;
		CurrentCoins = PlayerPrefs.GetInt("Coins");
		if (StartChecking && GameStart)
		{
			if (TimeAdsToStart > 0f)
			{
				InterOpenAds.gameObject.SetActive(value: false);
				TimeAdsToStart -= Time.deltaTime;
			}
			else
			{
				InterOpenAds.gameObject.SetActive(value: true);
				if (TimeShowAds > 0f)
				{
					TimeShowAds -= Time.deltaTime;
					ValueAdsMoving.text = "Ads in " + (int)TimeShowAds + " seconds";
				}
				else
				{
					ShowInter();
					TimeShowAds = 4f;
					TimeAdsToStart = 25f;
				}
			}
			ManagerGameFinish();
			ControllerFinishScreen();
		}
		if (RoundRoblox)
		{
			ContainerObstacle.SetActive(value: true);
		}
		else
		{
			ContainerObstacle.SetActive(value: false);
		}
	}

	private void ShowInter()
	{
		Advertisements.Instance.ShowInterstitial();
	}
}
