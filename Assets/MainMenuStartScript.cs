using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuStartScript : MonoBehaviour {

    EventSystem eventSystem;

    // Use this for initialization
    void Start () {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(this.gameObject, new BaseEventData(eventSystem));
    }
	
}
