using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static List<PlayerProfile> savedGames = new List<PlayerProfile>(3);

    // сохраненные игры

    static SaveLoad()
    {
        savedGames.Add(null);
        savedGames.Add(null);
        savedGames.Add(null);
        Load();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.sve"))
        {
            MonoBehaviour.print("Load game from " + Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sve", FileMode.Open);
            savedGames = (List<PlayerProfile>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void Save(PlayerProfile playerProf, int currentSlot)
    {
        MonoBehaviour.print(Application.persistentDataPath);
        savedGames[currentSlot] = playerProf;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.sve");
        bf.Serialize(file, savedGames);
        file.Close();
    }
}