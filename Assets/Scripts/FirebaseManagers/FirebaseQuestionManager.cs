using Constants;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseQuestionManager : MonoBehaviour
{
	public static List<string> categories = new List<string>();

	private void OnEnable()
	{
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	private void Subscribe()
	{
		EventManager.Instance.SendQuestion += SendQuestion;
		EventManager.Instance.GetPendingQuestions += GetPendingQuestions;
		EventManager.Instance.ApproveQuestion += ApproveQuestion;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.SendQuestion -= SendQuestion;
		EventManager.Instance.GetPendingQuestions -= GetPendingQuestions;
		EventManager.Instance.ApproveQuestion -= ApproveQuestion;
	}

	private void SendQuestion(Dictionary<string, object> sendedQuestionPack)
	{
		string questionID = FirebaseManager.PendingQuestionsDatabaseReference.Push().Key;

		FirebaseManager.PendingQuestionsDatabaseReference.Child(questionID).SetValueAsync(sendedQuestionPack);
	}

	private IEnumerator GetPendingQuestions()
	{
		Task<DataSnapshot> task = FirebaseManager.PendingQuestionsDatabaseReference.GetValueAsync();

		yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

		if (task.IsCanceled)
		{
			Debug.LogWarning(GetDataTaskDebugs.GetData + DebugPaths.IsCanceled);
		}
		else if (task.IsFaulted)
		{
			Debug.LogError(GetDataTaskDebugs.GetData + DebugPaths.IsFaulted);
		}
		else if (task.IsCompleted)
		{
			DataSnapshot snapshot = task.Result;

			List<Question> pendingQuestions = new List<Question>();

			foreach (DataSnapshot questionPack in snapshot.Children)
			{
				Question question = new Question();

				question = JsonUtility.FromJson<Question>(questionPack.GetRawJsonValue());

				question.QuestionID = questionPack.Key;

				Debug.Log(question.SenderPlayerID);

				pendingQuestions.Add(question);
			}

			EventManager.Instance.CreatePendingQuestionList(pendingQuestions);
		}
	}

	private void ApproveQuestion(Question question, Action onSuccessCallback, Action onFailCallback)
	{
		string questionID = FirebaseManager.PendingQuestionsDatabaseReference.Push().Key;

		FirebaseManager.PublishedQuestionsDatabaseReference.Child(questionID).SetValueAsync(question);

		onSuccessCallback();

		DeleteQuestion(FirebaseManager.PendingQuestionsDatabaseReference, question.QuestionID);
	}

	private void DeleteQuestion(DatabaseReference databaseReference, string questionID) 
	{
		databaseReference.Child(questionID).RemoveValueAsync();
	}
}
