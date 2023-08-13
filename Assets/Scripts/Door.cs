using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
	[Header("Boolean Controller")]
	internal bool DoorIsOpen;

	internal bool SoundOpen = true;

	[Header("Door Locked")]
	public bool DoorLocked;

	[Header("Audio Source Controller")]
	private AudioSource DoorOpend;

	private void Start()
	{
		DoorOpend = base.gameObject.GetComponent<AudioSource>();
	}

	private void FixedUpdate()
	{
		if (DoorIsOpen && base.transform.rotation.z < 0.48f && !DoorLocked)
		{
			base.transform.Rotate(0f, 0f, base.transform.localRotation.z + 0.9f);
			if (SoundOpen)
			{
				DoorOpend.Play();
				base.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
				SoundOpen = false;
			}
		}
		else if (DoorLocked && DoorIsOpen)
		{
			Debug.Log("This Door Its Locked ");
		}
	}
}
