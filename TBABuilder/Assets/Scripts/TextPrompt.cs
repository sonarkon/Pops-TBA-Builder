﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;

public class TextPrompt : MonoBehaviour
{
    [SerializeField] float typeSpeed = 0.02f;

    // Cached Variables
    TextMeshProUGUI textComponent;
    TMP_InputField inputField;
    InputParser inputParser;
    RoomTracker roomTracker;
    ActionHandler actionHandler;

    // Helper variables
    private bool isPrinting = false;
    private bool isDead = false;
    private Queue<string> printQueue;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = "";
        inputField = FindObjectOfType<TMP_InputField>();
        inputParser = FindObjectOfType<InputParser>();
        roomTracker = FindObjectOfType<RoomTracker>();
        actionHandler = FindObjectOfType<ActionHandler>();

        printQueue = new Queue<string>();
        
        inputField.ActivateInputField();
        actionHandler.printCurrentRoomText();
    }

    void Update()
    {
        if (isDead && !isPrinting && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrWhiteSpace(inputField.text) && !isDead)
        {
            // Input text
            inputField.ActivateInputField();
            string userInput = inputField.text;
            inputField.text = "";
            printText("\n\n> " + userInput);
            inputParser.parseInput(userInput);
        }
        else
        {
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isDead)
        {
            inputField.ActivateInputField();
        }
    }

    public void printText(string text)
    {
        printQueue.Enqueue(text);
        if (!isPrinting)
        {
            StartCoroutine(printQueuedText());
        }
    }

    public void killPlayer()
    {
        isDead = true;
        // For some reason inputfield doesn't deactivate so I just moved it off-screen lmao
        inputField.transform.position = new Vector2(0, -100);
    }

    private IEnumerator printTextCoroutine(string text)
    {
        int textLength = text.Length;
        StringBuilder printedText = new StringBuilder(textComponent.text);
        for (int i = 0; i < textLength; i++)
        {
            yield return new WaitForSeconds(typeSpeed);
            printedText.Append(text[i]);
            updateText(printedText);
        }
    }

    private void updateText(StringBuilder textBuilder)
    {
        textComponent.text = textBuilder.ToString();
    }

    private IEnumerator printQueuedText()
    {
        isPrinting = true;
        while (printQueue.Count != 0)
        {
            string currentText = printQueue.Dequeue();
            yield return StartCoroutine(printTextCoroutine(currentText));
        }
        isPrinting = false;
    }
}