using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [NonSerialized] public int amountOfMoney = 0;
    [NonSerialized] public int amountOfFood = 0;
}
