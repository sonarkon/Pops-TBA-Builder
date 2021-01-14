using UnityEngine;
using System;
using extOSC;


public class RoomTracker : MonoBehaviour
{
    #region Private Vars
        public OSCTransmitter globalTransmitter; // add the global transmitter from project
		public string myOscAddress = "/myOscLabel"; // to be set in the project
        public int cueID; // cue to send, to be set in the project
    #endregion

    [SerializeField] Room startingRoom = null;
    Room currentRoom = null;
    TextPrompt textPrompt;
    DefaultValues defaultValues;
    ActionHandler actionHandler;
    private void Start()
    {
        textPrompt = FindObjectOfType<TextPrompt>();
        defaultValues = FindObjectOfType<DefaultValues>();
        actionHandler = FindObjectOfType<ActionHandler>();

        currentRoom = startingRoom;
        currentRoom.initializeRuntimeVariables();
        printCurrentRoomEntryText();
        actionHandler.executeActions(currentRoom, currentRoom.roomEntryActions);
    }

    public void printCurrentLookText()
    {
        string lookText = currentRoom.runtimeLookText;

        if (System.String.IsNullOrEmpty(lookText))
        {
            textPrompt.printText("(You forgot to add Room Look Text for this room)");
        }
        else
        {
            textPrompt.printText(lookText);
        }
    }

    public void printCurrentRoomEntryText()
    {
        string roomText = currentRoom.runtimeRoomEntryText;

        if (System.String.IsNullOrEmpty(roomText))
        {
            textPrompt.printText("(You forgot to add Room Entry Text for this room)");
        }
        else
        {
            textPrompt.printText(roomText);
        }
    }

    public void printCurrentRoomEntryText(float seconds)
    {
        string roomText = currentRoom.runtimeRoomEntryText;

        if (System.String.IsNullOrEmpty(roomText))
        {
            StartCoroutine(textPrompt.printTextAfterDelay("(You forgot to add Room Entry Text for this room)", seconds));
        }
        else
        {
            StartCoroutine(textPrompt.printTextAfterDelay(roomText, seconds));
        }
    }

    public void printCurrentRoomInactiveText(Room.RoomConnectionVars roomConnection)
    {
        string roomText = roomConnection.roomInactiveText;

        if (System.String.IsNullOrEmpty(roomText))
        {
            textPrompt.printText("(You forgot to add Room Inactive Text for this room's connection)");
        }
        else
        {
            textPrompt.printText(roomText);
        }
    }

    public Room getCurrentRoom()
    {
        return currentRoom;
    }

    public void forceChangeRoom(Room room)
    {
        if (room == null)
        {
            Debug.Log("You forgot to add a target room to move in this action.");
            return;
        }

        if (room.isInitialized == false)
            room.initializeRuntimeVariables();

        currentRoom = room;

        // Is delayed so the flavor text can be printed before room text.
        actionHandler.executeActions(currentRoom, currentRoom.roomEntryActions);
        printCurrentRoomEntryText(0.1f);
    }

    public void changeRoomViaRoomConnection(string userInput)
    {
        Room newRoom = findRoomConnection(userInput);
		var message = new OSCMessage(myOscAddress);

        if (newRoom == null)
            return;

        if (newRoom.isInitialized == false)
            newRoom.initializeRuntimeVariables();

        string raumFull = newRoom.ToString();
        string raum_short = raumFull.Substring(0, raumFull.Length-6);
        message.AddValue(OSCValue.String(raum_short));
        globalTransmitter.Send(message); // the global OSC Manager's transmitter sends message
        Debug.Log("from: " + currentRoom + "going to " + raum_short);

        currentRoom = newRoom;
        actionHandler.executeActions(currentRoom, currentRoom.roomEntryActions);
        printCurrentRoomEntryText();
        
    }

    private Room findRoomConnection(string userInput)
    {
        Room newRoom = null;

        foreach (Room.RoomConnectionVars roomConnection in currentRoom.roomConnections)
        {
            // No aliases
            if (string.IsNullOrWhiteSpace(roomConnection.roomAlias))
            {
                if (userInput.ToLower().Contains(roomConnection.room.name.ToLower()))
                {
                    // If room inactive
                    if (!roomConnection.runtimeIsActive)
                    {
                        printCurrentRoomInactiveText(roomConnection);
                        return null;
                    }
                    newRoom = roomConnection.room;
                    break;
                }
            }

            // Has aliases
            else
            {
                string[] roomAliases = roomConnection.roomAlias.Split(',');
                foreach (string roomAlias in roomAliases)
                {
                    if (userInput.ToLower().Contains(roomAlias.ToLower()))
                    {
                        // If room inactive
                        if (!roomConnection.runtimeIsActive)
                        {
                            printCurrentRoomInactiveText(roomConnection);
                            return null;
                        }
                        newRoom = roomConnection.room;
                        break;
                    }
                }
            }

            if (newRoom != null)
            {
                break;
            }
        }

        if (newRoom == null)
        {
            textPrompt.printText(defaultValues.roomNotFoundText);
        }

        return newRoom;
    }
}
