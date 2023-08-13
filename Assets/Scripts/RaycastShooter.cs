using UnityEngine;

public class RaycastShooter : MonoBehaviour
{
	[Header("Boolean Manager")]
	internal bool ShootingDone;

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.forward), out var hitInfo, 20f))
		{
			Debug.DrawRay(base.transform.position, base.transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
			if (hitInfo.collider.tag == "MonsterCh")
			{
				ShootingDone = true;
			}
			else
			{
				ShootingDone = false;
			}
		}
	}
}
