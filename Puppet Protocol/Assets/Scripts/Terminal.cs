using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public InputField inputField;
    public Text log;
    private GlobalQueue queue;

    private const int MaxLines = 25;

    private int queueIndex = 0;
    private List<String> commandHistory;


    // Start is called before the first frame update
    void Start()
    {
        inputField.onValidateInput += checkForNewLine;
        queue = GameObject.Find("GlobalQueue").GetComponent<GlobalQueue>();
        commandHistory = new List<String>();
    }

    // Update is called once per frame
    void Update()
    {
        inputField.Select();

        var upPressed = Input.GetKeyDown(KeyCode.UpArrow);
        var downPressed = Input.GetKeyDown(KeyCode.DownArrow);

        if (!(upPressed ^ downPressed)) return; // Continue if one but not both are pressed

        if (upPressed)
        {
            queueIndex--;
            if (queueIndex < 0)
                queueIndex = 0;
            inputField.text = commandHistory[queueIndex];
        } else if (downPressed)
        {
            queueIndex++;
            if (queueIndex >= commandHistory.Count) // Back to the empty line
            {
                queueIndex = commandHistory.Count;
                inputField.text = "";
            } else
            {
                inputField.text = commandHistory[queueIndex];
            }
        }

    }

    public char checkForNewLine(string oldText, int charIndex, char addedChar)
    {
        if (addedChar == '\n')
        {
            pushCommand(oldText);
            inputField.text = "";
            return '\0';
        }
        return addedChar;
    }

    void pushCommand(string newCommand)
    {
        var newOutput = parseCommand(newCommand); // Try getting this from a factory or something
        int lines = log.text.Count(c => c == '\n');
        var additionalLines = newOutput.Count(c => c == '\n');

        if (lines + additionalLines >= MaxLines)
        {
            var allLines = (log.text + newOutput).Split('\n');
            string finalOutput = "";
            for (int i = allLines.Length - MaxLines; i < allLines.Length; i++)
            {
                finalOutput += allLines[i].Trim() + "\n";
            }
            log.text = finalOutput;
        } else
        {
            log.text += newOutput + "\n";
        }

        commandHistory.Add(newCommand);
        queueIndex = commandHistory.Count;
    }

    private string parseCommand(string commandText)
    {
        return queue.push(commandText);
    }

}
