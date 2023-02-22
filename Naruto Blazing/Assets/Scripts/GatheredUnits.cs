using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheredUnits{
    private static GatheredUnits instance;
    public List<Sprite> gatheredUnits = new List<Sprite>();
    //TODO: set gatheredUnits list to private and make functions needed for it to work
    private GatheredUnits(){}
    public static GatheredUnits getInstance(){
        if(instance == null){
            instance = new GatheredUnits();
        }
        return instance;
    }

}
