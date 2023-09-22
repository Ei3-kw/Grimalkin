using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public enum ObservableType {LARGE ,MED, SMALL}; 
public class observable : MonoBehaviour
{
    public ObservableType type;
    public List<string> info;
}
