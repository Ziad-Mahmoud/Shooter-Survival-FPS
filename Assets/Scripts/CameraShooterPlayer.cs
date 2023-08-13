using UnityEngine;

public class CameraShooterPlayer : MonoBehaviour
{
	[Header("Boolean manager")]
	internal bool StartShooting;

	[Header("Object Container")]
	public GameObject[] ListWeapons;

	private void Update()
	{
		if (!Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), out var hitInfo, 80f))
		{
			return;
		}
		Debug.DrawRay(base.transform.position, base.transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.blue);
		GameObject[] listWeapons;
		if (hitInfo.collider.tag == "MonsterCh")
		{
			listWeapons = ListWeapons;
			foreach (GameObject gameObject in listWeapons)
			{
				if (gameObject.gameObject.activeSelf)
				{
					StartShooting = true;
					gameObject.gameObject.GetComponent<WeaponShooter>().StartShoot = true;
				}
			}
			return;
		}
		listWeapons = ListWeapons;
		foreach (GameObject gameObject2 in listWeapons)
		{
			if (gameObject2.gameObject.activeSelf)
			{
				StartShooting = false;
				gameObject2.gameObject.GetComponent<WeaponShooter>().StartShoot = false;
			}
		}
	}
}
