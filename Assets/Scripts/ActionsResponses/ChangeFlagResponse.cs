using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/ActionReponses/ChangeFlag", fileName = "ChangeFlag")]
public class ChangeFlagResponse : ActionResponse
{
    public string flagToChange;
    public bool newFlagState;

    public override bool DoActionResponse(GameController controller)
    {
        if (requiredString == controller.roomNavigation.currentRoom.roomName)
        {
            FlagManager.SetFlagValue(flagToChange, newFlagState);

            return true;
        }
        else
            return false;
    }
}
