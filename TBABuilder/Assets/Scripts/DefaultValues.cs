﻿using UnityEngine;

public class DefaultValues : MonoBehaviour
{
    [Tooltip("Which commands are usable in-game")]
    [Header("Activated Commands")]
    [SerializeField] public bool eatActive = true;
    [SerializeField] public bool drinkActive = true;
    [SerializeField] public bool talkActive = true;
    [SerializeField] public bool killActive = true;
    [SerializeField] public bool breakActive = true;
    [SerializeField] public bool sitActive = true;
    [SerializeField] public bool useActive = true;
    [SerializeField] public bool pickupActive = true;
    [SerializeField] public bool wearActive = true;
    [SerializeField] public bool openActive = true;
    [SerializeField] public bool lookAtActive = true;

    [Tooltip("The default flavor texts of generic actions")]
    [Header("Default Action Texts")]

    [SerializeField] public string edibleSuccessText;
    [SerializeField] public string edibleFailText;
    [SerializeField] public string drinkableSuccessText;
    [SerializeField] public string drinkableFailText;
    [SerializeField] public string talkableSuccessText;
    [SerializeField] public string talkableFailText;
    [SerializeField] public string killableSuccessText;
    [SerializeField] public string killableFailText;
    [SerializeField] public string breakableSuccessText;
    [SerializeField] public string breakableFailText;
    [SerializeField] public string sittableSuccessText;
    [SerializeField] public string sittableFailText;
    [SerializeField] public string usableSuccessText;
    [SerializeField] public string usableFailText;
    [SerializeField] public string pickupableSuccessText;
    [SerializeField] public string pickupableFailText;
    [SerializeField] public string wearableSuccessText;
    [SerializeField] public string wearableFailText;
    [SerializeField] public string openableSuccessText;
    [SerializeField] public string openableFailText;

    [SerializeField] public string lookAtDefaultText;

    [Tooltip("Other default flavor texts of generic actions")]
    [Header("Other Action Texts")]

    [SerializeField] public string edibleNotFoundText;
    [SerializeField] public string drinkableNotFoundText;
    [SerializeField] public string talkableNotFoundText;
    [SerializeField] public string killableNotFoundText;
    [SerializeField] public string breakableNotFoundText;
    [SerializeField] public string sittableNotFoundText;
    [SerializeField] public string usableNotFoundText;
    [SerializeField] public string pickupableNotFoundText;
    [SerializeField] public string wearableNotFoundText;
    [SerializeField] public string openableNotFoundText;
    [SerializeField] public string lookAtNotFoundText;

    [Tooltip("Miscellaneous default texts in the game")]
    [Header("Misc Texts")]

    [SerializeField] public string winText;
    [SerializeField] public string deathText;
    [SerializeField] public string emptyInventoryText;
    [SerializeField] public string occupiedInventoryText;
    [SerializeField] public string emptyEquipmentText;
    [SerializeField] public string occupiedEquipmentText;
    [SerializeField] public string roomNotFoundText;
    [SerializeField] public string unknownCommand;
    [TextArea(1, 5)] [SerializeField] public string HelpText;
}
