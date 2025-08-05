using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequestManager : MonoBehaviour
{
    static WebRequestManager instance;
    public static WebRequestManager Instance => instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public IEnumerator DownloadImage(string url, Action<Texture2D> successCallback, Action failCallback)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                successCallback?.Invoke(texture);
            }
            else
            {
                Debug.LogError($"Failed to download image from {url}: {uwr.error}");
                failCallback?.Invoke();
            }
        }
    }


}

