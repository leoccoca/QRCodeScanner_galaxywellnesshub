﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    // The singleton instance
    protected static ConfigManager instance = null;

    public static ConfigManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Debug
    //[NonSerialized]
    public ClientConfig clientConfig = new ClientConfig();

    void Awake()
    {
        Debug.Log("### ConfigManager Awake");

        // set the singleton instance
        //instance = this;
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

        Init();
    }

    private void Init()
    {
        FileLog.BackupPrevLog();
        FileLog.CreateNewLog();
        FileLog.Enable();

        clientConfig = ClientConfig.BuildFromFile();

        if (clientConfig.debugLog == 0)
            FileLog.Disable();

    }

}

