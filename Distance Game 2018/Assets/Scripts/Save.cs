using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int Money { get; set; }
    public int GravityUpgrade { get; set; }
    public int SlowDownUpgrade { get; set; }
    public int MoveSpeedUpgrade { get; set; }
    public int BounceUpgrade { get; set; }
    public int SpeedUpUpgrade { get; set; }
    public bool SeenTutorial { get; set; }
}
