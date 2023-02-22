using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IStats{
    private int health;
    private int attackDamage;
    private int jutsuDamage;

    private void Awake() {
        attackDamage = 100;
        jutsuDamage = 500;
        health = 700;
    }

    public int getAttackDamage(){
        return attackDamage;
    }

    public int getJutsuDamage(){
        return jutsuDamage;
    }

    public int getHealth(){
        return health;
    }

    public void decreaseHealth(int damage){
        health-=damage;
    }
}
