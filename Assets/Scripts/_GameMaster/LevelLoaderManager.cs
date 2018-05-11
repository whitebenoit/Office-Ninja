using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderManager : MonoBehaviour {

    public GameMasterManager gMM;

    private void Awake()
    {
        gMM = GameMasterManager.Instance;
        
    }

    private void Start()
    {
        gMM.LevelSetUp();
    }
}
