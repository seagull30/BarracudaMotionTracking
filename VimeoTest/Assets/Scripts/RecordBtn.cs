using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordBtn : MonoBehaviour
{
    private Button[] Btns;
    private int BtnCount;

    enum BtnNum
    {
        Start,
        Pause,
        Resume,
        Upload,
        End,
        Cancel,
        Menu,

    }

    private void Awake()
    {
        BtnCount = transform.childCount;
        Btns = new Button[BtnCount];

        for (int i = 0; i < BtnCount; ++i)
        {
            Btns[i] = transform.GetChild(i).GetComponent<Button>();
            Debug.Log(Btns[i].gameObject.name);
            Btns[i].gameObject.SetActive(false);
        }
        Btns[(int)BtnNum.Start].gameObject.SetActive(true);
        Btns[(int)BtnNum.Menu].gameObject.SetActive(true);
    }
   
    public void StartRecording()
    {
        Btns[(int)BtnNum.Pause].gameObject.SetActive(true);
        Btns[(int)BtnNum.End].gameObject.SetActive(true);
        Btns[(int)BtnNum.Cancel].gameObject.SetActive(true);

        Btns[(int)BtnNum.Menu].gameObject.SetActive(false);
        Btns[(int)BtnNum.Upload].gameObject.SetActive(false);
    }

    public void PauseRecording()
    {
        Btns[(int)BtnNum.Pause].gameObject.SetActive(false);
        Btns[(int)BtnNum.Resume].gameObject.SetActive(true);

    }

    public void ResumeRecording()
    {
        Btns[(int)BtnNum.Resume].gameObject.SetActive(false);
        Btns[(int)BtnNum.Pause].gameObject.SetActive(true);

    }

    public void EndofRecording()
    {
        Btns[(int)BtnNum.Resume].gameObject.SetActive(false);
        Btns[(int)BtnNum.Pause].gameObject.SetActive(false);
        Btns[(int)BtnNum.Cancel].gameObject.SetActive(false);
        Btns[(int)BtnNum.End].gameObject.SetActive(false);

        Btns[(int)BtnNum.Start].gameObject.SetActive(true);
        Btns[(int)BtnNum.Menu].gameObject.SetActive(true);
        Btns[(int)BtnNum.Upload].gameObject.SetActive(true);
    }

    public void CancelRecording()
    {
        Btns[(int)BtnNum.Resume].gameObject.SetActive(false);
        Btns[(int)BtnNum.Pause].gameObject.SetActive(false);
        Btns[(int)BtnNum.End].gameObject.SetActive(false);
        Btns[(int)BtnNum.Cancel].gameObject.SetActive(false);

        Btns[(int)BtnNum.Start].gameObject.SetActive(true);
        Btns[(int)BtnNum.Menu].gameObject.SetActive(true);
    }

    public void Upload()
    {
        Btns[(int)BtnNum.Upload].gameObject.SetActive(false);

    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
