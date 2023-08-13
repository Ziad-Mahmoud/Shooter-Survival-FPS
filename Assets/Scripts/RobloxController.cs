using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RobloxController : MonoBehaviour
{
	[Header("Controller Manager Body")]
	public SkinnedMeshRenderer ControllerBody;

	public RaycastShooter Shootermanager;

	[Header("Raggdoll Manager")]
	public Rigidbody[] ListRigid;

	public BoxCollider[] ListColliders;

	public CapsuleCollider[] ListCapsules;

	public Animator ControllerManager;

	[Header("Game Object Controller")]
	public GameObject HealthBarObject;

	public GameObject LocalisationPoint;

	public GameObject WeaponContainer;

	[Header("List Weapons")]
	public GameObject TagDead;

	public GameObject[] listWeapons;

	[Header("Dead")]
	public GameObject DeadIcon;

	[Header("Navigation AI")]
	private NavMeshAgent AiMesh;

	[Header("Color Manager")]
	public SpriteRenderer ControllerCurrentMode;

	[Header("Health Bar UI")]
	public Image HealthBar;

	[Header("Objective")]
	public GameObject[] ListMonster;

	public GameObject[] ListRoblox;

	[Header("Integer Controller")]
	internal int HealthPlayer = 100;

	public int CurrentFollowingObject;

	[Header("Floating Controller")]
	internal float TimeShoot = 0.5f;

	[Header("Controller Player")]
	public float SpeedMovement = 5f;

	[Header("Character Controller")]
	private CharacterController ControllerCh;

	[Header("Texture Controller")]
	public Texture[] ListBodys;

	[Header("Boolean Manager")]
	internal bool RoundRoblox;

	internal bool RoundMonster;

	internal bool StartMove = true;

	internal bool CharacterDead = true;

	private void Awake()
	{
		base.gameObject.AddComponent<NavMeshAgent>();
		AiMesh = base.gameObject.GetComponent<NavMeshAgent>();
		AiMesh.radius = 1.25f;
		AiMesh.stoppingDistance = 4f;
		ControllerCh = base.gameObject.GetComponent<CharacterController>();
		listWeapons[Random.Range(0, listWeapons.Length)].gameObject.SetActive(value: true);
		AiMesh.speed = SpeedMovement;
	}

	private void Start()
	{
		Rigidbody[] listRigid = ListRigid;
		for (int i = 0; i < listRigid.Length; i++)
		{
			listRigid[i].isKinematic = true;
		}
		CapsuleCollider[] listCapsules = ListCapsules;
		for (int i = 0; i < listCapsules.Length; i++)
		{
			listCapsules[i].enabled = false;
		}
		BoxCollider[] listColliders = ListColliders;
		for (int i = 0; i < listColliders.Length; i++)
		{
			listColliders[i].enabled = false;
		}
		ControllerBody.material.mainTexture = ListBodys[Random.Range(0, ListBodys.Length)];
	}

	private void Update()
	{
		SetDestination();
		HealthBar.fillAmount = (float)HealthPlayer / 100f;
		if (HealthPlayer > 0)
		{
			return;
		}
		DeadIcon.gameObject.SetActive(value: true);
		if (CharacterDead)
		{
			WeaponContainer.gameObject.SetActive(value: false);
			HealthBarObject.gameObject.SetActive(value: false);
			LocalisationPoint.gameObject.SetActive(value: false);
			TagDead.tag = "Dead";
			base.gameObject.tag = "Dead";
			Object.Destroy(ControllerManager);
			Object.Destroy(AiMesh);
			Rigidbody[] listRigid = ListRigid;
			for (int i = 0; i < listRigid.Length; i++)
			{
				listRigid[i].isKinematic = false;
			}
			CapsuleCollider[] listCapsules = ListCapsules;
			for (int i = 0; i < listCapsules.Length; i++)
			{
				listCapsules[i].enabled = true;
			}
			BoxCollider[] listColliders = ListColliders;
			for (int i = 0; i < listColliders.Length; i++)
			{
				listColliders[i].enabled = true;
			}
			base.gameObject.GetComponent<AudioSource>().Play();
			CharacterDead = false;
		}
	}

	private void FixedUpdate()
	{
		if (HealthPlayer <= 0)
		{
			return;
		}
		_ = ControllerCh.velocity;
		GameObject[] array = listWeapons;
		foreach (GameObject gameObject in array)
		{
			if (!Shootermanager.ShootingDone)
			{
				if (gameObject.activeSelf)
				{
					if (gameObject.name == "Weapon")
					{
						gameObject.gameObject.GetComponent<WeaponShooter>().StartShoot = false;
						ControllerManager.Play("Gun");
					}
					else
					{
						ControllerManager.Play("characterrun_sword");
					}
				}
			}
			else
			{
				if (!gameObject.activeSelf)
				{
					continue;
				}
				if (gameObject.name == "Weapon")
				{
					gameObject.gameObject.GetComponent<WeaponShooter>().StartShoot = true;
					if (TimeShoot > 0f)
					{
						TimeShoot -= Time.deltaTime;
						continue;
					}
					ControllerManager.Play("attack_gun");
					TimeShoot = 1.5f;
				}
				else if (TimeShoot > 0f)
				{
					TimeShoot -= Time.deltaTime;
				}
				else
				{
					ControllerManager.Play("attack_sword");
					TimeShoot = 1.5f;
				}
			}
		}
	}

	private void SetDestination()
	{
		if (HealthPlayer <= 0)
		{
			return;
		}
		if (RoundMonster)
		{
			ListRoblox = GameObject.FindGameObjectsWithTag("RobloxCh");
			if (StartMove)
			{
				CurrentFollowingObject = Random.Range(0, ListRoblox.Length);
				StartMove = false;
			}
			else if (!StartMove)
			{
				GameObject[] listRoblox = ListRoblox;
				for (int i = 0; i < listRoblox.Length; i++)
				{
					if (!(listRoblox[i] != null))
					{
						continue;
					}
					if (CurrentFollowingObject < ListRoblox.Length)
					{
						if (ListRoblox[CurrentFollowingObject] != null)
						{
							AiMesh.SetDestination(ListRoblox[CurrentFollowingObject].transform.position);
						}
					}
					else if (ListRoblox != null)
					{
						Debug.Log("Roblox Has Dead");
						CurrentFollowingObject = Random.Range(0, ListRoblox.Length);
					}
				}
			}
		}
		if (!RoundRoblox)
		{
			return;
		}
		ListMonster = GameObject.FindGameObjectsWithTag("MonsterCh");
		if (StartMove)
		{
			CurrentFollowingObject = Random.Range(0, ListMonster.Length);
			StartMove = false;
		}
		else
		{
			if (StartMove)
			{
				return;
			}
			GameObject[] listRoblox = ListMonster;
			for (int i = 0; i < listRoblox.Length; i++)
			{
				if (!(listRoblox[i] != null))
				{
					continue;
				}
				if (CurrentFollowingObject < ListMonster.Length)
				{
					if (ListMonster[CurrentFollowingObject] != null)
					{
						AiMesh.SetDestination(ListMonster[CurrentFollowingObject].transform.position);
					}
				}
				else if (ListMonster != null)
				{
					Debug.Log("Monster Has Dead");
					CurrentFollowingObject = Random.Range(0, ListMonster.Length);
				}
			}
		}
	}
}
