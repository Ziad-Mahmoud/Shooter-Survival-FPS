using UnityEngine;
using UnityEngine.UI;

public class EventBroker : MonoBehaviour
{
	[Header("Controller Manager UI")]
	public GameObject MiniMap;

	public GameObject PlayerActive;

	[Header("Cameras Controller")]
	public GameObject CameraRadar;

	[Header("Controller Manager UI")]
	public Image ColorTypeModeAuto;

	[Header("Colors Manager")]
	public Color MonsterColor;

	public Color RobloxColor;

	[Header("Controller Role Game")]
	public GameObject RoleRoblox;

	public GameObject RoleMonster;

	[Header("Compoenet Guns")]
	public GameObject HiddenView;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		ManagerRotationPlayer();
	}

	private void FixedUpdate()
	{
	}

	private void ManagerRotationPlayer()
	{
		if (PlayerActive != null)
		{
			float z = Mathf.Atan2(PlayerActive.transform.forward.x, PlayerActive.transform.forward.z) * 57.29578f;
			MiniMap.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, z));
			CameraRadar.transform.position = new Vector3(PlayerActive.transform.position.x, 0f, PlayerActive.transform.position.z);
		}
	}
}
