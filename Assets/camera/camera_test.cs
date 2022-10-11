using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class camera_test : MonoBehaviour
{
    [SerializeField]
    private int m_width = 1920;
    [SerializeField]
    private int m_height = 1080;
    [SerializeField]
    private RawImage m_displayUI = null;

    private WebCamTexture m_webCamTexture = null;


    private IEnumerator Start()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogFormat("Camera not found");
            yield break;
        }

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogFormat("Camera access permission denied");
            yield break;
        }

        WebCamDevice userCameraDevice = WebCamTexture.devices[0];
        m_webCamTexture = new WebCamTexture(userCameraDevice.name, m_width, m_height);
        m_displayUI.texture = m_webCamTexture;
        // さあ、撮影開始だ！
        m_webCamTexture.Play();
    }
} // class TestCamera