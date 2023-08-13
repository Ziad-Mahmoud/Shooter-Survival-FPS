using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
	[Header("Controller Sounds")]
	private AudioSource CurrentSource;

	[Header("Effect Shooting")]
	public GameObject ShooterSplash;

	public GameObject ContainerBullet;

	[Header("Dead Component")]
	public GameObject[] ListObjectDead;

	[Header("Controller HIt")]
	public AudioSource HitSource;

	[Header("DeadItk")]
	public GameObject DeadIcon;

	[Header("UI Image Sprite Level")]
	public Image CurrentHealthBar;

	public Sprite CircleSprite;

	public SpriteRenderer CurrentType;

	[Header("Raggdoll Controller")]
	public Rigidbody[] ListRigids;

	public Animator CurrentAnim;

	[Header("Objective")]
	public GameObject TagController;

	public GameObject[] ListMonster;

	public GameObject[] ListRoblox;

	[Header("Navigation Ai")]
	private NavMeshAgent AiMesh;

	private CharacterController ControllerManager;

	[Header("Controller Clips")]
	public AudioClip[] ListClips;

	[Header("Controller Player")]
	public float SpeedMovement = 5f;

	[Header("Integer Controller Manager")]
	internal int CurrentHealth = 100;

	public int CurrentFollowingObject;

	[Header("Boolean Manager")]
	internal bool RoundRoblox;

	internal bool RoundMonster;

	internal bool StartMove = true;

	internal bool CharacteritsAlive = true;

	internal bool Dead = true;

	private void Awake()
	{
		ContainerBullet = GameObject.Find("BG");
		base.gameObject.AddComponent<NavMeshAgent>();
		AiMesh = base.gameObject.GetComponent<NavMeshAgent>();
		AiMesh.radius = 1.1f;
		AiMesh.speed = SpeedMovement;
		CurrentType.sprite = CircleSprite;
		CurrentType.gameObject.transform.localPosition = new Vector3(0f, -0.43f, 0f);
		CurrentType.gameObject.transform.localScale = new Vector3(4f, 4f, 4f);
		Rigidbody[] listRigids = ListRigids;
		foreach (Rigidbody obj in listRigids)
		{
			obj.useGravity = false;
			obj.isKinematic = true;
		}
	}

	private void Start()
	{
		ControllerManager = base.gameObject.GetComponent<CharacterController>();
		CurrentSource = base.gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (CurrentHealth <= 0)
		{
			DeadIcon.gameObject.SetActive(value: true);
			GameObject[] listObjectDead = ListObjectDead;
			for (int i = 0; i < listObjectDead.Length; i++)
			{
				listObjectDead[i].gameObject.SetActive(value: false);
			}
			if (Dead)
			{
				base.gameObject.GetComponent<AudioSource>().Play();
				Dead = false;
			}
			base.gameObject.GetComponent<CharacterController>().enabled = false;
			CurrentAnim.enabled = false;
			CharacteritsAlive = false;
			base.gameObject.tag = "Dead";
			TagController.tag = "Dead";
			Object.Destroy(AiMesh);
			base.gameObject.GetComponent<BoxCollider>().enabled = false;
			Object.Destroy(GetComponent<Rigidbody>());
			Rigidbody[] listRigids = ListRigids;
			foreach (Rigidbody obj in listRigids)
			{
				obj.useGravity = true;
				obj.isKinematic = false;
			}
		}
		if (CharacteritsAlive)
		{
			SetDestination();
			CurrentHealthBar.fillAmount = (float)CurrentHealth / 100f;
		}
	}

	private void FixedUpdate()
	{
		_ = ControllerManager.velocity;
		if (CurrentAnim.enabled)
		{
			CurrentAnim.Play("run_FPS");
		}
	}

	private void SetDestination()
	{
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Bullet"))
		{
			CurrentHealth -= 3;
			Object.Instantiate(ShooterSplash, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity).transform.SetParent(ContainerBullet.transform);
		}
		if (other.CompareTag("RobloxCh"))
		{
			HitSource.Play();
			other.gameObject.GetComponent<RobloxController>().HealthPlayer -= 40;
		}
	}
}
