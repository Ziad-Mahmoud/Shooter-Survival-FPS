using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
	[Header("Controller")]
	public LevelManager Level;

	public GameManager Manager;

	[Header("UI Controller")]
	public Text CurrentView;

	[Header("String Controller")]
	public string CurrentName = "";

	[Header("LevelOne")]
	private string MotA = "D";

	private string MotB = "O";

	private string MotC = "O";

	private string MotD = "R";

	private string MotE = "B";

	private string MotF = "F";

	private string MotG = "H";

	[Header("Boolean")]
	internal bool MotOne;

	internal bool MotTwo;

	internal bool MotThree;

	internal bool MotFour;

	internal bool MotFive;

	internal bool MotSix;

	internal bool MotSeven;

	[Header("Boolean Manager")]
	internal bool FixedWin = true;

	[Header("Container Levels")]
	public GameObject[] ListLevelOne;

	public GameObject[] ListLevelTwo;

	public GameObject[] ListLevelThree;

	public GameObject[] ListLevelFour;

	public GameObject[] ListLevelFive;

	[Header("Boolean Manager")]
	internal bool CheckText = true;

	internal bool FixStation = true;

	private void Update()
	{
		if (Manager.RoundRoblox)
		{
			if (FixStation)
			{
				FixStation = false;
			}
			if (Level.LevelOne)
			{
				ManagerLevelOne();
			}
			if (Level.LevelTwo)
			{
				ManagerLevelTwo();
			}
			if (Level.LevelThree)
			{
				ManagerLevelThree();
			}
			if (Level.LevelFour)
			{
				ManagerLevelFour();
			}
			if (Level.LevelFive)
			{
				ManagerLevelFive();
			}
		}
		else if (!FixStation)
		{
			FixStation = true;
		}
	}

	internal void ResetValueFixed()
	{
		GameObject[] listLevelOne = ListLevelOne;
		foreach (GameObject obj in listLevelOne)
		{
			obj.gameObject.SetActive(value: false);
			obj.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelTwo;
		foreach (GameObject obj2 in listLevelOne)
		{
			obj2.gameObject.SetActive(value: false);
			obj2.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelThree;
		foreach (GameObject obj3 in listLevelOne)
		{
			obj3.gameObject.SetActive(value: false);
			obj3.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelFour;
		foreach (GameObject obj4 in listLevelOne)
		{
			obj4.gameObject.SetActive(value: false);
			obj4.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelFive;
		foreach (GameObject obj5 in listLevelOne)
		{
			obj5.gameObject.SetActive(value: false);
			obj5.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
	}

	internal void ResetValue()
	{
		GameObject[] listLevelOne = ListLevelOne;
		foreach (GameObject obj in listLevelOne)
		{
			obj.gameObject.SetActive(value: true);
			obj.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelTwo;
		foreach (GameObject obj2 in listLevelOne)
		{
			obj2.gameObject.SetActive(value: true);
			obj2.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelThree;
		foreach (GameObject obj3 in listLevelOne)
		{
			obj3.gameObject.SetActive(value: true);
			obj3.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelFour;
		foreach (GameObject obj4 in listLevelOne)
		{
			obj4.gameObject.SetActive(value: true);
			obj4.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
		listLevelOne = ListLevelFive;
		foreach (GameObject obj5 in listLevelOne)
		{
			obj5.gameObject.SetActive(value: true);
			obj5.gameObject.GetComponent<LetterManagement>().BoxChecked = false;
		}
	}

	private void ManagerLevelOne()
	{
		if (CheckText)
		{
			CurrentName = "DOOR";
			MotA = "D";
			MotB = "O";
			MotC = "O";
			MotD = "R";
			CheckText = false;
		}
		CurrentName = MotA + MotB + MotC + MotD;
		CurrentView.text = "<size=40>Collect letters</size>" + CurrentName;
		if (ListLevelOne[0].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotOne = true;
			MotA = "<color=yellow>D</color>";
		}
		if (ListLevelOne[1].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotTwo = true;
			MotB = "<color=yellow>O</color>";
		}
		if (ListLevelOne[2].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotThree = true;
			MotC = "<color=yellow>O</color>";
		}
		if (ListLevelOne[3].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotFour = true;
			MotD = "<color=yellow>R</color>";
		}
		if (MotOne && MotTwo && MotThree && MotFour && FixedWin)
		{
			Manager.GameWin = true;
			FixedWin = false;
		}
	}

	private void ManagerLevelTwo()
	{
		if (CheckText)
		{
			CurrentName = "WOOD";
			MotA = "W";
			MotB = "O";
			MotC = "O";
			MotD = "D";
			CheckText = false;
		}
		CurrentName = MotA + MotB + MotC + MotD;
		CurrentView.text = "<size=40>Collect letters</size>" + CurrentName;
		if (ListLevelTwo[0].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotA = "<color=yellow>W</color>";
			MotOne = true;
		}
		if (ListLevelTwo[1].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotB = "<color=yellow>O</color>";
			MotTwo = true;
		}
		if (ListLevelTwo[2].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotC = "<color=yellow>O</color>";
			MotThree = true;
		}
		if (ListLevelTwo[3].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotD = "<color=yellow>D</color>";
			MotFour = true;
		}
		if (MotOne && MotTwo && MotThree && MotFour && FixedWin)
		{
			Manager.GameWin = true;
			FixedWin = false;
		}
	}

	private void ManagerLevelThree()
	{
		if (CheckText)
		{
			CurrentName = "UNIVERS";
			MotA = "U";
			MotB = "N";
			MotC = "I";
			MotD = "V";
			MotE = "E";
			MotF = "R";
			MotG = "S";
			CheckText = false;
		}
		CurrentName = MotA + MotB + MotC + MotD + MotE + MotF + MotG;
		CurrentView.text = "<size=40>Collect letters</size>" + CurrentName;
		if (ListLevelThree[0].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotA = "<color=yellow>U</color>";
			MotOne = true;
		}
		if (ListLevelThree[1].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotB = "<color=yellow>N</color>";
			MotTwo = true;
		}
		if (ListLevelThree[2].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotC = "<color=yellow>I</color>";
			MotThree = true;
		}
		if (ListLevelThree[3].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotD = "<color=yellow>V</color>";
			MotFour = true;
		}
		if (ListLevelThree[4].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotE = "<color=yellow>E</color>";
			MotFive = true;
		}
		if (ListLevelThree[5].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotF = "<color=yellow>R</color>";
			MotSix = true;
		}
		if (ListLevelThree[6].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotG = "<color=yellow>S</color>";
			MotSeven = true;
		}
		if (MotOne && MotTwo && MotThree && MotFour && MotFive && MotSix && MotSeven && FixedWin)
		{
			Manager.GameWin = true;
			FixedWin = false;
		}
	}

	private void ManagerLevelFour()
	{
		if (CheckText)
		{
			CurrentName = "MOON";
			MotA = "M";
			MotB = "O";
			MotC = "O";
			MotD = "N";
			CheckText = false;
		}
		CurrentName = MotA + MotB + MotC + MotD;
		CurrentView.text = "<size=40>Collect letters</size>" + CurrentName;
		if (ListLevelFour[0].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotA = "<color=yellow>M</color>";
			MotOne = true;
		}
		if (ListLevelFour[1].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotB = "<color=yellow>O</color>";
			MotTwo = true;
		}
		if (ListLevelFour[2].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotC = "<color=yellow>O</color>";
			MotThree = true;
		}
		if (ListLevelFour[3].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotD = "<color=yellow>N</color>";
			MotFour = true;
		}
		if (MotOne && MotTwo && MotThree && MotFour && FixedWin)
		{
			Manager.GameWin = true;
			FixedWin = false;
		}
	}

	private void ManagerLevelFive()
	{
		if (CheckText)
		{
			CurrentName = "ROCK";
			MotA = "R";
			MotB = "O";
			MotC = "C";
			MotD = "K";
			CheckText = false;
		}
		CurrentName = MotA + MotB + MotC + MotD;
		CurrentView.text = "<size=40>Collect letters</size>" + CurrentName;
		if (ListLevelFive[0].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotA = "<color=yellow>R</color>";
			MotOne = true;
		}
		if (ListLevelFive[1].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotB = "<color=yellow>O</color>";
			MotTwo = true;
		}
		if (ListLevelFive[2].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotC = "<color=yellow>C</color>";
			MotThree = true;
		}
		if (ListLevelFive[3].gameObject.GetComponent<LetterManagement>().BoxChecked)
		{
			MotD = "<color=yellow>K</color>";
			MotFour = true;
		}
		if (MotOne && MotTwo && MotThree && MotFour && FixedWin)
		{
			Manager.GameWin = true;
			FixedWin = false;
		}
	}
}
