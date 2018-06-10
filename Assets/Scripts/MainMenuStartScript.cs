using EazyTools.SoundManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuStartScript : MonoBehaviour {

    EventSystem eventSystem;
    int audioMainMenu;

    // Use this for initialization
    void Start ()
    {
        audioMainMenu = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.MAIN_MENU];
        SoundManager.GetAudio(audioMainMenu).Play();
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(this.gameObject, new BaseEventData(eventSystem));
    }
	
}
