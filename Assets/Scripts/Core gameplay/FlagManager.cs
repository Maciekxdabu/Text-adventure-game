using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [Tooltip("A list of flags for easier and more coherent choosing in other scripts (by default all flags are OFF)\n" +
        "This list does not matter in gameplay itis purely for Editor game setup")]
    [SerializeField] public string[] availableFlags;

    //A list of flags that are currently considered ON
    private static List<string> flagsOn = new List<string>();

    private GameController controller;

    // ---------- Unity messages

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    // ---------- public static methods

    public static bool AreFlagsMet(Flag[] flags)
    {
        foreach (Flag flag in flags)
        {
            bool flagOn = flagsOn.Contains(flag.flag);
            if (flag.whitelist != flagOn)//XNOR (checking if flag with whitelist is not met)
                return false;
        }

        //return true if all flags passed their checks
        return true;
    }

    public static void SetFlagValue(string flag, bool value)
    {
        if (value)//set ON
            if (flagsOn.Contains(flag) == false)
                flagsOn.Add(flag);
        else//set OFF
            if (flagsOn.Contains(flag))
                flagsOn.Remove(flag);
    }
}
