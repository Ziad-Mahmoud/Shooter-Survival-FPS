using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
	[Header("Manager Controller")]
	public CameraShooterPlayer ControllerWeapon;

	public GameManager ManagerGame;

	[Header("Animator Controller")]
	public Animator AnimManager;

	[Header("FPS Camera Manager")]
	public GameObject FPSCamera;

	public GameObject ControllerTransformPositionCamera;

	[Header("List Weapons")]
	public GameObject[] ListWeapons;

	[Header("Physics Controller")]
	public Rigidbody[] ListRigid;

	[Header("Audio Management")]
	public AudioSource FootSteps;

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

	internal int MaxAmmo = 200;

	internal int CurrentAmmo;

	[Header("Strings Controller")]
	internal string Ammo = "";

	[Header("Boolean Manager")]
	public bool PlayerIsDead;

	public bool IsCurrentPlayer = true;

	internal bool IsMoving;

	internal bool JumpBtn;

	internal bool ReloadAmmo;

	private Vector3 moveDirection = Vector3.zero;

	private CharacterController controller;

	private void Awake()
	{
		ManagerGame = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
	}

	private void Start()
	{
		controller = base.gameObject.GetComponent<CharacterController>();
		GameObject[] listWeapons = ListWeapons;
		foreach (GameObject gameObject in listWeapons)
		{
			if (gameObject.name == PlayerPrefs.GetString("CurrentUsed"))
			{
				gameObject.SetActive(value: true);
			}
		}
	}

	private void Update()
	{
		if (PlayerIsDead)
		{
			Rigidbody[] listRigid = ListRigid;
			for (int i = 0; i < listRigid.Length; i++)
			{
				listRigid[i].isKinematic = false;
				AnimManager.enabled = false;
			}
		}
		else
		{
			Rigidbody[] listRigid = ListRigid;
			for (int i = 0; i < listRigid.Length; i++)
			{
				listRigid[i].isKinematic = true;
				AnimManager.enabled = true;
			}
		}
		Ammo = CurrentAmmo + "/" + MaxAmmo;
		ManagerAmmo();
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

	private void ManagerAmmo()
	{
		GameObject[] listWeapons = ListWeapons;
		foreach (GameObject gameObject in listWeapons)
		{
			if (gameObject.gameObject.activeSelf && gameObject.GetComponent<WeaponShooter>() != null)
			{
				WeaponShooter component = gameObject.gameObject.GetComponent<WeaponShooter>();
				CurrentAmmo = component.CurrentAmmo;
				if (component.CurrentAmmo == 1)
				{
					ReloadAmmo = true;
				}
			}
		}
	}

	internal void Reload()
	{
		if (!ReloadAmmo || MaxAmmo <= 100)
		{
			return;
		}
		GameObject[] listWeapons = ListWeapons;
		foreach (GameObject gameObject in listWeapons)
		{
			if (gameObject.gameObject.activeSelf && gameObject.GetComponent<WeaponShooter>() != null)
			{
				gameObject.gameObject.GetComponent<WeaponShooter>().CurrentAmmo = 100;
			}
		}
		MaxAmmo -= 100;
	}

	private void ManagerAnimation()
	{
		GameObject[] listWeapons;
		if (controller.velocity.magnitude != 0f)
		{
			listWeapons = ListWeapons;
			foreach (GameObject gameObject in listWeapons)
			{
				if (!gameObject.gameObject.activeSelf)
				{
					continue;
				}
				if (gameObject.gameObject.GetComponent<WeaponShooter>() != null)
				{
					AnimManager.Play("Gun");
					if (IsMoving)
					{
						FootSteps.Play();
						IsMoving = false;
					}
				}
				else
				{
					AnimManager.Play("characterrun_sword");
					if (IsMoving)
					{
						FootSteps.Play();
						IsMoving = false;
					}
				}
			}
			return;
		}
		listWeapons = ListWeapons;
		foreach (GameObject gameObject2 in listWeapons)
		{
			if (!gameObject2.gameObject.activeSelf)
			{
				continue;
			}
			if (gameObject2.gameObject.GetComponent<WeaponShooter>() != null)
			{
				if (!IsMoving)
				{
					FootSteps.Stop();
					IsMoving = true;
				}
				AnimManager.Play("character_bones|idle_gun");
			}
			else
			{
				AnimManager.Play("idle_sword");
				if (IsMoving)
				{
					FootSteps.Play();
					IsMoving = false;
				}
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
		if (other.CompareTag("MonsterCh"))
		{
			GameObject[] listWeapons = ListWeapons;
			foreach (GameObject gameObject in listWeapons)
			{
				if (gameObject.gameObject.activeSelf && !(gameObject.gameObject.GetComponent<WeaponShooter>() != null))
				{
					other.gameObject.GetComponent<MonsterAI>().CurrentHealth -= 20;
					AnimManager.Play("attack_sword");
				}
			}
			if (Health > 10)
			{
				Health -= 10;
			}
			else
			{
				ManagerGame.GameOver = true;
			}
		}
		if (other.CompareTag("Collectibles"))
		{
			other.gameObject.GetComponent<LetterManagement>().BoxChecked = true;
			other.gameObject.SetActive(value: false);
		}
	}
}
