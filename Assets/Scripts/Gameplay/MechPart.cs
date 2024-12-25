using UnityEngine;

public enum PartType
{
    Core,
    Armor,
    Thruster,
    Storage,
    Weapon,
    Internal
}
public enum PartStatus
{
    OK,
    Damaged,
    Destroyed
}

public class MechPart
{
    public string mPartName { get; private set; }
    public PartType mType { get; private set; }

    public MechPart()
    {
    }


}
