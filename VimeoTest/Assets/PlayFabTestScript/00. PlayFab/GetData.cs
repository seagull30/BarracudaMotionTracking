using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class GetData : MonoBehaviour
{
    public Text UserName;

    void CreatePlayerAndUpdateDisplayName()
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CustomId = "PlayFabGetPlayerProfileCustomId",
            CreateAccount = true
        }, result => {
            Debug.Log("Successfully logged in a player with PlayFabId: " + result.PlayFabId);
            UpdateDisplayName();
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    void UpdateDisplayName()
    {   
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = "UnicornTossMaster"
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    void GetPlayerProfile(string playFabId)
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
        result => Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile.DisplayName),
        error => Debug.LogError(error.GenerateErrorReport()));
    }

}
