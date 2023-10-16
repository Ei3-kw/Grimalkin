/* 
 * Project Grimalkin
 * Author: Fahed Alhanaee
 * 
 * Purpose:
 * - store information for the observer class to retrieve  
 *   
 * Attached to objects in game scene:
 * - any object the is "observable" i.e. that we can 
 *   record that the user is looking at it and keep track that the user has looked at it
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observable : MonoBehaviour
{
    // a list of topics the this object might represent 
    public List<string> info;
}
