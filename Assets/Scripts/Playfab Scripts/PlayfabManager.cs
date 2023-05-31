using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance;

    [Header("UI")]
    public TMP_Text messageText, LoggedAs;
    public TMP_InputField registerEmailInputField, registerPasswordInputField, registerDisplayNameField, loginEmailInputField, loginPasswordInputField, emailRecoveryInputField;
    public GameObject SpeedrunLoginMenu, speedrunLeaderboard;

    [Header("Score Table UI")]
    public GameObject rowPrefab;
    public Transform rowsParent;

    private void Awake() {
        Instance = this;
    }

    public void Login() {
        var request = new LoginWithEmailAddressRequest {
            Email = loginEmailInputField.text,
            Password = loginPasswordInputField.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void Register() {
        if (registerPasswordInputField.text.Length < 6)
        {
            messageText.text = "Password should contain at least 6 characters";
            return;
        }

        if (registerDisplayNameField.text.Length < 3)
        {
            messageText.text = "Username field should contain at least 3 characters";
            return;
        }

        var request = new RegisterPlayFabUserRequest { 
            Email = registerEmailInputField.text,
            Password = registerPasswordInputField.text,
            RequireBothUsernameAndEmail = false,
            DisplayName = registerDisplayNameField.text,
            
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void resetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailRecoveryInputField.text,
            TitleId = "E4EB2"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnSuccess(LoginResult result) {
        Debug.Log("Successful login!");
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        messageText.text = "Register completed!";
    }

    void OnLoginSuccess(LoginResult result) {
        Debug.Log("Successful login!");
        SpeedrunLoginMenu.SetActive(false);
        speedrunLeaderboard.SetActive(true);
    }

    void OnError(PlayFabError error) {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result) {
        messageText.text = "Password reset email sent!";
    }

    public void LogOut() {
        PlayFabClientAPI.ForgetAllCredentials();
        Debug.Log("Logged out successfuly");
    }

    public void SendLeaderboard(int time) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Speedrun Score Time",
                    Value = time
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }


    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "Speedrun Score Time",
            StartPosition = 0,
            MaxResultsCount = 8
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result) {
        foreach(Transform item in rowsParent) {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard) {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            //texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = (Convert.ToSingle(item.StatValue) / 100).ToString() + "s";
        }
    }

    public void IfLogged() {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            SpeedrunLoginMenu.SetActive(false);
            speedrunLeaderboard.SetActive(true);
        }
        else
        {
            Debug.Log("User not logged in");
        }
    }
}
