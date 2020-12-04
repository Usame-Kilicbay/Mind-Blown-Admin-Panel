using Constants;
using EasyMobile;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AprrovePendingQuestionUI : MonoBehaviour
{
	[Header("InputFields")]
	[SerializeField] private TMP_InputField m_QuestionInputField;
	[SerializeField] private TMP_InputField m_CorrectOptionInputField;
	[SerializeField] private TMP_InputField m_WrongOptionInputField1;
	[SerializeField] private TMP_InputField m_WrongOptionInputField2;
	[SerializeField] private TMP_InputField m_WrongOptionInputField3;

	[Header("InputField List")]
	[SerializeField] private List<TMP_InputField> m_InputFields;

	[Header("Dropdowns")]
	[SerializeField] private TMP_Dropdown m_QuestionCategoryDropdown;
	[SerializeField] private TMP_Dropdown m_QuestionLevelDropdown;
	[SerializeField] private TMP_Dropdown m_QuestionLanguageDropdown;

	[Header("Buttons")]
	[SerializeField] private Button m_BackToGetPendingQuestionPanelButton;
	[SerializeField] private Button m_ApprovePendingQuestionButton;

	private void OnEnable()
	{
		LoadQuestion();
	}

	private void Start()
	{
		OnClickAddListener();
	}

	void tryd()
	{
		Debug.Log("Kategori caption text text   " + m_QuestionCategoryDropdown.captionText.text.Replace(" ", string.Empty));
	}

	private void OnClickAddListener()
	{
		m_BackToGetPendingQuestionPanelButton.onClick.AddListener(UIManager.Instance.ShowGetPendingQuestionsPanel);
		m_ApprovePendingQuestionButton.onClick.AddListener(tryd);
	}

	private void LoadQuestion()
	{
		m_QuestionInputField.text = QuestionKeeper.S_QuestionText;
		m_CorrectOptionInputField.text = QuestionKeeper.S_Options.CorrectOption.OptionText;
		m_WrongOptionInputField1.text = QuestionKeeper.S_Options.WrongOption1.OptionText;
		m_WrongOptionInputField2.text = QuestionKeeper.S_Options.WrongOption2.OptionText;
		m_WrongOptionInputField3.text = QuestionKeeper.S_Options.WrongOption3.OptionText;
	}

	private void ApproveQuestion()
	{
		//      if (_questionInputField.text != null && _correctOptionInputField.text != null && _wrongOption3InputField.text != null && _wrongOption3InputField.text != null && _wrongOption3InputField.text != null)
		//      {
		//          Dictionary<string, string> approvedQuestionPack = new Dictionary<string, string>()
		//          {
		//              [QuestionPaths.QuestionDetailPaths.Question] = _questionInputField.text,
		//              [QuestionPaths.QuestionDetailPaths.CorrectOption] = _correctOptionInputField.text,
		//              [QuestionPaths.QuestionDetailPaths.WrongOption1] = _wrongOption1InputField.text,
		//              [QuestionPaths.QuestionDetailPaths.WrongOption2] = _wrongOption2InputField.text,
		//              [QuestionPaths.QuestionDetailPaths.WrongOption3] = _wrongOption3InputField.text,
		//              [QuestionPaths.QuestionDetailPaths.QuestionCategory] = _questionCategoryDropdown.options[_questionCategoryDropdown.value].text,
		//              [QuestionPaths.QuestionDetailPaths.QuestionLevel] = _questionLevelDropdown.options[_questionLevelDropdown.value].text,
		//              [QuestionPaths.QuestionDetailPaths.QuestionLanguage] = _questionLanguageDropdown.options[_questionLanguageDropdown.value].text,
		//              [QuestionPaths.QuestionDetailPaths.SenderPlayerID] = PendingQuestionPackKeeper.SenderPlayerID
		//          };

		//          EventManager.Instance.ApproveQuestion(approvedQuestionPack, ApproveQuestionSuccesful, ApproveQuestionFailed);
		//      }
		//else
		//{
		//          ApproveQuestionFailed();
		//}




		if (IsAnyInputFieldNull())
		{
			NativeUI.ShowToast($"{SendQuestionDebugs.QuestionSendFailed}");

			return;
		}

		Question question = new Question
		{
			QuestionText = m_QuestionInputField.text,

			Options = new Options
			{
				CorrectOption = new Option
				{
					IsCorrectOption = true,
					OptionText = m_CorrectOptionInputField.text
				},

				WrongOption1 = new Option
				{
					IsCorrectOption = false,
					OptionText = m_WrongOptionInputField1.text
				},

				WrongOption2 = new Option
				{
					IsCorrectOption = false,
					OptionText = m_WrongOptionInputField2.text
				},

				WrongOption3 = new Option
				{
					IsCorrectOption = false,
					OptionText = m_WrongOptionInputField3.text
				}

			},

			QuestionCategory = m_QuestionCategoryDropdown.captionText.text.Replace(" ", string.Empty),
			QuestionLanguage = m_QuestionLanguageDropdown.captionText.text.Replace(" ", string.Empty),
			QuestionLevel = int.Parse(m_QuestionLevelDropdown.captionText.text.Replace(" ", string.Empty)),
			SenderPlayerID = FirebaseManager.auth.CurrentUser.UserId
		};

		EventManager.Instance.ApproveQuestion(question, ApproveQuestionSuccesful, ApproveQuestionFailed);

		NativeUI.ShowToast($"{SendQuestionDebugs.QuestionSendSuccessful}");

		//ResetFields();
	}

	private bool IsAnyInputFieldNull()
	{
		foreach (TMP_InputField inputField in m_InputFields)
		{
			if (string.IsNullOrEmpty(inputField.text))
			{
				return true;
			}
		}

		return false;
	}

	private void ApproveQuestionSuccesful()
	{
		NativeUI.ShowToast("Soru eklendi");
	}

	private void ApproveQuestionFailed()
	{
		NativeUI.ShowToast("Soruyu onaylayabilmek için tüm alanların dolu olduğundan emin olun!", true);
	}
}
