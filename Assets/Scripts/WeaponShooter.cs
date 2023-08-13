using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
	[Header("Shooting Balls")]
	public bool StartShoot;

	[Header("Floating Manager")]
	public float ShootingTime = 0.1f;

	[Header("Audio Source Shoot")]
	public AudioSource ShootingSound;

	[Header("Object To Spawn ON")]
	public GameObject SpawnContainer;

	[Header("Inisialize Time")]
	internal float ShotStart = 0.07f;

	[Header("Integer Controller")]
	internal int CurrentAmmo = 100;

	[Header("Effect Controller")]
	public ParticleSystem[] ListParticles;

	[Header("Postion Shoot")]
	public GameObject PositionShoot;

	public GameObject BulletShooter;

	private void Awake()
	{
		SpawnContainer = GameObject.Find("BG");
		ShootingSound = base.gameObject.GetComponent<AudioSource>();
	}

	private void Start()
	{
		ShotStart = ShootingTime;
	}

	private void Update()
	{
		if (!StartShoot)
		{
			return;
		}
		if (ShootingTime > 0f)
		{
			ShootingTime -= Time.deltaTime;
		}
		else if (CurrentAmmo > 1)
		{
			Object.Instantiate(BulletShooter, PositionShoot.transform.position, PositionShoot.transform.rotation).transform.SetParent(SpawnContainer.transform);
			ShootingSound.Play();
			ParticleSystem[] listParticles = ListParticles;
			for (int i = 0; i < listParticles.Length; i++)
			{
				listParticles[i].Play();
			}
			CurrentAmmo--;
			ShootingTime = ShotStart;
		}
	}
}
