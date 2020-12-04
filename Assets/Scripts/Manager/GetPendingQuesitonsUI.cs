using Constants;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetPendingQuesitonsUI : MonoBehaviour
{
	[Header("Buttons")]
	[SerializeField] private Button _goToMainMenuButton;
	[SerializeField] private Button _refreshListButton;

	[Header("Parents")]
	[SerializeField] private GameObject _pendingQuestionParent;
	
	[Header("RectTransforms")]
	[SerializeField] private RectTransform _pendingQuestionParentRectTransform;

	[Header("Prefabs")]
	[SerializeField] private Button _pendingQuestionListButtonPrefab;
	
	private TextMeshProUGUI _pendingQuestionText;

	private List<Button> _pendingQuestions;

	private void OnEnable()
	{
		Subscribe();
		StartCoroutine(EventManager.Instance.GetPendingQuestions?.Invoke());
	}

	private void Start()
	{
		OnClickAddListener();
		_pendingQuestions = new List<Button>();
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);	
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.CreatePendingQuestionList += CreatePendingQuestionList;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.CreatePendingQuestionList -= CreatePendingQuestionList;
	}

	#endregion

	private void OnClickAddListener()
	{
		_goToMainMenuButton.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
		_refreshListButton.onClick.AddListener(() => StartCoroutine(EventManager.Instance.GetPendingQuestions()));
	}

	private void CreatePendingQuestionList(List<Question> pendingQuestionList)
	{
		if (_pendingQuestions.Count > 0)
		{
			foreach (Button button in _pendingQuestions)
			{
				Destroy(button.gameObject);
			}

			_pendingQuestions.Clear();
		}

		int questionAmount = 0;

		foreach (Question question in pendingQuestionList)
		{
			Button newButton = Instantiate(_pendingQuestionListButtonPrefab, _pendingQuestionParent.transform);
			_pendingQuestions.Add(newButton);

			newButton.GetComponent<PendingQuestionButton>().Init(question);

			_pendingQuestionText = newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			_pendingQuestionText.SetText(question.QuestionText);

			questionAmount++;
		}

		_pendingQuestionParentRectTransform.sizeDelta = new Vector2(0, questionAmount * 150f + 100f);
	}
}
