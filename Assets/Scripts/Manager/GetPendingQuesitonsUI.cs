using ConstantKeeper;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct PendingQuestionPackStruct 
{
	public string Question;
	public string CorrectOption;
	public string WrongOption1;
	public string WrongOption2;
	public string WrongOption3;
	public string SenderPlayerID;
	public string QuestionID;
}

public static class PendingQuestionPackKeeper
{
	public static string Question;
	public static string CorrectOption;
	public static string WrongOption1;
	public static string WrongOption2;
	public static string WrongOption3;
	public static string QuestionCategory;
	public static string QuestionLevel;
	public static string QuestionID;
	public static string SenderPlayerID;
}


public class GetPendingQuesitonsUI : MonoBehaviour
{
	[Header("Button")]
	[SerializeField] private Button _goToMainMenuButton;
	[SerializeField] private Button _refreshListButton;

	[SerializeField] private GameObject _pendingQuestionParent;
	[SerializeField] private RectTransform _pendingQuestionParentRectTransform;

	[SerializeField] private Button _pendingQuestionListButtonPrefab;
	private TextMeshProUGUI _pendingQuestionText;
	private PendingQuestionPackSetter _pendingQuestionPackSetter;

	private List<Button> _pendingQuestions;

	private void OnEnable()
	{
		Subscribe();
		StartCoroutine(ActionManager.Instance.GetPendingQuestions?.Invoke());
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

	private void Subscribe()
	{
		ActionManager.Instance.CreatePendingQuestionList += CreatePendingQuestionList;
	}

	private void Unsubscribe()
	{
		ActionManager.Instance.CreatePendingQuestionList -= CreatePendingQuestionList;
	}

	private void OnClickAddListener()
	{
		_goToMainMenuButton.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
		_refreshListButton.onClick.AddListener(() => StartCoroutine(ActionManager.Instance.GetPendingQuestions()));
	}

	private void CreatePendingQuestionList(List<Dictionary<string, string>> pendingQuestionPackDictList)
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

		foreach (Dictionary<string, string> questionPackDict in pendingQuestionPackDictList)
		{
			Button newButton = Instantiate(_pendingQuestionListButtonPrefab, _pendingQuestionParent.transform);
			_pendingQuestions.Add(newButton);

			_pendingQuestionText = newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			_pendingQuestionText.SetText(questionPackDict[QuestionPaths.QuestionDetailPaths.Question]);

			_pendingQuestionPackSetter = newButton.GetComponent<PendingQuestionPackSetter>();
			_pendingQuestionPackSetter.SetPendingQuestionKeeper(questionPackDict);

			questionAmount++;
		}

		_pendingQuestionParentRectTransform.sizeDelta = new Vector2(0, questionAmount * 150 + 100);
	}
}
