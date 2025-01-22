using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Use", fileName = "Use")]
public class Use : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        //use InputAction Logs a string by its ActionResponse
        controller.interactableItems.UseItem(separatedInputWords);
    }
}
