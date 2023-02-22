using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSummons : SummonBase{
    protected override void summonUnits(){
        int rarity = base.chooseNewUnitsRarity();
        createUnit(rarity);
    }
}



