using System;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public KeyManager[] keyManager;
    public LeverManager[] leverManager;

    public KeyType GetKeyType(GatesNo gateno_)
    {
        KeyManager keys = Array.Find(keyManager, i => i.gateNo == gateno_);
        return keys.key;
    }

    public GatesNo GetGateNo(LeverType leverType)
    {
        LeverManager lever = Array.Find(leverManager, i => i.lever == leverType);
        return lever.gateNo ;
    }
}
[Serializable]
public class KeyManager
{
    public KeyType key;
    public GatesNo gateNo;
}
[Serializable]
public class LeverManager
{
    public LeverType lever;
    public GatesNo gateNo;
}
public enum KeyType
{
    Key_Gate0,
    Key_Gate1,
    Key_Gate2
}
public enum LeverType
{
    Lever_Gate2,
    Lever_Gate6
}