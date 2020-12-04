using Constants;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class FirebaseUserManager : MonoBehaviour
{
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
		EventManager.Instance.CreatUserProfile += CreateUserProfile;
		EventManager.Instance.UpdateUserData += UpdateUserData;
		EventManager.Instance.GetCurrentUserProfile += GetCurrentUserProfile;
		EventManager.Instance.DeleteUserProfile += DeleteUserProfile;

		EventManager.Instance.ControlIsUsernameExist += ControlIsUsernameExist;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.CreatUserProfile -= CreateUserProfile;
		EventManager.Instance.UpdateUserData -= UpdateUserData;
		EventManager.Instance.GetCurrentUserProfile -= GetCurrentUserProfile;
		EventManager.Instance.DeleteUserProfile -= DeleteUserProfile;

		EventManager.Instance.ControlIsUsernameExist -= ControlIsUsernameExist;
	}

	private IEnumerator ControlIsUsernameExist(string _Username, Action onSuccessCallback, Action onFailCallback) 
	{
		Task<DataSnapshot> task = FirebaseManager.UserNullDatabaseReference.GetValueAsync();

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
			
			if (snapshot.ChildrenCount <= 0)
			{
				onSuccessCallback();
			}
			else
			{
				foreach (DataSnapshot userID in snapshot.Children)
				{
					string username = userID.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Username).Value.ToString();

					if (username.Equals(_Username))
					{
						onFailCallback();
						break;
					}
					onSuccessCallback();
				}
			}
		}
	}

	/*	private void CreateUserProfile(string username, string language)
		{
			// General
			UserGeneral userGenerals = new UserGeneral
				(
				username,
				DateTime.Now.ToString("dd/MM/yyyy"),
				DateTime.Now.ToString("dd/MM/yyyy"),
				"Türkiye",
				language,
				true,
				true
				);

			string generalJson = JsonUtility.ToJson(userGenerals);
			Debug.Log(generalJson);
			UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).SetRawJsonValueAsync(generalJson);


			// Progression
			UserProgression userProgressions = new UserProgression
			  (
			  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1
			  );

			string progressionJson = JsonUtility.ToJson(userProgressions);
			Debug.Log(progressionJson);
			UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).SetRawJsonValueAsync(progressionJson);


			// Consumables
			UserConsumable userConsumable = new UserConsumable
				(
				10, 100, 10, 5
				);

			string consumableJson = JsonUtility.ToJson(userConsumable);
			Debug.Log(consumableJson);
			UserDatabaseReference.Child(UserPaths.PrimaryPaths.Consumable).SetRawJsonValueAsync(consumableJson);
		}*/

	private void CreateUserProfile(string username, string language)
	{
		// General
		UserGeneralMold userGeneralMold = new UserGeneralMold
		{
			Username = username,
			SignUpDate = DateTime.Now.ToString("dd/MM/yyyy"),
			Language = language
		};

		string generalJson = JsonUtility.ToJson(userGeneralMold);
		Debug.Log(generalJson);
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).SetRawJsonValueAsync(generalJson);


		// Progression
		UserProgressionMold userProgressionMold = new UserProgressionMold
		{
			Rank = "Cahil",
			Level = 0,
			WrongAnswers = 0,
			CorrectAnswers = 0,
			HighScore = 0,
			TotalPlayTime = 0f
		};

		string progressionJson = JsonUtility.ToJson(userProgressionMold);
		Debug.Log(progressionJson);
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).SetRawJsonValueAsync(progressionJson);
	}

	private void UpdateUserData(string secondaryPath, string key, object value)
	{
		//AddUserDataListener();
		FirebaseManager.UserDatabaseReference.Child(secondaryPath).Child(key).SetValueAsync(value);
	}

	private void GetUsers()
	{
		FirebaseManager.UserDatabaseReference.GetValueAsync().ContinueWith(task =>
		{
			if (task.IsFaulted)
			{
				//           LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
			}
			else if (task.IsCompleted)
			{
				DataSnapshot snapshot = task.Result;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();

				foreach (DataSnapshot x in snapshot.Children)
				{
					string key = x.Key;
					object value = snapshot.Child(key).Value;
					//Debug.Log(key);
					//Debug.Log(value);
					dictionary.Add(key, value);
					Debug.Log(dictionary);
				}
			}
		}
		);
	}

	private IEnumerator GetCurrentUserProfile()
	{
		//DatabaseReference getCurrentUserProfileReference = FirebaseDatabase.DefaultInstance.GetReference($"{UserPaths.Users}/{UserPaths.UserID}/{FirebaseManager.auth.CurrentUser.UserId}");

		Task<DataSnapshot> task = FirebaseManager.UserDatabaseReference.GetValueAsync();

		yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);
		if (task.IsCanceled)
		{
			Debug.LogWarning(UserTaskDebugs.GetCurrentUserProfile + DebugPaths.IsCanceled);
		}
		else if (task.IsFaulted)
		{
			Debug.LogError(UserTaskDebugs.GetCurrentUserProfile + DebugPaths.IsFaulted);
		}
		else if (task.IsCompleted)
		{
			DataSnapshot snapshot = task.Result;

			string json = snapshot.GetRawJsonValue();
			Debug.Log(json);

			// String General 
			CurrentUserProfileKeeper.Username = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Username).Value.ToString();
			//CurrentUserProfileKeeper.Country = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Country).Value.ToString();
			CurrentUserProfileKeeper.Language = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Language).Value.ToString();
			CurrentUserProfileKeeper.SignUpDate = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.SignUpDate).Value.ToString();
			//CurrentUserProfileKeeper.LastSeen = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.LastSeen).Value.ToString();

			// Bool General                                   
			//CurrentUserProfileKeeper.SignInStatus = bool.Parse(snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.SignInStatus).Value.ToString());
			//CurrentUserProfileKeeper.Intermateable = bool.Parse(snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Intermateable).Value.ToString());


			// Int Progression
			CurrentUserProfileKeeper.Level = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Level).Value.ToString());
			//CurrentUserProfileKeeper.Cup = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Cup).Value.ToString());
			CurrentUserProfileKeeper.Rank = snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Rank).Value.ToString();
			//CurrentUserProfileKeeper.Rank = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Rank).Value.ToString());
			CurrentUserProfileKeeper.TotalPlayTime = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.TotalPlayTime).Value.ToString());
			CurrentUserProfileKeeper.HighScore = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.HighScore).Value.ToString());
			//CurrentUserProfileKeeper.TotalMatches = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.TotalMatches).Value.ToString());
			//CurrentUserProfileKeeper.CompletedMatches = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.CompletedMatches).Value.ToString());
			//CurrentUserProfileKeeper.AbandonedMatches = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.AbandonedMatches).Value.ToString());
			CurrentUserProfileKeeper.CorrectAnswers = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.CorrectAnswers).Value.ToString());
			CurrentUserProfileKeeper.WrongAnswers = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.WrongAnswers).Value.ToString());
			//CurrentUserProfileKeeper.Wins = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Wins).Value.ToString());
			//CurrentUserProfileKeeper.Losses = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Losses).Value.ToString());
			//CurrentUserProfileKeeper.WinningStreak = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.WinningStreak).Value.ToString());

			// Int Consumable
			//CurrentUserProfileKeeper.Papcoin = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Consumable).Child(UserPaths.ConsumablePaths.Papcoin).Value.ToString());
			//CurrentUserProfileKeeper.Gem = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Consumable).Child(UserPaths.ConsumablePaths.Gem).Value.ToString());

			////Debug.Log("bir de bu ");

			//ActionManager.Instance.ShowUserProfilePanel();
		}
	}

	private void DeleteUserProfile()
	{
		FirebaseManager.UserDatabaseReference.RemoveValueAsync();
	}

	private void AddUserDataListener()
	{
		Debug.Log("Shigure");
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).ChildChanged += GetuserDAtaBridge;
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).ChildChanged += GetuserDAtaBridge;
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Consumable).ChildChanged += GetuserDAtaBridge;
	}

	private void RemoveUserDataListener()
	{
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).ChildChanged -= GetuserDAtaBridge;
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).ChildChanged -= GetuserDAtaBridge;
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Consumable).ChildChanged -= GetuserDAtaBridge;
	}

	private void GetuserDAtaBridge(object sender, ChildChangedEventArgs args)
	{
		Debug.Log("ML");
		if (FirebaseManager.auth.CurrentUser == null)
		{
			return;
		}
		else
		{
			StartCoroutine(GetCurrentUserProfile());
		}
	}
}
