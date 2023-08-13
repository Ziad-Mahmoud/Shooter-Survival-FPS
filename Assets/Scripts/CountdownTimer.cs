using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	[Header("Manager Controller")]
	public UiController Controller;

	public Text timerText;

	public float countdownTimeSeconds = 300f;

	public string timeFormat = "HH:mm";

	private float remainingTime;

	private bool isRunning;

	private int remainingSeconds;

	private int remainingMinutes;

	private int remainingHours;

	private void Start()
	{
		if (PlayerPrefs.GetString("LockedBtn") == "Done")
		{
			StartTimer();
			remainingSeconds = PlayerPrefs.GetInt("SecondsLeft" + base.name);
			remainingMinutes = PlayerPrefs.GetInt("MinutLeft" + base.name);
			remainingHours = PlayerPrefs.GetInt("HoursLeft" + base.name);
			Controller.CheckedBtn = false;
		}
		else
		{
			Controller.CheckedBtn = true;
		}
	}

	private void Update()
	{
		if (isRunning)
		{
			remainingTime -= Time.deltaTime;
			if (remainingTime <= 0f)
			{
				PlayerPrefs.SetString("LockedBtn", "");
				Controller.CheckedBtn = true;
				remainingTime = 0f;
				StopTimer();
			}
			UpdateTimerText();
		}
	}

	public void StartTimer()
	{
		PlayerPrefs.SetString("LockedBtn", "Done");
		remainingTime = countdownTimeSeconds;
		isRunning = true;
		UpdateTimerText();
		Controller.CheckedBtn = false;
	}

	public void StopTimer()
	{
		isRunning = false;
	}

	private void UpdateTimerText()
	{
		PlayerPrefs.SetInt("MinutLeft" + base.name, remainingMinutes);
		PlayerPrefs.SetInt("SecondsLeft" + base.name, remainingSeconds);
		PlayerPrefs.SetInt("HoursLeft" + base.name, remainingHours);
		remainingSeconds = (int)remainingTime;
		remainingMinutes = remainingSeconds / 60;
		remainingHours = remainingMinutes / 60;
		remainingMinutes %= 60;
		string text = $"{remainingHours:00}h :{remainingMinutes:00}m";
		timerText.text = text;
	}
}
