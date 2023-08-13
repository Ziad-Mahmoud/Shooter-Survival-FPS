using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
	public Material skyboxMaterial;

	public float rotationSpeed;

	private void Start()
	{
		skyboxMaterial.SetFloat("_Rotation", 140f);
	}

	private void Update()
	{
		_ = Time.time;
		_ = rotationSpeed;
	}
}
