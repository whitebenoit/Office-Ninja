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
        //Debug.Log("PARENT:" + this.gameObject.ToString() + "/" + this.gameObject.transform.position);
        Invoke("Son", 0.75f);
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(this.gameObject, new BaseEventData(eventSystem));
    }

    private void Son()
    {
        audioMainMenu = Dictionaries.instance.dic_audioID[Dictionaries.AudioName.MAIN_MENU];
        SoundManager.GetAudio(audioMainMenu).Play();
    }
	
}
