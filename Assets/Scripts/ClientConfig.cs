using System;
using System.IO;
using UnityEngine;

[Serializable]
public class ClientConfig
{
    public bool isInit = false;
    public bool isFileExists = false;


    public int scannerMode = 0;
    public float scannerWaitTime = 1;

    public float switchWaitTime = 1;


    public int debugLog = 1;    

    public static ClientConfig BuildFromFile()
    {
        ClientConfig clientConfig;

        string curPath = Application.dataPath + "/..";
        if (Application.platform == RuntimePlatform.OSXPlayer)
            curPath += "/../..";

        string filePath = curPath + "/" + "ClientConfig.json";

        UnityEngine.Debug.Log("ClientConfig: filePath: " + filePath);

        if (System.IO.File.Exists(filePath))
        {
            string configFileText = File.ReadAllText(filePath);

            clientConfig = JsonUtility.FromJson<ClientConfig>(configFileText);

            clientConfig.isFileExists = true;
        }
        else
        {
            clientConfig = new ClientConfig();
        }

        clientConfig.isInit = true;

        return clientConfig;
    }

}
