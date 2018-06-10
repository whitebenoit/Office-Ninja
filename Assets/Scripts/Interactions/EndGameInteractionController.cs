using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameInteractionController : ObjectInteractionController {
    public float fadeInDuration = 5;
    public string text = "Merci d'avoir joué";

    public override void Interaction(ObjectInteractionController oicCaller, Collider other)
    {
        FadeInOutController.instance.FadeIn(() =>
        {
            SceneManager.LoadScene(0);
        }, fadeInDuration, text);
    }

    protected override void ModifiedMove(Vector3 direction, ObjectInteractionController oicCaller, Collider other)
    {
        throw new System.NotImplementedException();
    }
    
}
