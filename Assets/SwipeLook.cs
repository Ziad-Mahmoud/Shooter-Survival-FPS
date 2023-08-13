using UnityEngine;
using UnityEngine.UI;

public class SwipeLook : MonoBehaviour
{
	public enum AxisOption
	{
		Both = 0,
		OnlyHorizontal = 1,
		OnlyVertical = 2
	}

	private float minDelta = 1f;

	public RectTransform zone;

	private bool waitingTouches = true;

	public Text lblInfo;

	private Vector2 startPoint;

	private Touch touch;

	private Vector2 prevPos;

	public float sens = 1f;

	public AxisOption axesToUse;

	public bool invertX;

	public bool invertY;

	public string horizontalAxisName = "Horizontal";

	public string verticalAxisName = "Vertical";

	private bool m_UseX;

	private bool m_UseY;

	private InputManager.Axis m_HorizontalVirtualAxis;

	private InputManager.Axis m_VerticalVirtualAxis;

	private void Start()
	{
		minDelta = (float)(Screen.width + Screen.height) / 2f;
		minDelta /= 20f;
		minDelta *= minDelta;
	}

	private bool IsPointInZone(Vector2 point)
	{
		Rect rect = zone.rect;
		float num = zone.position.x - rect.width * zone.lossyScale.x * zone.pivot.x;
		float num2 = zone.position.x + rect.width * zone.lossyScale.x * (1f - zone.pivot.x);
		float num3 = zone.position.y - rect.height * zone.lossyScale.y * zone.pivot.y;
		float num4 = zone.position.y + rect.height * zone.lossyScale.y * (1f - zone.pivot.y);
		return point.x > num && point.x < num2 && point.y > num3 && point.y < num4;
	}

	private void Update()
	{
		if (Input.touches.Length > 0)
		{
			if (waitingTouches)
			{
				if (lblInfo != null)
				{
					lblInfo.text = "Waiting touches " + Input.touches.Length;
				}
				for (int i = 0; i < Input.touches.Length; i++)
				{
					if (IsPointInZone(Input.touches[i].position))
					{
						touch = Input.touches[i];
						PointerDown(touch.position);
						if (lblInfo != null)
						{
							lblInfo.text = string.Concat("Found touch ", Input.touches[i].position, " ", Input.touches[i].fingerId, " ", touch.position, " ", touch.fingerId);
						}
					}
				}
				return;
			}
			bool flag = false;
			for (int j = 0; j < Input.touches.Length; j++)
			{
				if (Input.touches[j].fingerId == touch.fingerId)
				{
					flag = true;
					touch = Input.touches[j];
					OnDrag(Input.touches[j].position - prevPos);
					prevPos = Input.touches[j].position;
					break;
				}
			}
			if (!flag)
			{
				PointerUp(touch.position);
			}
			else if (lblInfo != null)
			{
				lblInfo.text = "Found " + touch.position;
			}
		}
		else
		{
			if (lblInfo != null)
			{
				lblInfo.text = "0 touches";
			}
			waitingTouches = true;
			OnDrag(Vector2.zero);
		}
	}

	private void PointerDown(Vector2 coord)
	{
		startPoint = coord;
		prevPos = startPoint;
		waitingTouches = false;
	}

	private void PointerUp(Vector2 coord)
	{
		UpdateVirtualAxes(Vector2.zero);
		waitingTouches = true;
	}

	private void OnEnable()
	{
		CreateVirtualAxes();
	}

	private void UpdateVirtualAxes(Vector2 delta)
	{
		if (m_UseX)
		{
			if (invertX)
			{
				m_HorizontalVirtualAxis.Set(sens * delta.x);
			}
			else
			{
				m_HorizontalVirtualAxis.Set((0f - sens) * delta.x);
			}
		}
		if (m_UseY)
		{
			if (invertY)
			{
				m_VerticalVirtualAxis.Set((0f - sens) * delta.y);
			}
			else
			{
				m_VerticalVirtualAxis.Set(sens * delta.y);
			}
		}
	}

	private void CreateVirtualAxes()
	{
		m_UseX = axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal;
		m_UseY = axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical;
		if (m_UseX)
		{
			m_HorizontalVirtualAxis = new InputManager.Axis(horizontalAxisName);
		}
		if (m_UseY)
		{
			m_VerticalVirtualAxis = new InputManager.Axis(verticalAxisName);
		}
	}

	public void OnDrag(Vector2 delta)
	{
		UpdateVirtualAxes(delta);
	}

	private void OnDisable()
	{
		if (m_UseX)
		{
		}
		if (!m_UseY)
		{
		}
	}

	public void ToogleInvertX(bool b)
	{
		invertX = !invertX;
	}

	public void ToogleInvertY(bool b)
	{
		invertY = !invertY;
	}
}
