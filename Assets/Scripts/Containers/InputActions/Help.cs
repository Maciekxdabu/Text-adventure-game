using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Help", fileName = "Help")]
public class Help : InputAction
{
    [TextArea]
    public string helpMessage;

    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.LogStringWithReturn(helpMessage);
    }
}
