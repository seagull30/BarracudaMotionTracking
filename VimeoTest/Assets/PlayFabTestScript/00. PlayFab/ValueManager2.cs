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
            }, (error) => Debug.Log("���� �ҷ����� ����"));
    
    }

    //-----------------------------INT DATA-----------------------------
    public void SetDataButton()
    {
        // ���ο� �����͸� ������Ʈ��
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                // �� ��� �����Ϳ� ���� ����
                new StatisticUpdate {StatisticName = Input1.text, Value = int.Parse(Input2.text)},
            }
        },
        (result) => { LogText1.text = "�� ���� ����"; }, // �� �����ϱ� ����
        (error) => { LogText1.text = "�� ���� ����"; }); // �� �����ϱ� ����
    }
    private void GetData(string DataName)
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) => // �� �ҷ����� ����
            {   
                foreach (var eachStat in result.Statistics)
                {
                    if (eachStat.StatisticName == DataName)
                    {
                        LogText1.text = $"{DataName}�� �� = {eachStat.Value}";
                        break;
                    }
                }
            },
            (error) => { LogText1.text = "�� �ҷ����� ����"; });// �� �ҷ����� ����
    }
    public void GetButton()
    {
        GetData(Input1.text);
    }

    //-----------------------------STRING DATA-----------------------------

    public void SetStringDataButton()
    {
        var request = new UpdateUserDataRequest { Data = new Dictionary<string, string>() { { Input3.text, Input4.text } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => LogText1.text = "��Ʈ���� ���� ����", (error) => LogText1.text = "��Ʈ���� ���� ����");
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
        }, (error) => LogText1.text = "��Ʈ���� �ҷ����� ����");
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
        (error) => LogText2.text = "��ŷ �ҷ����� ����");
    }
}
