using UnityEngine;
using System;
using System.IO;

public class SaveSystem
{

    private BinarySaver _saver;
    public SaveSystem() //constructor since we are not deriving from monobehaviour
    {
        try
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public string SaveFolder
    {
        get
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            //android devices
            return Path.Combine(Application.persistentDataPath, "Ratstonauts", "Save");
            #else
            //desktop
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documents, "Ratstonauts", "Save");
            #endif
        }
    }

    public string MainSaveSlot { get { return "MainSave";}}

    public string QuickSaveSlot { get { return "QuickSave"; } }

    public string FileExtension { get { return ".save"; } }

    public void Save(string slot)
    {
        _saver = new BinarySaver();
        string saveFilePath = Path.Combine(SaveFolder, slot + FileExtension);
        _saver.PrepareWrite(saveFilePath);

        GameManager.Instance.Save(_saver);

        //if we have gameobjects to save, uncomment below
        //ISaveable[] saveables = GameObject.FindObjectOfType<MonoBehaviour>(includeInactive: true).OfType<ISaveable>().ToArray();
        //_saver.WriteInt(saveables.Length);
        //foreach(ISaveable saveable in saveables){ saveable.Save(_saver);}
        //

        _saver.FinalizeWrite();
    }

    public void Load(string slot)
    {
        _saver = new BinarySaver();
        string saveFilePath = Path.Combine(SaveFolder, slot + FileExtension);
        if (_saver.PrepareRead(saveFilePath) == false)
        {
            return;
        }

        GameManager.Instance.Load(_saver);
        _saver.FinalizeRead();
    }
}
