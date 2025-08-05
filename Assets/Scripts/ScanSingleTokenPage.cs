using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScanSingleTokenPage: MonoBehaviour
{
    public AppKit.BarcodeScanner barcodeScanner;

    [SerializeField] string testCode = "https://galaxywellnesshub.sharing.com.hk/poster/0a0efff9-c6f2-4a2a-a2a5-e9a1cdd87a57_poster.jpg";
    //public bool readyToScan = true;


    // Start is called before the first frame update
    void Start()
    {
        //MainManager.Instance.Init();

        barcodeScanner.OnScanned += OnScanned;
    }

    private void OnEnable()
    {
        Debug.Log(">>> OnEnable");

        //readyToScan = true;

    }


    // Update is called once per frame
    void Update()
    {

		if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Test master qr code
            //string testCode = "XKGE103Z";                

            OnScanned(testCode);
        }


    }

    //barcode receive some string
    public void OnScanned(string code)
    {
        MainManager.Instance.DownloadImage(code);
    }


}
