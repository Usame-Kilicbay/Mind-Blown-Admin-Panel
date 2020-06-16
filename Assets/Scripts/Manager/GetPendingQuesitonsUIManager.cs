using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct PendingQuestionStruct 
{
	public string Question;
	public string CorrectOption;
	public string WrongOption1;
	public string WrongOption2;
	public string WrongOption3;
	public string SenderPlayerID;
}

public class GetPendingQuesitonsUIManager : MonoBehaviour
{
	[Header("Button")]
	[SerializeField] private Button _goToMainMenuButton;
	[SerializeField] private Button _refreshListButton;

	[SerializeField] private GameObject _pendingQuestionParent;
	[SerializeField] private RectTransform _pendingQuestionParentRectTransform;

	[SerializeField] private Button _pendingQuestionListButtonPrefab;
    private TextMeshProUGUI _pendingQuestionText;

	//[SerializeField] private List<Button> _pendingQuestions;
	 private List<Button> _pendingQuestions;

	private void Start()
	{
		Subscribe();
		OnClickAddListener();
		_pendingQuestions = new List<Button>();
	}

	private void Subscribe()
	{
		ActionManager.Instance.CreatePendingQuestionList += CreatePendingQuestionList;
	}

	private void OnClickAddListener()
	{
		_goToMainMenuButton.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
		_refreshListButton.onClick.AddListener(() => StartCoroutine(ActionManager.Instance.GetPendingQuestions()));
	}

	private void CreatePendingQuestionList(List<string> pendingQuestionList) 
	{
		if (_pendingQuestions.Count > 0)
		{
			foreach (Button button in _pendingQuestions)
			{
				Debug.Log(_pendingQuestions.Count);
				Destroy(button.gameObject);
			}

			_pendingQuestions.Clear();
		}
		
		int questionAmount = 0;

		foreach (string questionPack in pendingQuestionList)
		{
			PendingQuestionStruct pendingQuestionStruct = JsonUtility.FromJson<PendingQuestionStruct>(questionPack);

		 	Button newButton = Instantiate(_pendingQuestionListButtonPrefab, _pendingQuestionParent.transform);
			_pendingQuestions.Add(newButton);

			_pendingQuestionText = newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			_pendingQuestionText.SetText(pendingQuestionStruct.Question);
			
			Debug.Log(pendingQuestionStruct.WrongOption1);

			questionAmount++;
		}

		_pendingQuestionParentRectTransform.sizeDelta = new Vector2(0, questionAmount * 150 + 100);
	}
}
