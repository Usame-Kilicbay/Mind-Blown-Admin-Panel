using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullToRefresh : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	[SerializeField] private float deviceHeight;
	[SerializeField] private float deviceWidht;

	[SerializeField] private float dragLine;
	[SerializeField] private Vector2 dragLineBegin;
	[SerializeField] private Vector2 dragLineEnd;

	bool isLineEnoughSize = false;

	void Start()
	{
		deviceHeight = Screen.height;
		deviceWidht = Screen.width;
		Debug.LogError(deviceHeight / 50);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isLineEnoughSize = false;
		dragLine = 0;
		dragLineBegin = eventData.position;
	}


	public void OnEndDrag(PointerEventData eventData)
	{
		if (isLineEnoughSize)
		{
			Debug.Log("Burada metod çağrılacak");
			StartCoroutine(EventManager.Instance.GetPendingQuestions());
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		dragLine = Vector2.Distance(dragLineEnd, dragLineBegin);

		if (dragLine >= deviceHeight / 20f)
		{
			Debug.Log("Yeterince uzun bırakabilirsin");
			isLineEnoughSize = true;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log(eventData.position);
	}
}
