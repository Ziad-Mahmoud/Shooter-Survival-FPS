using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[Header("Manager Controller")]
	public CharacterList CharacterManager;

	public GameManager ManagerGame;

	[Header("Level Manager")]
	public Door[] DoorList;

	[Header("position Spawning")]
	public GameObject SpawenPoint;

	[Header("List AllDoors")]
	public Door[] ListDoors;

	[Header("UI Controller Manager")]
	public Text CurrentLevel;

	[Header("Boolean Manager")]
	public bool LevelOne;

	public bool LevelTwo;

	public bool LevelThree;

	public bool LevelFour;

	public bool LevelFive;

	[Header("Position Manager")]
	public GameObject[] ListPositionLevelOne;

	public GameObject[] ListPositionLevelTwo;

	public GameObject[] ListPositionLevelThree;

	public GameObject[] ListPositionLevelFour;

	public GameObject[] ListPositionLevelFive;

	[Header("Level Container")]
	public GameObject[] ListPerfabe;

	private void Update()
	{
		if (PlayerPrefs.GetInt("CurrentLevel") == 0)
		{
			LevelOne = true;
			ListPerfabe[0].gameObject.SetActive(value: true);
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") == 1)
		{
			ListPerfabe[0].gameObject.SetActive(value: false);
			ListPerfabe[1].gameObject.SetActive(value: true);
			Door[] listDoors = ListDoors;
			foreach (Door door in listDoors)
			{
				if (door.gameObject.name == "Lv2")
				{
					door.DoorIsOpen = true;
				}
			}
			LevelOne = false;
			LevelTwo = true;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") == 2)
		{
			ListPerfabe[0].gameObject.SetActive(value: false);
			ListPerfabe[1].gameObject.SetActive(value: false);
			ListPerfabe[2].gameObject.SetActive(value: true);
			Door[] listDoors = ListDoors;
			foreach (Door door2 in listDoors)
			{
				if (door2.gameObject.name == "Lv2")
				{
					door2.DoorIsOpen = true;
				}
				if (door2.gameObject.name == "Lv3")
				{
					door2.DoorIsOpen = true;
				}
			}
			LevelOne = false;
			LevelTwo = false;
			LevelThree = true;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") == 3)
		{
			ListPerfabe[0].gameObject.SetActive(value: false);
			ListPerfabe[1].gameObject.SetActive(value: false);
			ListPerfabe[2].gameObject.SetActive(value: false);
			ListPerfabe[3].gameObject.SetActive(value: true);
			Door[] listDoors = ListDoors;
			foreach (Door door3 in listDoors)
			{
				if (door3.gameObject.name == "Lv2")
				{
					door3.DoorIsOpen = true;
				}
				if (door3.gameObject.name == "Lv3")
				{
					door3.DoorIsOpen = true;
				}
				if (door3.gameObject.name == "Lv4")
				{
					door3.DoorIsOpen = true;
				}
			}
			LevelOne = false;
			LevelThree = false;
			LevelTwo = false;
			LevelFour = true;
		}
		else if (PlayerPrefs.GetInt("CurrentLevel") == 4)
		{
			ListPerfabe[0].gameObject.SetActive(value: false);
			ListPerfabe[1].gameObject.SetActive(value: false);
			ListPerfabe[2].gameObject.SetActive(value: false);
			ListPerfabe[3].gameObject.SetActive(value: false);
			ListPerfabe[4].gameObject.SetActive(value: true);
			Door[] listDoors = ListDoors;
			foreach (Door door4 in listDoors)
			{
				if (door4.gameObject.name == "Lv2")
				{
					door4.DoorIsOpen = true;
				}
				if (door4.gameObject.name == "Lv3")
				{
					door4.DoorIsOpen = true;
				}
				if (door4.gameObject.name == "Lv4")
				{
					door4.DoorIsOpen = true;
				}
				if (door4.gameObject.name == "Lv5")
				{
					door4.DoorIsOpen = true;
				}
			}
			LevelOne = false;
			LevelTwo = false;
			LevelThree = false;
			LevelFive = true;
		}
		ManagerPostionSpawning();
	}

	private void ManagerPostionSpawning()
	{
		if (LevelOne)
		{
			CurrentLevel.text = "Level 1";
			GameObject[] listPositionLevelOne = ListPositionLevelOne;
			for (int i = 0; i < listPositionLevelOne.Length; i++)
			{
				listPositionLevelOne[i].SetActive(value: true);
			}
		}
		else if (LevelTwo)
		{
			CurrentLevel.text = "Level 2";
			Door[] doorList = DoorList;
			for (int i = 0; i < doorList.Length; i++)
			{
				doorList[i].DoorLocked = false;
			}
			GameObject[] listPositionLevelOne = ListPositionLevelTwo;
			for (int i = 0; i < listPositionLevelOne.Length; i++)
			{
				listPositionLevelOne[i].SetActive(value: true);
			}
		}
		else if (LevelThree)
		{
			CurrentLevel.text = "Level 3";
			Door[] doorList = DoorList;
			for (int i = 0; i < doorList.Length; i++)
			{
				doorList[i].DoorLocked = false;
			}
			GameObject[] listPositionLevelOne = ListPositionLevelThree;
			for (int i = 0; i < listPositionLevelOne.Length; i++)
			{
				listPositionLevelOne[i].SetActive(value: true);
			}
		}
		else if (LevelFour)
		{
			CurrentLevel.text = "Level 4";
			Door[] doorList = DoorList;
			for (int i = 0; i < doorList.Length; i++)
			{
				doorList[i].DoorLocked = false;
			}
			GameObject[] listPositionLevelOne = ListPositionLevelFour;
			for (int i = 0; i < listPositionLevelOne.Length; i++)
			{
				listPositionLevelOne[i].SetActive(value: true);
			}
		}
		else if (LevelFive)
		{
			CurrentLevel.text = "Level 5";
			Door[] doorList = DoorList;
			for (int i = 0; i < doorList.Length; i++)
			{
				doorList[i].DoorLocked = false;
			}
			GameObject[] listPositionLevelOne = ListPositionLevelFive;
			for (int i = 0; i < listPositionLevelOne.Length; i++)
			{
				listPositionLevelOne[i].SetActive(value: true);
			}
		}
	}
}
