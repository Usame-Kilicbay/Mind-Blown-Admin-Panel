using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] Button button_Play;
    [SerializeField] Button button_Settings;
    [SerializeField] Button button_UserProfile;
    [SerializeField] Button button_SendQuestion;
    [SerializeField] Button button_ApprovePendingQuestion;


    private void OnEnable()
    {
        OnClickAddListener();
      //  LoadData();
    }

    private void OnClickAddListener() 
    {
        button_Play.onClick.AddListener(UIManager.Instance.ShowCategoriesPanel);
        button_Settings.onClick.AddListener(UIManager.Instance.ShowSettingsPanel);
        button_UserProfile.onClick.AddListener(UserProfile);
        button_ApprovePendingQuestion.onClick.AddListener(UIManager.Instance.ShowApprovePendingQuestionPanel);
    }

    //private void LoadData() 
    //{
        //txt_Papcoin.SetText(CurrentUserProfileKeeper.Papcoin.ToString());
        //txt_Gem.SetText(CurrentUserProfileKeeper.Gem.ToString());
    //}


    private void RateUs() { }

    private void GetRank() { }

    private void UserProfile() 
    {
        BottomNavigationBarManager.Instance.ShowUserNavigation();
    }
}
