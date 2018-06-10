using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager {

	public static void Save(GameData gd, string saveName)
    {
        string destination = Application.persistentDataPath + "/"+ saveName+".dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, gd);
        file.Close();
    }

    public static GameData LoadFile(string saveName)
    {
        GameData gd = new GameData();

        string destination = Application.persistentDataPath + "/" + saveName + ".dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.Log("File not found - new Empty file returned");
            return gd;
        }

        BinaryFormatter bf = new BinaryFormatter();
        gd = (GameData)bf.Deserialize(file);
        file.Close();

        return gd;
    }

    public static void Load(string saveName,MonoBehaviour caller)
    {
        GameData gd = SaveManager.LoadFile(saveName);

        GameMasterManager gMM = GameMasterManager.instance;
        gMM.gd_nextLevel = gd;

        SceneManager.LoadScene(gd.sceneName);
        
    }

}
