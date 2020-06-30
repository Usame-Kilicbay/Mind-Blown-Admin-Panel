using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public enum Panels
{
	MainMenu,
	Categories,
	Lobby,
	CreateRoom,
	Game,
	Settings,
	Store,
	SignIn,
	SignUp,
	ResetPassword,
	UserProfile,
	Lose,
	Win,
	SendQuestion,
	AprrovePendingQuestions,
	GetPendingQuestions
}

public class UIManager : Singleton<UIManager>
{
	[Header("Panel")]
	[SerializeField] private GameObject panel_PanelParent;
	[SerializeField] private GameObject panel_MainMenu;
	[SerializeField] private GameObject panel_Categories;
	[SerializeField] private GameObject panel_Settings;
	[SerializeField] private GameObject panel_SignIn;
	[SerializeField] private GameObject panel_UserProfile;
	[SerializeField] private GameObject panel_GetPendingQuestions;
	[SerializeField] private GameObject panel_ApprovePendingQuestions;

	[Header("NavBar")]
	[SerializeField] private GameObject _bottomNavigationBar;

	[Header("LoadingPanel")]
	[SerializeField] private GameObject _loadingPanel;

	//[Header("RectTransform")]
	//private RectTransform rectTransform_Parent;
	//private RectTransform rectTransform_MainMenu;
	//private RectTransform rectTransform_Categories;
	//private RectTransform rectTransform_Game;
	//private RectTransform rectTransform_Settings;
	//private RectTransform rectTransform_SignIn;
	//private RectTransform rectTransform_SignUp;
	//private RectTransform rectTransform_ResetPassword;
	//private RectTransform rectTransform_UserProfile;
	//private RectTransform rectTransform_SendQuestion;
	//private RectTransform rectTransform_ApprovePendingQuestions;

	[SerializeField] private List<GameObject> panelList;

	private void OnEnable()
	{
		ActionManager.Instance.ShowSignInPanel += ShowSignInPanel;
		ActionManager.Instance.ShowUserProfilePanel += ShowUserProfilePanel;
	}

	//private void OnDisable()
	//{
	//	ActionManager.Instance.ShowSignInPanel -= ShowSignInPanel;
	//	ActionManager.Instance.ShowSignUpPanel -= ShowSignUpPanel;
	//	ActionManager.Instance.ShowUserProfilePanel -= ShowUserProfilePanel;
	//}

	//private void OnApplicationQuit()
	//{
	//	ActionManager.Instance.ShowSignInPanel -= ShowSignInPanel;
	//	ActionManager.Instance.ShowSignUpPanel -= ShowSignUpPanel;
	//	ActionManager.Instance.ShowUserProfilePanel -= ShowUserProfilePanel;
	//}

	//private void Start()
	//{
	//	RectTransformSetter();
	//}

	//private void RectTransformSetter()
	//{
	//	rectTransform_Parent = panel_PanelParent.GetComponent<RectTransform>();
	//	rectTransform_MainMenu = panel_MainMenu.GetComponent<RectTransform>();
	//	rectTransform_Categories = panel_Categories.GetComponent<RectTransform>();
	//	rectTransform_Game = panel_Game.GetComponent<RectTransform>();
	//	rectTransform_Settings = panel_Settings.GetComponent<RectTransform>();
	//	rectTransform_SignIn = panel_SignIn.GetComponent<RectTransform>();
	//	rectTransform_SignUp = panel_SignUp.GetComponent<RectTransform>();
	//	rectTransform_ResetPassword = panel_ResetPassword.GetComponent<RectTransform>();
	//	rectTransform_UserProfile = panel_UserProfile.GetComponent<RectTransform>();
	//	rectTransform_SendQuestion = panel_SendQuestion.GetComponent<RectTransform>();
	//	rectTransform_ApprovePendingQuestions = panel_ApprovePendingQuestions.GetComponent<RectTransform>();
	//}

