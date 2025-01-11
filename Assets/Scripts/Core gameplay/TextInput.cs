using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private GameController controller;

    // ---------- Unity methods

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    // ---------- private methods

    private void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);

        char[] delimeterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimeterCharacters);

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }

        InputComplete();
    }

    private void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
