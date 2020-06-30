using ConstantKeeper;
using EasyMobile;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AprrovePendingQuestionUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _questionInputField;
    [SerializeField] private TMP_InputField _correctOptionInputField;
    [SerializeField] private TMP_InputField _wrongOption1InputField;
    [SerializeField] private TMP_InputField _wrongOption2InputField;
    [SerializeField] private TMP_InputField _wrongOption3InputField;

    [SerializeField] private TMP_Dropdown _questionCategoryDropdown;
    [SerializeField] private TMP_Dropdown _questionLevelDropdown;
    [SerializeField] private TMP_Dropdown _questionLanguageDropdown;

    [SerializeField] private Button _backToGetPendingQuestionPanelButton;
    [SerializeField] private Button _approvePendingQuestionButton;

	private void OnEnable()
	{
        FillFields();
	}

	private void Start()
	{
        OnClickAddListener();
    }

	private void OnClickAddListener() 
    {
        _backToGetPendingQuestionPanelButton.onClick.AddListener(UIManager.Instance.ShowGetPendingQuestionsPanel);
        _approvePendingQuestionButton.onClick.AddListener(ApproveQuestion);
    }

	private void FillFields() 
    {
        _questionInputField.text = PendingQuestionPackKeeper.Question;
        _correctOptionInputField.text = PendingQuestionPackKeeper.CorrectOption;
        _wrongOption1InputField.text = PendingQuestionPackKeeper.WrongOption1;
        _wrongOption2InputField.text = PendingQuestionPackKeeper.WrongOption2;
        _wrongOption3InputField.text = PendingQuestionPackKeeper.WrongOption3;
    }

    private void ApproveQuestion()
    {
        if (_questionInputField.text != null && _correctOptionInputField.text != null && _wrongOption3InputField.text != null && _wrongOption3InputField.text != null && _wrongOption3InputField.text != null)
        {
            Dictionary<string, string> approvedQuestionPack = new Dictionary<string, string>()
            {
                [QuestionPaths.QuestionDetailPaths.Question] = _questionInputField.text,
                [QuestionPaths.QuestionDetailPaths.CorrectOption] = _correctOptionInputField.text,
                [QuestionPaths.QuestionDetailPaths.WrongOption1] = _wrongOption1InputField.text,
                [QuestionPaths.QuestionDetailPaths.WrongOption2] = _wrongOption2InputField.text,
                [QuestionPaths.QuestionDetailPaths.WrongOption3] = _wrongOption3InputField.text,
                [QuestionPaths.QuestionDetailPaths.QuestionCategory] = _questionCategoryDropdown.options[_questionCategoryDropdown.value].text,
                [QuestionPaths.QuestionDetailPaths.QuestionLevel] = _questionLevelDropdown.options[_questionLevelDropdown.value].text,
                [QuestionPaths.QuestionDetailPaths.QuestionLanguage] = _questionLanguageDropdown.options[_questionLanguageDropdown.value].text,
                [QuestionPaths.QuestionDetailPaths.SenderPlayerID] = PendingQuestionPackKeeper.SenderPlayerID
            };

            ActionManager.Instance.ApproveQuestion(approvedQuestionPack, ApproveQuestionSuccesful, ApproveQuestionFailed);
        }
		else
		{
            ApproveQuestionFailed();
		}
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
