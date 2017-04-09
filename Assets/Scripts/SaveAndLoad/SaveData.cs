using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable()]
public class SaveData : ISerializable
{

    public int HitPoints { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public IWeapon CurrentWeapon { get; set; }
    public int CurrentLevel { get; set; }


    public SaveData()
    {
    }

    public SaveData(SerializationInfo info, StreamingContext context)
    {
        HitPoints = (int) info.GetValue("HitPoints", typeof(int));
        Damage = (int) info.GetValue("Damage", typeof(int));
        Range = (int) info.GetValue("Range", typeof(int));
        CurrentWeapon = (IWeapon) info.GetValue("CurrentWeapon", typeof(IWeapon));
        CurrentLevel = (int) info.GetValue("CurrentLevel", typeof(int));
    }



    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("HitPoints", (HitPoints));
        info.AddValue("Damage", Damage);
        info.AddValue("Range", Range);
        info.AddValue("CurrentWeapon", CurrentWeapon);
        info.AddValue("CurrentLevel", CurrentLevel);
    }

    public void Save()
    {
        SaveData data = new SaveData();
        Stream stream = File.Open("MySavedGame.game", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder();
        Debug.Log("Writing Information");
        bformatter.Serialize(stream, data);
        stream.Close();
    }

    public void Load()
    {
        SaveData data = new SaveData();
        Stream stream = File.Open("MySavedGame.gamed", FileMode.Open);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder();
        Debug.Log("Reading Data");
        data = (SaveData) bformatter.Deserialize(stream);
        stream.Close();
    }

}
