using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AppKit
{
    /// <summary>
    /// Barcode Scanner Input
    /// 
    /// Tested on http://amzn.asia/6JDu51P
    /// </summary>
    public class BarcodeScanner : MonoBehaviour
    {
        public const int ScannerWaitReturnMode = 0;
        public const int ScannerWaitTimeMode = 1;

        public event Action<string> OnScanned;

        StringBuilder builder;

        float firstScanTime = 0;

        public static bool isUsing = false;


        void Start()
        {
            builder = new StringBuilder();
        }

        private void OnEnable()
        {
            Debug.Log("BarcodeScanner.OnEnable");

            isUsing = true;
        }

        private void OnDisable()
        {
            Debug.Log("BarcodeScanner.OnDisable");

            isUsing = false;
        }

        void Update()
        {
            string str = Input.inputString;
            if (!string.IsNullOrEmpty(str))
            {
                if (builder.Length == 0)
                {
                    firstScanTime = Time.realtimeSinceStartup;

                    Debug.Log("set firstScanTime: " + firstScanTime);

                    Debug.Log("check time: " + (firstScanTime + ConfigManager.Instance.clientConfig.scannerWaitTime) );

                    Debug.Log("scannerMode: " + ConfigManager.Instance.clientConfig.scannerMode);
                    
                }

                builder.Append(str);

                Debug.Log("code: " + str);
            }


            // Detect return key for ScannerWaitReturnMode
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (OnScanned != null)
                {
                    OnScanned(builder.ToString().TrimEnd('\r', '\n'));
                }
                builder.Clear();

                firstScanTime = 0;
            }


            if (ConfigManager.Instance.clientConfig.scannerMode == ScannerWaitTimeMode)
            {
                if ((firstScanTime != 0) && ((firstScanTime + ConfigManager.Instance.clientConfig.scannerWaitTime) < Time.realtimeSinceStartup) )
                {
                    Debug.Log("---1");

                    if (OnScanned != null)
                    {
                        OnScanned(builder.ToString().TrimEnd('\r', '\n'));
                    }
                    builder.Clear();

                    firstScanTime = 0;
                }
                //else
                //{
                //    Debug.Log("---2");
                //}
            }

        }
    }
}