using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[Header("Mnaager Controller")]
	public GameManager Manager;

	[Header("Camera")]
	private Camera _camera;

	[Header("boolean Manager")]
	internal bool CheckPostion;

	public bool MakeThatsWork;

	[Header("Mask Component")]
	public LayerMask layerMask;

	private void Awake()
	{
		Manager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
		_camera = base.gameObject.GetComponent<Camera>();
	}

	private void Update()
	{
		CheckerDoors();
		CheckerEnemys();
	}

	private void CheckerDoors()
	{
		if (Manager.RoundRoblox)
		{
			Debug.DrawRay(base.transform.position, Vector3.forward * 1.4f, Color.red);
			if (Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out var hitInfo, 1.4f, layerMask))
			{
				hitInfo.transform.gameObject.GetComponent<Door>().DoorIsOpen = true;
			}
		}
		if (Manager.RoundMonster)
		{
			Debug.DrawRay(base.transform.position, Vector3.forward * 3.4f, Color.red);
			if (Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out var hitInfo2, 3.4f, layerMask))
			{
				hitInfo2.transform.gameObject.GetComponent<Door>().DoorIsOpen = true;
			}
		}
	}

	private void CheckerEnemys()
	{
		if (!Manager.RoundRoblox || !MakeThatsWork || !(Manager.eventBroker.PlayerActive != null))
		{
			return;
		}
		if (Manager.eventBroker.PlayerActive.gameObject.GetComponent<CharacterManager>().ControllerWeapon.StartShooting)
		{
			if (CheckPostion)
			{
				base.gameObject.GetComponent<Animator>().Play("CameraShooter");
				CheckPostion = false;
			}
		}
		else if (!CheckPostion)
		{
			base.gameObject.GetComponent<Animator>().Play("CameraShooterOut");
			CheckPostion = true;
		}
	}
}
