using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabManager2 : MonoBehaviour
{
    public static PlayFabManager2 instance = null;

    private void Awake()
    {
        SignUpPage.SetActive(false);
        SystemPage.SetActive(false);
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }
    //----- �̱��� -----

    [Header("Sign In")]
    public InputField EmailInput;
    public InputField PasswordInput;

    [Header("Sign Up")]
    public InputField EmailSignUp;
    public InputField PasswordSignUp;
    public InputField PasswordCheckSignUp;
    public InputField NameSignUp;

    [Header("System")]
    public Text SystemText;

    [Header("Page")]
    public GameObject SignUpPage;
    public GameObject SystemPage;

    public static string ID;

    public static string GetID()
    {
        return ID;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)&& Input.GetKeyDown(KeyCode.S))
        {
            Otherogin();
        }
    }

    //�ӽ�
    private void Otherogin()
    {
        EmailInput.text = "zxc@zxc.zxc";
        PasswordInput.text = "zxczxczxc";
        LoginButton();
    }

    public void LoginButton()
    {
        // �α��ο� �ʿ��� ���� �Է�
        var Email_request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        // ���� ���� �α��� ���� ���� �̵� iD���� ���⼭ ����
        PlayFabClientAPI.LoginWithEmailAddress(Email_request, (result) => { ID = result.PlayFabId; SceneManager.LoadScene(2); }, OnLoginFailure);   
    }
    bool IsRegister;
    public void RegisterButton()
    {
        IsRegister = true;
        // ȸ�����Կ� �ʿ��� ���� �Է�
        var Email_request = new RegisterPlayFabUserRequest { Email = EmailSignUp.text, Password = PasswordSignUp.text, Username = NameSignUp.text ,DisplayName = NameSignUp.text};

        // ���� ���� üũ
        RegisterCheck();
        
        // ȸ������
        if (IsRegister)
        {
            PlayFabClientAPI.RegisterPlayFabUser (Email_request, OnRegisterSuccess, OnRegisterFailire);
        }
    }
    public void RegisterCheck()
    {
        // �̸��� Ȯ��
        // ��Ƴ� �̰�

        // ��й�ȣ �ڸ��� üũ
        if (PasswordSignUp.text.Length <= 8)
        {
            Debug.LogWarning("��й�ȣ 8�ڸ� �̻�");
            _SystemPage("��й�ȣ 8�ڸ� �̻�");
            IsRegister = false;
            return;
        }

        // ��й�ȣ Ȯ��
        if (PasswordSignUp.text != PasswordCheckSignUp.text)
        {
            Debug.LogWarning("��й�ȣ Ȯ��");
            _SystemPage("��й�ȣ Ȯ��");
            IsRegister = false;
            return;
        }
    }

    public void SignTrueButton()
    {
        //ȸ������ ������ �ø���
        _SignUpPage(true);
    }

    public void SignFalseButton()
    {
        //ȸ������ ������ ������
        _SignUpPage(false);
    }
    public void SystemButton()
    {
        SystemPage.SetActive(false);
    }

    private void _SignUpPage(bool Value) { SignUpPage.SetActive(Value); }
    private void _SystemPage(string Text) { SystemPage.SetActive(true); SystemText.text = Text; }

    private void OnLoginSuccess(LoginResult result) 
    {
        Debug.Log("�α��� ����");



        //_SystemPage("�α��� ����");
        SceneManager.LoadScene(2);
    }
    private void OnLoginFailure(PlayFabError error) 
    {
        Debug.LogWarning("�α��� ����");
        _SystemPage("�α��� ����");
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ������ ����");
        _SignUpPage(false);
        _SystemPage("ȸ������ ����");
    }
    private void OnRegisterFailire(PlayFabError error) 
    {
        Debug.LogWarning("ȸ������ ����");
        _SystemPage("ȸ������ ����");
    }
}
