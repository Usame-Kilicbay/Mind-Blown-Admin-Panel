using Constants;
using DG.Tweening;
using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignInUI : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color color_Green;
    [SerializeField] private Color color_Red;

    [Header("InputField")]
    [SerializeField] private TMP_InputField inputField_Email;
    [SerializeField] private TMP_InputField inputField_Password;
    
    [Header("Button")]
    [SerializeField] private Button btn_GoToMainMenu;
    [SerializeField] private Button btn_SignIn;
    [SerializeField] private Button btn_ResetPassword;
    [SerializeField] private Button btn_GoToSignUp;

    [Header("RectTransform")]
    [SerializeField] private RectTransform panel_Parent;

    private void Start()
    {
        OnClickAddListener();
    }

    private void OnClickAddListener()
    {
        btn_GoToMainMenu.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
        btn_SignIn.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        string email = inputField_Email.textComponent.text.Replace("\u200B", "");
        string password = inputField_Password.textComponent.text.Replace("\u200B", "");

        StartCoroutine(EventManager.Instance.SignInWithEmailPassword(email, password, SignInEmailPasswordSuccessful, SignInWithEmailPasswordFailed));
    }

    private void SignInEmailPasswordSuccessful()
    {
		//NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignInPaths.SignInSuccessful, AuthenticationsDebugs.SignInPaths.SignInSuccessfulDetails);
		NativeUI.ShowToast($"{AuthenticationsDebugs.SignInPaths.SignInSuccessful} \n {AuthenticationsDebugs.SignInPaths.SignInSuccessfulDetails}");
	}

    private void SignInWithEmailPasswordFailed()
    {
        NativeUI.ShowToast($"{AuthenticationsDebugs.SignInPaths.SignInSuccessful} \n {AuthenticationsDebugs.SignInPaths.SignInSuccessfulDetails}");
    }
}
