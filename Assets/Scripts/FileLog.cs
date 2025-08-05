using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;


public class FileLog
{
    public static readonly string FILENAME = "DebugLog.log";
    public static readonly string PREV_FILENAME = "DebugLog-prev.log";
    public static readonly string ZIP_FILENAME = "DebugLog.log.gz";

    //static string logFilePath = Application.persistentDataPath + "/" + FILENAME;
    //static string prevLogFilePath = Application.persistentDataPath + "/" + PREV_FILENAME;
    //static string zipFilePath = Application.persistentDataPath + "/" + ZIP_FILENAME;

    static string logFilePath;
    static string prevLogFilePath;
    static string zipFilePath;

    static readonly int TRIM_LINE_LENGTH = 255;
	
	private static StreamWriter sw = null;

	public static bool writeFile = true;

    public static bool trimLine = false;

    static FileLog()
    {
        string curPath = Application.dataPath + "/..";
        if (Application.platform == RuntimePlatform.OSXPlayer)
            curPath += "/../..";

        //string curPath = Path.GetFullPath(".");
        UnityEngine.Debug.Log("FileLog: curPath: " + curPath);

        logFilePath = curPath + "/" + FILENAME;
        prevLogFilePath = curPath + "/" + PREV_FILENAME;
        zipFilePath = curPath + "/" + ZIP_FILENAME;
    }

    public static void Enable()
    {
        Application.logMessageReceivedThreaded += LogMessageReceivedHandler;

        UnityEngine.Debug.Log("FileLog: Enable Time:" + System.DateTime.Now.ToString());
    }

    public static void Disable()
    {
        UnityEngine.Debug.Log("FileLog: Disable");

        Application.logMessageReceivedThreaded -= LogMessageReceivedHandler;

        Close();
    }

    public static void LogMessageReceivedHandler(string logString, string stackTrace, LogType type)
    {
        if (trimLine && (logString.Length > TRIM_LINE_LENGTH))
            WriteLogFile(type.ToString() + ": " + logString.Substring(0, TRIM_LINE_LENGTH) + "...[TRIMMED]" );
        else
            WriteLogFile(type.ToString() + ": " + logString);

        if (type == LogType.Exception)
            WriteLogFile("CallStack: " + stackTrace);
    }

	public static void CreateNewLog()
	{
		try
		{
            sw = File.CreateText(logFilePath);
		}
		catch (Exception e)
		{
            Debug.LogException(e);
		}
	}

	public static void Close()
	{
		sw = null;
	}
        
	public static void BackupPrevLog()
	{
        Debug.LogWarning("Info: BackupPrevLog");

		try
		{
            if (File.Exists(logFilePath))
            {
                //Archiver.Compress(logFilePath, zipFilePath);

    			System.IO.File.Copy(logFilePath, prevLogFilePath, true);
            }
		}
		catch (Exception e)
		{
            Debug.LogException(e);
		}
	}

/*
    public static void CompressPrevLog()
    {
        Debug.LogWarning("Info: CompressPrevLog");

        try
        {
            if (File.Exists(prevLogFilePath))
            {
                Tracer tracer = new Tracer("CompressPrevLog");

                Archiver.Compress(prevLogFilePath, zipFilePath);

                tracer.Exit();
                tracer = null;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
*/

    public static string GetZipLogFilePath()
    {
        return zipFilePath;
    }

    public static bool CheckZipLogFileExists()
    {
        return File.Exists(zipFilePath);
    }

    public static void TrimLog(string msg)
    {
        if ( (Application.platform == RuntimePlatform.OSXEditor) || (Application.platform == RuntimePlatform.WindowsEditor) )
        {
            UnityEngine.Debug.Log(msg);
        }
        else
        {
            if (msg.Length > TRIM_LINE_LENGTH)
                UnityEngine.Debug.Log(msg.Substring(0, TRIM_LINE_LENGTH) + "...[TRIMMED]" );
            else
                UnityEngine.Debug.Log(msg);
        }
    }

	public static void WriteLogFile(string line)
	{
		if ( !writeFile )
			return;

		if (sw == null)
			sw = File.AppendText(logFilePath);

		try
		{
    		sw.WriteLine(line);

			sw.Flush();
		}
		catch (Exception e)
		{
			UnityEngine.Debug.LogError("WriteLogFile Exception:" + e.ToString());
		}
	}

}
