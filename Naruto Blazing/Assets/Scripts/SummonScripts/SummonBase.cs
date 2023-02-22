using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBase : MonoBehaviour{
    private static List<Sprite> summonedUnits;
    private static List<int> summonedUnitsRarityID;
    private SummonableUnits summonableUnits;

    private const int fiveStarID = 5, fourStarID = 4, threeStarID = 3;

    public void startUnitSummoning(){
        summonableUnits = GameObject.Find("SummonableUnits").GetComponent<SummonableUnits>();
        summonedUnitsRarityID = new List<int>();
        summonedUnits = new List<Sprite>();
        summonUnits();
    }

    protected virtual void summonUnits(){}

    protected int chooseNewUnitsRarity(){
        int rarity = Random.Range(0,100);
        return rarity;
    }

    protected void createUnit(int rarity){
        int summonedUnitIndex;
        if(rarity >= 0 && rarity <= 11){
            summonedUnitIndex = Random.Range(0,summonableUnits.fiveStarUnits.Count - 1);
            summonedUnits.Add(summonableUnits.fiveStarUnits[summonedUnitIndex]);
            summonedUnitsRarityID.Add(fiveStarID);
        }else if(rarity >= 12 && rarity <= 62){
            summonedUnitIndex = Random.Range(0,summonableUnits.fourStarUnits.Count - 1);
            summonedUnits.Add(summonableUnits.fourStarUnits[summonedUnitIndex]);
            summonedUnitsRarityID.Add(fourStarID);
        }else{
            summonedUnitIndex = Random.Range(0,summonableUnits.threeStarUnits.Count - 1);
            summonedUnits.Add(summonableUnits.threeStarUnits[summonedUnitIndex]);
            summonedUnitsRarityID.Add(threeStarID);
        }
    }

    public Sprite getSummonedUnit(int summonedUnitIndex){
        return summonedUnits[summonedUnitIndex];
    }

    public int getSummonedUnitRarityID(int summonedUnitIndex){
        return summonedUnitsRarityID[summonedUnitIndex];
    }

    public int getNumberOfSummonedUnits(){
        return summonedUnits.Count;
    }

}
