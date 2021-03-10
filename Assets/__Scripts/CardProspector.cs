﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// An enum defines a variable type with a few prenamed values
public enum eCardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector : Card
{
    [Header("Set Dynamically:CardProspector")]
    // This is how you use the enum eCardState
    public eCardState state = eCardState.drawpile;
    public List<CardProspector> hiddenBy = new List<CardProspector>();
    public int layoutID;
    public SlotDef slotDef;
}
