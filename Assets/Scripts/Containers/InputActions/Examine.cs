using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Examine", fileName = "Examine")]
public class Examine : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(controller.interactableItems.examineDictionary, separatedInputWords[0], separatedInputWords[1]));

        //AudioEvent
        controller.interactableItems.TryRunAudioEvent(separatedInputWords);
    }
}
