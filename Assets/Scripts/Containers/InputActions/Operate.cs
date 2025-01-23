using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Operate", fileName = "Operate")]
public class Operate : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        //operate InputAction Logs a string by its ActionResponse
        controller.interactableItems.OperateItem(separatedInputWords);

        //AudioEvent
        controller.interactableItems.TryRunAudioEvent(separatedInputWords);

        //Rotate towards
        controller.roomNavigation.RotateListenerTowards(separatedInputWords[1]);
    }
}
