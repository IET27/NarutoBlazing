using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonType : MonoBehaviour{
    
    private static string summonType;

    public void setSummonType(string type){
        summonType = type;
    }

    public void summonUnits(){
        SummonBase summonBase = (summonType == "multi")?GetComponent<MultiSummons>():GetComponent<SingleSummons>();
        summonBase.startUnitSummoning();
    }


}
