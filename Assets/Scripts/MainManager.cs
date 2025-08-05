using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour {


    static MainManager instance;
    public static MainManager Instance => instance;

    [SerializeField] RawImage ScannedImage;

    public TMP_Text resultText;

    [SerializeField] Button PrintButton;


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
    private void OnEnable()
    {
        PrintButton.onClick.AddListener(PrintPhoto);
        ResetPrintJob();
    }

    private void OnDisable()
    {
        PrintButton.onClick.RemoveListener(PrintPhoto);
    }

    void ResetPrintJob()
    {
        PrintButton.gameObject.SetActive(false);
        ScannedImage.gameObject.SetActive(false);
        resultText.text = "";
    }

    public void DisplayScannedResult(string msg)
    {
        resultText.text = msg;

    }

    public void DownloadImage(string url)
    {
        PrintButton.gameObject.SetActive(false);
        string photoUrl = url;
        resultText.text = "BarCode Scanned";
        StartCoroutine(WebRequestManager.Instance.DownloadImage(photoUrl, DownloadImageSuccessCallback, DownloadImageFailCallback));
    }



    void DownloadImageSuccessCallback(Texture2D tex)
    {
        Debug.Log("Found download the image, display now");

        ScannedImage.texture = tex;
        resultText.text = "ValidQRCode";
        ScannedImage.gameObject.SetActive(true);
        PrintButton.gameObject.SetActive(true);
    }

    void DownloadImageFailCallback()
    {
        Debug.Log("Cant download the image");
        resultText.text = "InValidQRCode";
        ScannedImage.gameObject.SetActive(false);
        PrintButton.gameObject.SetActive(false);
    }

    void PrintPhoto()
    {
        resultText.text = "Printing Photo, Please wait";
        //PrinterController.Instance.DoPrint(ScannedImage);
        PrintButton.gameObject.SetActive(false);
    }

    
}
