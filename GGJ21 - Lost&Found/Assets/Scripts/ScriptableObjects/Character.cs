using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "NPC/Character")]
public class Character : ScriptableObject
{
    public new string name;
    public Item lostObject;
}
