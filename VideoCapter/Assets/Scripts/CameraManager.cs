using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class CameraManager : MonoBehaviour
{
    WebCamTexture camTexture;

    public RawImage cameraViewImage;

    private void Start()
    {
        //카메라 권한 부여
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            //권한이 없을때 권한 부여
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    //카메라켜기
    public void CameraOn()
    {

        //카메라가 없으면 실행 안되게 하기
        if(WebCamTexture.devices.Length == 0)
        {
            Debug.Log("no Camera");
            return;
        }

        //스마트폰의 카메라 정보를 모두 가져옴
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndex = -1;

        //후면 카메라 찾기
        for(int i = 0; i < devices.Length; i++)
        {
            if(devices[i].isFrontFacing == false)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        //카메라 켜기
        if(selectedCameraIndex >= 0)
        {
            //선택된 후면 카메라를 가져옴.
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);

            //카메라 프레임 설정
            camTexture.requestedFPS = 30;

            //영상을 raw Image에 할당
            cameraViewImage.texture = camTexture;

            //카메라 시작하기
            camTexture.Play();
        }

    }

    //카메라 끄기
    public void CameraOff()
    {
        //카메라가 있으면
        if(camTexture != null)
        {
            //카메라 정지
            camTexture.Stop();

            //카메라 객체 반납
            WebCamTexture.Destroy(camTexture);

            //변수 초기화
            camTexture = null;
        }
    }


}