	public void ShowMainMenuPanel() { StartCoroutine(PanelChanger(Panels.MainMenu)); }
	public void ShowCategoriesPanel() { StartCoroutine(PanelChanger(Panels.Categories)); }
	public void ShowSettingsPanel() { StartCoroutine(PanelChanger(Panels.Settings)); }
	public void ShowSignInPanel() { StartCoroutine(PanelChanger(Panels.SignIn)); }
	public void ShowUserProfilePanel() { StartCoroutine(PanelChanger(Panels.UserProfile)); }
	public void ShowGetPendingQuestionsPanel() { StartCoroutine(PanelChanger(Panels.GetPendingQuestions)); }
	public void ShowApprovePendingQuestionPanel() { StartCoroutine(PanelChanger(Panels.AprrovePendingQuestions)); }

	private IEnumerator PanelChanger(Panels panel)
	{
		PanelOpener();

		//RectTransform tempRectTransform = new RectTransform();

		//switch (panels)
		//{
		//	case Panels.MainMenu:
		//		tempRectTransform = rectTransform_MainMenu;
		//		break;
		//	case Panels.Categories:
		//		tempRectTransform = rectTransform_Categories;
		//		break;
		//	case Panels.Lobby:
		//		break;
		//	case Panels.CreateRoom:
		//		break;
		//	case Panels.Game:
		//		tempRectTransform = rectTransform_Game;
		//		break;
		//	case Panels.Settings:
		//		tempRectTransform = rectTransform_Settings;
		//		break;
		//	case Panels.Store:
		//		break;
		//	case Panels.SignIn:
		//		tempRectTransform = rectTransform_SignIn;
		//		break;
		//	case Panels.SignUp:
		//		tempRectTransform = rectTransform_SignUp;
		//		break;
		//	case Panels.ResetPassword:
		//		tempRectTransform = rectTransform_ResetPassword;
		//		break;
		//	case Panels.UserProfile:
		//		tempRectTransform = rectTransform_UserProfile;
		//		break;
		//	case Panels.Lose:
		//		break;
		//	case Panels.Win:
		//		break;
		//	case Panels.SendQuestion:
		//		tempRectTransform = rectTransform_SendQuestion;
		//		break;
		//	case Panels.AprrovePendingQuestions:
		//		tempRectTransform = rectTransform_ApprovePendingQuestions;
		//		break;
		//	default:
		//		tempRectTransform = new RectTransform();
		//		break;
		//}

		yield return new WaitForSeconds(0f);

		/*Sequence panelSequence = DOTween.Sequence();

		panelSequence.Append(rectTransform_Parent.DOAnchorPosX(-tempRectTransform.anchoredPosition.x, 0.3f))
					 .Append(rectTransform_Parent.DOAnchorPosY(-tempRectTransform.anchoredPosition.y, 0.3f));

	
		yield return panelSequence.WaitForCompletion();*/

		panel_MainMenu.SetActive(panel == Panels.MainMenu);
		panel_Categories.SetActive(panel == Panels.Categories);
		panel_Settings.SetActive(panel == Panels.Settings);
		panel_SignIn.SetActive(panel == Panels.SignIn);
		panel_UserProfile.SetActive(panel == Panels.UserProfile);
		panel_GetPendingQuestions.SetActive(panel == Panels.GetPendingQuestions);
		panel_ApprovePendingQuestions.SetActive(panel == Panels.AprrovePendingQuestions);


		if (panel == Panels.Categories || panel == Panels.Game || panel == Panels.SignIn || panel == Panels.GetPendingQuestions || panel == Panels.AprrovePendingQuestions)
		{
			_bottomNavigationBar.SetActive(false);
		}
		else
		{
			_bottomNavigationBar.SetActive(true);
		}
	}

	private void PanelOpener()
	{
		//mainCanvas.SetActive(true);

		//foreach (GameObject panel in panelList)
		//{
		//	panel.SetActive(true);
		//}
	}
}