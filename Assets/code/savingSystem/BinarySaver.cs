using System.IO;
using System;
using UnityEngine;

public class BinarySaver
{
    private BinaryReader _reader;
    private BinaryWriter _writer;

    public bool PrepareRead(string path) //called before reading the data from save file. Opens filestream and creates the binary reader.
    {
        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            _fileStream = File.Open(path, FileMode.Open, FileAccess.Read);
            _reader = new BinaryReader(_fileStream);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
        return true;
    }

    public void FinalizeRead() //called after file is read to release memory and save file
    {
        _reader.Close();
        _fileStream.Close();

        _reader = null;
        _fileStream = null;
    }

    public bool PrepareWrite(string path)
    {
        try
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
            _writer = new BinaryWriter(_fileStream);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }

        return true;
    }

    public void FinalizeWrite()
    {
        _writer.Close();
        _fileStream.Close();

        _writer = null;
        _fileStream = null;
    }
    private FileStream _fileStream;

    #region Reading
    public int ReadInt()
    {
        return _reader.ReadInt32();
    }
    public float ReadFloat()
    {
        return _reader.ReadSingle();
    }
    public bool ReadBool()
    {
        return _reader.ReadBoolean();
    }
    public string ReadString()
    {
        return _reader.ReadString();
    }
    public int ReadTime()
    {
        int year = _reader.ReadInt32();
        int month = _reader.ReadInt32();
        int day = _reader.ReadInt32();
        int hour = _reader.ReadInt32();
        int minute = _reader.ReadInt32();
        int second = _reader.ReadInt32();
        return DateTime(year, month, day, hour, minute, second);
    }
    #endregion

    #region Writing
    public void WriteInt(int value)
    {
        _writer.Write(value);
    }
    public void WriteFloat(float value)
    {
        _writer.Write(value);
    }
    public void WriteBool(bool value)
    {
        _writer.Write(value);
    }
    public void WriteString(string value)
    {
        _writer.Write(value);
    }

    public void WriteTime(DateTime time)
    {
        _writer.Write(time.year);
        _writer.Write(time.month);
        _writer.Write(time.day);
        _writer.Write(time.hour);
        _writer.Write(time.minute);
        _writer.Write(time.second);
    }
    #endregion
}
