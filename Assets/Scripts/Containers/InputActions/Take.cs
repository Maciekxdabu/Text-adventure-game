using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Take", fileName = "Take")]
public class Take : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

        if (takeDictionary != null)
        {
            controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));

            //AudioEvent
            controller.interactableItems.TryRunAudioEvent(separatedInputWords);

            //Rotate towards
            controller.roomNavigation.RotateListenerTowards(separatedInputWords[1]);
        }
    }
}
