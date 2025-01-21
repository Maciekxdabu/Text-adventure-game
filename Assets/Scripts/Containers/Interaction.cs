using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction
{
    public InputAction inputAction;
    public Flag[] flags;
    [TextArea, Tooltip("Text Response when flags met (does not work with \"use\" and \"inventory\")")]
    public string textResponse;
    [TextArea, Tooltip("Text Response when flags not met (does not work with \"use\" and \"inventory\")")]
    public string failedTextResponse;
    public ActionResponse actionResponse;

    // ---------- public methods

    public string GetResponseBasedOnFlags()
    {
        if (FlagManager.AreFlagsMet(flags))
            return textResponse;
        else
            return failedTextResponse;
    }
}
