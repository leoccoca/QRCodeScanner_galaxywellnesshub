using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrinterController : MonoBehaviour
{

    static PrinterController instance;
    public static PrinterController Instance
    {
        get
        {
            return instance;
        }
    }




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




    public void DoPrint(RawImage targetImage)
    {
        Debug.Log("ResultPage.DoPrint()");

        PrinterPlugin.print(targetImage.texture, false, PrinterPlugin.PrintScaleMode.FILL_PAGE);
    }
}
