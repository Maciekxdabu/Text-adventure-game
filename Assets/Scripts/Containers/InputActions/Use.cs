using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Use", fileName = "Use")]
public class Use : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        //use Action does not Log a string so an interaction must deal with it
        controller.interactableItems.UseItem(separatedInputWords);
    }
}
