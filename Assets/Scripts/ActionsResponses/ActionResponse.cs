using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject
{
    //response requirement (currently just checking for player in a room)
    public string requiredString;
    [TextArea, Tooltip("A result that will be displayed when the Action is successfull")]
    public string successResponse;
    public abstract bool DoActionResponse(GameController controller);
}
