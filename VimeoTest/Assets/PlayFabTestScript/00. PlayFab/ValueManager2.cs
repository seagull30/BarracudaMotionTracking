using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.CodeDom.Compiler;
using PlayFab.Internal;

public class ValueManager2 : MonoBehaviour
{
    [Header("int")]
    public InputField Input1;
    public InputField Input2;
    public Text LogText1;

    [Header("string")]
    public InputField Input3;
    public InputField Input4;

    [Header("Rank")]
    public InputField Input5;
    public Text LogText2;

    [Header("Interface")]
    public Text userName;

    //-----------------------------INTERFACE-----------------------------
    private void Start()
    {
        GetMyInfo();
    }

    private void Otherogin()
    {

    }

    public void GetMyInfo()
    {
        var request = new GetAccountInfoRequest { PlayFabId = PlayFabManager2.GetID()};
        PlayFabClientAPI.GetAccountInfo(request,
            (result) =>
            {
                userName.text = result.AccountInfo.Username;
            }, (error) => Debug.Log("정보 불러오기 실패"));
    
    }

    //-----------------------------INT DATA-----------------------------
    public void SetDataButton()
    {
        // 새로운 데이터를 업데이트함
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                // 각 통계 데이터에 값을 넣음
                new StatisticUpdate {StatisticName = Input1.text, Value = int.Parse(Input2.text)},
            }
        },
        (result) => { LogText1.text = "값 저장 성공"; }, // 값 저장하기 성공
        (error) => { LogText1.text = "값 저장 성공"; }); // 값 저장하기 실패
    }
    private void GetData(string DataName)
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) => // 값 불러오기 성공
            {   
                foreach (var eachStat in result.Statistics)
                {
                    if (eachStat.StatisticName == DataName)
                    {
                        LogText1.text = $"{DataName}의 값 = {eachStat.Value}";
                        break;
                    }
                }
            },
            (error) => { LogText1.text = "값 불러오기 실패"; });// 값 불러오기 실패
    }
    public void GetButton()
    {
        GetData(Input1.text);
    }

    //-----------------------------STRING DATA-----------------------------

    public void SetStringDataButton()
    {
        var request = new UpdateUserDataRequest { Data = new Dictionary<string, string>() { { Input3.text, Input4.text } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => LogText1.text = "스트링값 저장 성공", (error) => LogText1.text = "스트링값 저장 실패");
    }

    public void GetStringDataButton()
    {
        var request = new GetUserDataRequest() { PlayFabId = PlayFabManager2.GetID() };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            foreach (var Data in result.Data)
            {
                if (Data.Key == Input3.text)
                {
                    LogText1.text = Data.Key + " : " + Data.Value;
                }
            }
        }, (error) => LogText1.text = "스트링값 불러오기 실패");
    }

    //-----------------------------RANK DATA-----------------------------

    public void GetLeaderBoardButton()
    {
        GetLeaderBoard(Input5.text);
    }

    public void GetLeaderBoard(string ValueName)
    {
        var request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = ValueName, MaxResultsCount = 20, ProfileConstraints = new PlayerProfileViewConstraints() { ShowLocations = true, ShowDisplayName = true } };
        LogText2.text = "";
        PlayFabClientAPI.GetLeaderboard(request, (result) =>
        {
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                var CurrentBoard = result.Leaderboard[i];
                
                LogText2.text += CurrentBoard.Profile.Locations[0].ContinentCode.Value + "/" + CurrentBoard.DisplayName + "/" + CurrentBoard.StatValue + "\n";
            }
        },
        (error) => LogText2.text = "랭킹 불러오기 실패");
    }
}
