using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSummons : SummonBase{
    private const int MULTI_SUMMONS_RECEIVED_UNITS_NUMBER = 10;
    protected override void summonUnits(){
        for(int i=0;i<MULTI_SUMMONS_RECEIVED_UNITS_NUMBER;++i){
            int rarity = chooseNewUnitsRarity();
            createUnit(rarity);
        }
    }
}
