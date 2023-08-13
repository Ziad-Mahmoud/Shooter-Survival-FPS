using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class CharacterManagerMonster : MonoBehaviour
{
	[Header("Animator Controller")]
	public Animator[] AnimManager;

	[Header("Manager Controller")]
	public GameManager ManagerGame;

	[Header("Current Monster")]
	public GameObject[] ListMonster;

	public GameObject[] ListPositionCamera;

	[Header("FPS Camera Manager")]
	public GameObject FPSCamera;

	public GameObject ControllerTransformPositionCamera;

	[Header("Physics Controller")]
	public Rigidbody[] ListRigid;

	[Header("Audio Management")]
	public AudioSource FootSteps;

	public AudioSource MonsterAttack;

	[Header("Manager Componenet Effect")]
	public GameObject ShooterSplash;

	public GameObject ContainerBullet;

	[Header("Floating Controller")]
	public float speed = 6f;

	public float jumpSpeed = 8f;

	public float MoveFast = 1f;

	public float gravity = 20f;

    [Header("CameraSettings")]
    [Tooltip("Mouse Sensetivity value")]
    public float mouseSensetivity;
    [Tooltip("Main camera transform")]
    public Transform cameraTransform;
    private float clampX;
    private float clampY;

    [Tooltip("Clamp camera by Y axis")]
    public bool clampByY;
    public Vector2 clampXaxis;
    public Vector2 clampYaxis;
    [Header("Integer Controller")]
	internal int Health = 100;

	[Header("Boolean Manager")]
	public bool PlayerIsDead;

	public bool IsCurrentPlayer = true;

	internal bool IsMoving;

	internal bool JumpBtn;

	private Vector3 moveDirection = Vector3.zero;

	private CharacterController controller;

	private void Awake()
	{
		ManagerGame = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
		ContainerBullet = GameObject.Find("BG");
		GameObject[] listPositionCamera = ListPositionCamera;
		for (int i = 0; i < listPositionCamera.Length; i++)
		{
			Object.Destroy(listPositionCamera[i].gameObject.GetComponent<CameraManager>());
		}
		Rigidbody[] listRigid = ListRigid;
		foreach (Rigidbody rigidbody in listRigid)
		{
			if (rigidbody.gameObject.GetComponent<BoxCollider>() != null)
			{
				rigidbody.gameObject.GetComponent<BoxCollider>().enabled = false;
			}
			if (rigidbody.gameObject.GetComponent<CapsuleCollider>() != null)
			{
				rigidbody.gameObject.GetComponent<CapsuleCollider>().enabled = false;
			}
			rigidbody.isKinematic = true;
		}
	}

	private void Start()
	{
		controller = base.gameObject.GetComponent<CharacterController>();
		GameObject[] listMonster = ListMonster;
		foreach (GameObject gameObject in listMonster)
		{
			if (!(gameObject.name == PlayerPrefs.GetString("CurrentUsed")))
			{
				continue;
			}
			gameObject.SetActive(value: true);
			if (!gameObject.gameObject.activeSelf)
			{
				continue;
			}
			GameObject[] listPositionCamera = ListPositionCamera;
			foreach (GameObject gameObject2 in listPositionCamera)
			{
				if (gameObject2.gameObject.name == gameObject.gameObject.name)
				{
					ControllerTransformPositionCamera.transform.parent = gameObject2.transform;
					ControllerTransformPositionCamera.transform.rotation = Quaternion.identity;
					ControllerTransformPositionCamera.transform.localPosition = Vector3.zero;
				}
			}
		}
	}

	private void Update()
	{
		if (PlayerIsDead)
		{
			base.gameObject.tag = "Player";
			Rigidbody[] listRigid = ListRigid;
			for (int i = 0; i < listRigid.Length; i++)
			{
				listRigid[i].isKinematic = false;
				Animator[] animManager = AnimManager;
				foreach (Animator animator in animManager)
				{
					if (animator.gameObject.activeSelf)
					{
						animator.enabled = false;
					}
				}
			}
		}
		else
		{
			base.gameObject.tag = "MonsterCh";
			Rigidbody[] listRigid = ListRigid;
			for (int i = 0; i < listRigid.Length; i++)
			{
				listRigid[i].isKinematic = true;
				Animator[] animManager = AnimManager;
				for (int j = 0; j < animManager.Length; j++)
				{
					_ = animManager[j].gameObject.activeSelf;
				}
			}
		}
		MoveManager();
        CameraRotation();
    }

	private void FixedUpdate()
	{
		ManagerAnimation();
	}

	public void Jump()
	{
		JumpBtn = true;
	}

	private void ManagerAnimation()
	{
		Animator[] animManager;
		if (controller.velocity.magnitude != 0f)
		{
			animManager = AnimManager;
			foreach (Animator animator in animManager)
			{
				if (animator.enabled && animator.gameObject.activeSelf)
				{
					animator.Play("run_FPS");
				}
			}
			if (IsMoving)
			{
				FootSteps.Play();
				IsMoving = false;
			}
			return;
		}
		if (!IsMoving)
		{
			FootSteps.Stop();
			IsMoving = true;
		}
		animManager = AnimManager;
		foreach (Animator animator2 in animManager)
		{
			if (animator2.enabled && animator2.gameObject.activeSelf)
			{
				animator2.Play("idle_FPS");
			}
		}
	}
    private void CameraRotation()
    {
        float mouseX = CrossPlatformInputManager.GetAxis("Mouse X") * (mouseSensetivity * 2) * Time.deltaTime;
        float mouseY = CrossPlatformInputManager.GetAxis("Mouse Y") * (mouseSensetivity * 2) * Time.deltaTime;

        clampX += mouseY;
        clampY += mouseX;

        if (clampX > clampXaxis.y)
        {
            clampX = clampXaxis.y;
            mouseY = 0.0f;
            ClampXAxis(clampXaxis.x);
        }
        else if (clampX < clampXaxis.x)
        {
            clampX = clampXaxis.x;
            mouseY = 0.0f;
            ClampXAxis(clampXaxis.y);
        }



        if (clampByY)
        {

            if (clampY > clampYaxis.y)
            {
                clampY = clampYaxis.y;
                mouseX = 0.0f;
                ClampYAxis(clampYaxis.y);
            }
            else if (clampY < clampYaxis.x)
            {
                clampY = clampYaxis.x;
                mouseX = 0.0f;
                ClampYAxis(clampYaxis.x);
            }
        }


        cameraTransform.Rotate(Vector3.left * mouseY);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void ClampXAxis(float value)
    {
        Vector3 camEuler = cameraTransform.eulerAngles;
        camEuler.x = value;
        cameraTransform.eulerAngles = camEuler;
    }

    private void ClampYAxis(float value)
    {
        Vector3 camEuler = transform.eulerAngles;
        camEuler.y = value;
        transform.eulerAngles = camEuler;
    }

    private void MoveManager()
	{
		if (controller.isGrounded)
		{
			moveDirection = new Vector3(SimpleInput.GetAxis("Horizontal"), 0f, SimpleInput.GetAxis("Vertical"));
			moveDirection = base.transform.TransformDirection(moveDirection);
			if (JumpBtn)
			{
				moveDirection.y = jumpSpeed;
				JumpBtn = false;
			}
			moveDirection *= PlayerPrefs.GetFloat("AdditionSpeed") + speed * MoveFast;
		}
		else
		{
			FootSteps.Stop();
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("RobloxCh"))
		{
			Debug.Log("Killed Roblox :" + other.name);
			other.gameObject.GetComponent<RobloxController>().HealthPlayer -= 40;
		}
		if (other.CompareTag("Bullet"))
		{
			if (Health > 3)
			{
				Health -= 3;
				MonsterAttack.Play();
				Object.Instantiate(ShooterSplash, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity).transform.SetParent(ContainerBullet.transform);
			}
			else
			{
				ManagerGame.GameOver = true;
			}
		}
	}
}
