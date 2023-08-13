using UnityEngine;
using UnityEngine.UI;

public class CurrentTypePlay : MonoBehaviour
{
	[Header("Game Manager")]
	public GameManager ManagerGame;

	[Header("UI Controller")]
	public GameObject CurrentAvatar;

	public GameObject ColorAvatar;

	[Header("UI Text Manager")]
	public GameObject NameTyped;

	[Header("Controller Information")]
	public Sprite CurrentSprite;

	public string CurrentName;

	[Header("Buttons Manager")]
	private Button Controller;

	[Header("Boolean Controller")]
	public bool IsMonster;

	public bool IsGun;

	private void Start()
	{
		Controller = GetComponent<Button>();
		Transform obj = base.transform.Find("RoleIcon");
		Transform transform = base.transform.Find("RoleName");
		if (obj != null)
		{
			_ = transform != null;
		}
		Controller = base.gameObject.GetComponent<Button>();
		base.name = CurrentName;
		Controller.onClick.RemoveAllListeners();
		Controller.onClick.AddListener(ClickedBtn);
	}

	private void Update()
	{
		if (CurrentAvatar != null && ColorAvatar != null && NameTyped != null)
		{
			Text component = NameTyped.GetComponent<Text>();
			Image component2 = CurrentAvatar.GetComponent<Image>();
			Image component3 = ColorAvatar.GetComponent<Image>();
			if (IsMonster)
			{
				component3.color = Color.red;
				component2.sprite = CurrentSprite;
				component.text = CurrentName;
			}
			else if (IsGun)
			{
				component3.color = Color.yellow;
				component2.sprite = CurrentSprite;
				component.text = CurrentName;
			}
		}
	}

	private void ClickedBtn()
	{
		Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
		void CompleteMethod(bool completed, string advertiser)
		{
			Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
			if (completed)
			{
				PlayerPrefs.SetString("CurrentUsed", CurrentName);
				if (IsMonster)
				{
					ManagerGame.CurrentAutoSelectplayNoThanks = 0;
				}
				if (IsGun)
				{
					ManagerGame.CurrentAutoSelectplayNoThanks = 1;
				}
				ManagerGame.AutoSelectNoThanks = false;
				ManagerGame.NoThankRole();
			}
		}
	}
}
