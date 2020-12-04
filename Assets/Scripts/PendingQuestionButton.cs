using Constants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendingQuestionButton : MonoBehaviour
{
	[SerializeField] private Button _pendingQuestionListButtton;

	private Question m_Question;

	private void Start()
	{
		OnClickAddListener();
	}

	private void OnClickAddListener()
	{
		_pendingQuestionListButtton.onClick.AddListener(SetPendingQuestionKeeper);
	}

	public void Init(Question question)
	{
		m_Question = question;
	}

	private void SetPendingQuestionKeeper()
	{
		QuestionKeeper.S_QuestionText = m_Question.QuestionText;
		QuestionKeeper.S_Options.CorrectOption = m_Question.Options.CorrectOption;
		QuestionKeeper.S_Options.WrongOption1 = m_Question.Options.WrongOption1;
		QuestionKeeper.S_Options.WrongOption2 = m_Question.Options.WrongOption2;
		QuestionKeeper.S_Options.WrongOption3 = m_Question.Options.WrongOption3;
		QuestionKeeper.S_QuestionCategory = m_Question.QuestionCategory;
		QuestionKeeper.S_QuestionLevel = m_Question.QuestionLevel;
		QuestionKeeper.S_QuestionLanguage = m_Question.QuestionLanguage;
		QuestionKeeper.S_QuestionID = m_Question.QuestionID;
		QuestionKeeper.S_SenderPlayerID = m_Question.SenderPlayerID;

		UIManager.Instance.ShowApprovePendingQuestionPanel();
	}
}
