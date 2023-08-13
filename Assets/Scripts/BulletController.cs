using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[Header("Physics Controller")]
	private Rigidbody rb;

	[Header("Floating Controller")]
	public float SpeedMove = 1000f;

	private void Start()
	{
		rb = base.gameObject.GetComponent<Rigidbody>();
		StartCoroutine(DestroyingTime());
	}

	private void Update()
	{
		base.transform.Translate(-Vector3.forward * SpeedMove * Time.deltaTime);
	}

	private void FixedUpdate()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("MonsterCh"))
		{
			Debug.Log("Enemy hit by bullet");
		}
	}

	private IEnumerator DestroyingTime()
	{
		yield return new WaitForSeconds(2f);
		Object.Destroy(base.gameObject);
	}
}
