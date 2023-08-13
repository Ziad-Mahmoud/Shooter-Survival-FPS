using UnityEngine;

public class ShopController : MonoBehaviour
{
	[Header("Objects Controller")]
	public GameObject TabBoot;

	public GameObject ContentBoot;

	public GameObject TabRoblox;

	public GameObject ContentRoblox;

	public void Boot()
	{
		TabBoot.gameObject.SetActive(value: false);
		ContentBoot.gameObject.SetActive(value: true);
		TabRoblox.gameObject.SetActive(value: true);
		ContentRoblox.gameObject.SetActive(value: false);
	}

	public void Roblox()
	{
		TabBoot.gameObject.SetActive(value: true);
		ContentBoot.gameObject.SetActive(value: false);
		TabRoblox.gameObject.SetActive(value: false);
		ContentRoblox.gameObject.SetActive(value: true);
	}
}
