using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public class Axis
	{
		private string name;

		public Axis(string name)
		{
			this.name = name;
			Instance().AddAxis(name);
		}

		public float Get()
		{
			return Instance().GetAxis(name);
		}

		public void Set(float val)
		{
			Instance().SetAxis(name, val);
		}

		public void Update(float delta)
		{
			float v = Instance().GetAxis(name) + delta;
			Instance().SetAxis(name, v);
		}
	}

	public class Button
	{
		private string name;

		public Button(string name)
		{
			this.name = name;
			Instance().AddButton(name);
		}

		public bool Get()
		{
			return Instance().GetButton(name);
		}

		public void Set(bool val)
		{
			Instance().SetButton(name, val);
		}
	}

	private static InputManager instance;

	public List<string> axes;

	public List<float> values = new List<float>();

	public List<string> buttons;

	public List<bool> buttonsStates = new List<bool>();

	public List<bool> buttonsDown = new List<bool>();

	private List<bool> buttonsUp = new List<bool>();

	private Dictionary<string, bool> toSet = new Dictionary<string, bool>();

	public static InputManager Instance()
	{
		if (instance == null)
		{
			instance = Object.FindObjectOfType<InputManager>();
		}
		if (instance == null)
		{
			GameObject gameObject = new GameObject("InputManager");
			instance = gameObject.AddComponent<InputManager>();
			instance.axes = new List<string>();
			instance.buttons = new List<string>();
		}
		return instance;
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Object.Destroy(this);
		}
	}

	private void Start()
	{
		for (int i = 0; i < axes.Count; i++)
		{
			values.Add(0f);
		}
		for (int j = 0; j < buttons.Count; j++)
		{
			buttonsStates.Add(false);
			buttonsDown.Add(false);
			buttonsUp.Add(false);
		}
	}

	private void LateUpdate()
	{
		for (int i = 0; i < buttonsUp.Count; i++)
		{
			buttonsUp[i] = false;
		}
		for (int j = 0; j < buttonsDown.Count; j++)
		{
			buttonsDown[j] = false;
		}
		foreach (KeyValuePair<string, bool> item in toSet)
		{
			LateSetButton(item.Key, item.Value);
		}
		toSet.Clear();
	}

	public float GetAxis(string name)
	{
		if (!axes.Contains(name))
		{
			return 0f;
		}
		int i;
		for (i = 0; i < axes.Count && !(axes[i] == name); i++)
		{
		}
		if (i >= values.Count)
		{
			for (int j = values.Count; j < axes.Count; j++)
			{
				values.Add(0f);
			}
		}
		return values[i];
	}

	public float GetAxis(int id)
	{
		if (id < 0 || id >= axes.Count)
		{
			return 0f;
		}
		if (id >= values.Count)
		{
			for (int i = values.Count; i < axes.Count; i++)
			{
				values.Add(0f);
			}
		}
		return values[id];
	}

	public int GetAxisId(string name)
	{
		return axes.IndexOf(name);
	}

	public void SetAxis(string name, float v)
	{
		if (!axes.Contains(name))
		{
			return;
		}
		int i;
		for (i = 0; i < axes.Count && !(axes[i] == name); i++)
		{
		}
		if (i >= values.Count)
		{
			for (int j = values.Count; j < axes.Count; j++)
			{
				values.Add(0f);
			}
		}
		values[i] = v;
	}

	public void AddAxis(string name)
	{
		if (!axes.Contains(name))
		{
			axes.Add(name);
			values.Add(0f);
		}
	}

	public bool GetButton(string name)
	{
		if (!buttons.Contains(name))
		{
			return false;
		}
		int i;
		for (i = 0; i < buttons.Count && !(buttons[i] == name); i++)
		{
		}
		if (i >= buttonsStates.Count)
		{
			for (int j = buttonsStates.Count; j < buttons.Count; j++)
			{
				buttonsStates.Add(false);
				buttonsDown.Add(false);
				buttonsUp.Add(false);
			}
		}
		return buttonsStates[i];
	}

	public bool GetButtonDown(string name)
	{
		if (!buttons.Contains(name))
		{
			return false;
		}
		int i;
		for (i = 0; i < buttons.Count && !(buttons[i] == name); i++)
		{
		}
		if (i >= buttonsStates.Count)
		{
			for (int j = buttonsStates.Count; j < buttons.Count; j++)
			{
				buttonsStates.Add(false);
				buttonsDown.Add(false);
				buttonsUp.Add(false);
			}
		}
		return buttonsDown[i];
	}

	public bool GetButtonUp(string name)
	{
		if (!buttons.Contains(name))
		{
			return false;
		}
		int i;
		for (i = 0; i < buttons.Count && !(buttons[i] == name); i++)
		{
		}
		if (i >= buttonsStates.Count)
		{
			for (int j = buttonsStates.Count; j < buttons.Count; j++)
			{
				buttonsStates.Add(false);
				buttonsDown.Add(false);
				buttonsUp.Add(false);
			}
		}
		return buttonsUp[i];
	}

	public void SetButton(string name, bool v)
	{
		if (buttons.Contains(name))
		{
			if (!toSet.ContainsKey(name))
			{
				toSet.Add(name, false);
			}
			toSet[name] = toSet[name] || v;
		}
	}

	private void LateSetButton(string name, bool v)
	{
		if (!buttons.Contains(name))
		{
			return;
		}
		int i;
		for (i = 0; i < buttons.Count && !(buttons[i] == name); i++)
		{
		}
		if (i >= buttonsStates.Count)
		{
			for (int j = buttonsStates.Count; j < buttons.Count; j++)
			{
				buttonsStates.Add(false);
				buttonsDown.Add(false);
				buttonsUp.Add(false);
			}
		}
		if (buttonsStates[i] && !v)
		{
			buttonsUp[i] = true;
		}
		if (!buttonsStates[i] && v)
		{
			buttonsDown[i] = true;
		}
		buttonsStates[i] = v;
	}

	public void AddButton(string name)
	{
		if (!buttons.Contains(name))
		{
			buttons.Add(name);
			buttonsStates.Add(false);
			buttonsDown.Add(false);
			buttonsUp.Add(false);
		}
	}
}
