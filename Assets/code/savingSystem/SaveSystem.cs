using System.Collections;
using System.Collections.Generic;
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
            string saveLocation = Application.persistentDataPath;
            Path.Combine(saveLocation, "Ratstonauts", "Save");
            return saveLocation;
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

        //TODO: the actual saving
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
        _saver.PrepareRead(saveFilePath);

        GameManager.Instance.Load(_saver);
        _saver.FinalizeRead();
    }
}
