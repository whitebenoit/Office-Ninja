using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    RectTransform pauseMenuPanel;
    RectTransform itemPanel;
    List<RectTransform> itemListPanel = new List<RectTransform>();
    Button contBtn;
    Button quitBtn;

    int audioMenuIn;
    int audioMenuOut;

    public bool isPaused = false;

    //public enum ItemName { SCREWDRIVER, LAXATIVE, SHURIKEN, CALTROPS};
    //public enum ItemStateName { OWNED, MISSING, NOTIMPLEMENTEDYET};

    private static PauseController _instance = null;

    EventSystem eventSystem ;
    private static bool initialized = false;
    public static PauseController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PauseController)FindObjectOfType(typeof(PauseController));
                if (_instance == null)
                {
                    // Create gameObject and add component
                    _instance = (new GameObject("PauseControllerObj")).AddComponent<PauseController>();
                }
            }
            return _instance;
        }
    }


    /// <summary>
    /// The gameobject that the PauseController is attached to
    /// </summary>
    public static GameObject gameobject { get { return instance.gameObject; } }

    private void Awake()
    {
        instance.Init();

        FillProperties();

        UpdateItemDisplay();

        pauseMenuPanel.gameObject.SetActive(false);
    }

    private void FillProperties()
    {
        GameObject[] gO = GameObject.FindGameObjectsWithTag(Tags.pauseMenu);
        if(gO.Length > 0)
            pauseMenuPanel = gO[0].GetComponent<RectTransform>();
        RectTransform butPanel = null;
        quitBtn = contBtn = null;
        itemListPanel = new List<RectTransform>();
        foreach (RectTransform rectTrans in pauseMenuPanel.GetComponentsInChildren<RectTransform>(true))
        {
            if (rectTrans.gameObject.name == "ItemsPanel")
            {
                itemPanel = rectTrans;
                //Debug.Log("itemPanel: " + itemPanel.ToString());
            }
            if (rectTrans.gameObject.name == "ButtonsPanel")
            {
                butPanel = rectTrans;
            }
        }

        itemListPanel.AddRange(itemPanel.GetComponentsInChildren<RectTransform>(true));
        //itemListPanel = itemPanel.GetChild<RectTransform>();
        for (int i = 0; i < itemListPanel.Count; i++)
        {
            if (itemListPanel[i].name != "Panel")
            {
                itemListPanel.RemoveAt(i);
                i--;
            }
        }
        if (butPanel != null)
        {
            contBtn = butPanel.GetComponentsInChildren<Button>(true)[0];
            //contBtn.
            quitBtn = butPanel.GetComponentsInChildren<Button>(true)[1];
        }

        eventSystem = EventSystem.current;
    }
    

    public void Init()
    {
        FillProperties();
        if (!initialized)
        {
            initialized = true;
            GameMasterManager.DontDestroyOnLoad(this);
        }
    }


    private void Start()
    {
        audioMenuIn = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.MENU_IN];
        audioMenuOut = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.MENU_OUT];
    }

    public void TogglePause()
    {
        if (pauseMenuPanel == null)
            FillProperties();
        isPaused = pauseMenuPanel.gameObject.activeSelf;
        if (!isPaused)
        {
            OpenPause();
            SoundManager.GetAudio(audioMenuIn).Play();
        }
        else
        {
            ClosePause();
            SoundManager.GetAudio(audioMenuOut).Play();
        }
    }

    public void ClosePause()
    {
        if (pauseMenuPanel == null)
            FillProperties();
        if (pauseMenuPanel != null)
            pauseMenuPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }


    public void OpenPause()
    {
        pauseMenuPanel.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(contBtn.gameObject, new BaseEventData(eventSystem));
        Time.timeScale = 0;
    }


    public void NextButton()
    {
        Button currBtn = eventSystem.currentSelectedGameObject.GetComponent<Button>();
        if (currBtn == contBtn)
            eventSystem.SetSelectedGameObject(quitBtn.gameObject, new BaseEventData(eventSystem));
        else if (currBtn == quitBtn)
            eventSystem.SetSelectedGameObject(contBtn.gameObject, new BaseEventData(eventSystem));
    }

    public void ModifyItemDisplay(Dictionaries.ItemName itemName, Dictionaries.ItemStateName itemStateName)
    {
        RectTransform currItemPanel;
        switch (itemName)
        {
            case Dictionaries.ItemName.SCREWDRIVER:
                currItemPanel = itemListPanel[0];
                break;
            case Dictionaries.ItemName.LAXATIVE:
                currItemPanel = itemListPanel[1];
                break;
            case Dictionaries.ItemName.SHURIKEN:
                currItemPanel = itemListPanel[2];
                break;
            case Dictionaries.ItemName.CALTROPS:
                currItemPanel = itemListPanel[3];
                break;
            default:
                currItemPanel = itemListPanel[0];
                break;
        }

        Image currImage = currItemPanel.GetChild(0).GetComponent<Image>();
        Text currText = currItemPanel.GetChild(1).GetComponent<Text>();
        //Color nextColor;

        switch (itemStateName)
        {
            case Dictionaries.ItemStateName.OWNED:
                //currImage.color = new Color(0.8f, 0.8f, 0.8f);
                currImage.color = new Color(0.5f, 0.5f, 0.5f);
                currText.color = currImage.color;
                break;
            case Dictionaries.ItemStateName.MISSING:
                //currImage.color = new Color(1.0f, 1.0f, 1.0f);
                currImage.color = new Color(0.7f, 0.7f, 0.7f);
                currText.color = currImage.color;
                break;
            case Dictionaries.ItemStateName.NOTIMPLEMENTEDYET:
                //currImage.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                currImage.color = new Color(0.7f, 0.7f, 0.7f, 0.3f);
                currText.color = currImage.color;
                break;
            default:
                //currImage.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                currImage.color = new Color(0.7f, 0.7f, 0.7f, 0.3f);
                currText.color = currImage.color;
                break;
        }

        //currImage.color = nextColor;
        //currText.color = nextColor;
    }    

    public void UpdateItemDisplay(GameData gameData)
    {
        if (gameData.isScrewUnlocked)
        {
            ModifyItemDisplay(Dictionaries.ItemName.SCREWDRIVER, Dictionaries.ItemStateName.OWNED);
        }
        else
        {
            ModifyItemDisplay(Dictionaries.ItemName.SCREWDRIVER, Dictionaries.ItemStateName.MISSING);
        }
        if (gameData.isLaxaUnlocked)
        {
            ModifyItemDisplay(Dictionaries.ItemName.LAXATIVE, Dictionaries.ItemStateName.OWNED);
        }
        else
        {
            ModifyItemDisplay(Dictionaries.ItemName.LAXATIVE, Dictionaries.ItemStateName.MISSING);
        }

        ModifyItemDisplay(Dictionaries.ItemName.SHURIKEN, Dictionaries.ItemStateName.NOTIMPLEMENTEDYET);
        ModifyItemDisplay(Dictionaries.ItemName.CALTROPS, Dictionaries.ItemStateName.NOTIMPLEMENTEDYET);
    }

    public void UpdateItemDisplay()
    {
        UpdateItemDisplay(GameMasterManager.instance.gd_currentLevel);
    }

}
