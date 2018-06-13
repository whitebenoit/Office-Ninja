using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionaries : MonoBehaviour
{


    private static Dictionaries _instance = null;
    private static bool initialized = false;

    public static Dictionaries instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Dictionaries)FindObjectOfType(typeof(Dictionaries));
                if (_instance == null)
                {
                    // Create gameObject and add component
                    _instance = (new GameObject("DictionariesObj")).AddComponent<Dictionaries>();
                }
                _instance.FillDictionaries();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        instance.Init();
    }

    void Init()
    {
        if (!initialized)
        {
            initialized = true;
            GameMasterManager.DontDestroyOnLoad(this);
            FillDictionaries();
        }
    }



    [SerializeField]
    public Dictionary<DirectionElement, Vector3> dic_directions = new Dictionary<DirectionElement, Vector3>();

    [SerializeField]
    public Dictionary<PlayerCharacterController.ActionListElement, Sprite> dic_BtnUIImageRess = new Dictionary<PlayerCharacterController.ActionListElement, Sprite>();

    public enum DirectionElement { FORWARD, RIGHT, LEFT, BACK };
    
    public enum ColorNames { SIGHT_RED, SIGHT_BLUE};


    public enum ItemName { SCREWDRIVER, LAXATIVE, SHURIKEN, CALTROPS };
    public enum ItemStateName { OWNED, MISSING, NOTIMPLEMENTEDYET };
    [SerializeField]
    public Dictionary<ColorNames, Color> dic_Colors = new Dictionary<ColorNames, Color>();

    public enum AudioName { NONE, TP,MANAGER,MAIN_MENU,NEW_OBJECT,
                            TOILET,PANIC,SCREWDRIVER,DETECTED,
                            MENU_IN, MENU_OUT,VENTILATION}
    public Dictionary<AudioName, int> dic_audioID = new Dictionary<AudioName, int>();


    public enum MusicName
    {
        NONE, SALARYMAN, NINJA
    }
    public Dictionary<MusicName, int> dic_musicID = new Dictionary<MusicName, int>();


    public void FillDictionaries()
    {
        
        dic_BtnUIImageRess.Clear();
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.INTERACT, Resources.Load<Sprite>("Sprites/btn_A"));
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.HIDE, Resources.Load<Sprite>("Sprites/btn_B"));
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.USE, Resources.Load<Sprite>("Sprites/btn_X"));
        dic_BtnUIImageRess.Add(PlayerCharacterController.ActionListElement.DASH, Resources.Load<Sprite>("Sprites/btn_Y"));

        dic_directions.Clear();
        dic_directions.Add(DirectionElement.FORWARD, Vector3.forward);
        dic_directions.Add(DirectionElement.RIGHT, Vector3.right);
        dic_directions.Add(DirectionElement.LEFT, Vector3.left);
        dic_directions.Add(DirectionElement.BACK, Vector3.back);

        dic_Colors.Clear();
        dic_Colors.Add(ColorNames.SIGHT_RED, new Color(255, 0, 0));
        dic_Colors.Add(ColorNames.SIGHT_BLUE, new Color(0, 0, 255));

        if (dic_audioID.Count <= 0 || (SoundManager.soundsAudio == null || SoundManager.soundsAudio.Count<=0))
        {
            dic_audioID.Clear();
            AddToDicAudio(AudioName.DETECTED, "Sounds/audioDetected");
            AddToDicAudio(AudioName.MAIN_MENU, "Sounds/audioMainMenu");
            AddToDicAudio(AudioName.MANAGER, "Sounds/audioManager");
            AddToDicAudio(AudioName.MENU_IN, "Sounds/audioMenuIn");
            AddToDicAudio(AudioName.MENU_OUT, "Sounds/audioMenuOut");
            AddToDicAudio(AudioName.NEW_OBJECT, "Sounds/audioNewObject");
            AddToDicAudio(AudioName.PANIC, "Sounds/audioPanic");
            AddToDicAudio(AudioName.SCREWDRIVER, "Sounds/audioScrewdriver");
            AddToDicAudio(AudioName.TOILET, "Sounds/audioToilet");
            AddToDicAudio(AudioName.TP, "Sounds/audioTP");
            AddToDicAudio(AudioName.VENTILATION, "Sounds/audioVentilation");
        }

        if (dic_musicID.Count <= 0 || (SoundManager.musicAudio == null || SoundManager.musicAudio.Count <= 0))
        {
            dic_musicID.Clear();
            AddToDicMusic(MusicName.SALARYMAN, "Sounds/Music/musicSalaryman");
            //AddToDicMusic(MusicName.NINJA, "Sounds/Music/audioMainMenu");
        }
    }

    private void AddToDicAudio(AudioName audioName, string fileName)
    {
        AudioClip ac = (AudioClip)Resources.Load(fileName);
        if(ac != null)
        {
            int i = SoundManager.PlaySound(ac);
            dic_audioID.Add(audioName, i);
            SoundManager.GetAudio(dic_audioID[audioName]).Stop();
        }
        else
        {
            Debug.LogError("Missing Audio " + fileName);
        }
    }

    private void AddToDicMusic(MusicName musicName, string fileName)
    {
        AudioClip ac = (AudioClip)Resources.Load(fileName);
        if (ac != null)
        {
            int i = SoundManager.PlayMusic(ac,1.0f,true,true);
            dic_musicID.Add(musicName, i);
            SoundManager.GetMusicAudio(dic_musicID[musicName]).Stop();
        }
        else
        {
            Debug.LogError("Missing Musique " + fileName);
        }
    }
}
