using ConstantKeeper;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendingQuestionPackSetter : MonoBehaviour
{
	[SerializeField] private Button _pendingQuestionListButtton;

	Dictionary<string, string> uniquePendingQuestionPackDic;

	private void Start()
	{
		OnClickAddListener();
	}

	private void OnClickAddListener()
	{
		_pendingQuestionListButtton.onClick.AddListener(SetPendingQuestionKeeper);
	}

	public void SetPendingQuestionKeeper(Dictionary<string, string> pendingQuestionPackDict)
	{
		uniquePendingQuestionPackDic = pendingQuestionPackDict;
	}

	private void SetPendingQuestionKeeper()
	{
		PendingQuestionPackKeeper.Question = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.Question];
		PendingQuestionPackKeeper.CorrectOption = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.CorrectOption];
		PendingQuestionPackKeeper.WrongOption1 = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.WrongOption1];
		PendingQuestionPackKeeper.WrongOption2 = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.WrongOption2];
		PendingQuestionPackKeeper.WrongOption3 = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.WrongOption3];
		//PendingQuestionPackKeeper.QuestionCategory = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.QuestionCategory];
		//PendingQuestionPackKeeper.QuestionLevel = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.QuestionLevel];
		PendingQuestionPackKeeper.QuestionID = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.QuestionID];
		PendingQuestionPackKeeper.SenderPlayerID = uniquePendingQuestionPackDic[QuestionPaths.QuestionDetailPaths.SenderPlayerID];

		UIManager.Instance.ShowApprovePendingQuestionPanel();
	}
}
