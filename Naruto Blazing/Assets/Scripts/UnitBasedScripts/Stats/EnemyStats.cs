using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats  : MonoBehaviour, IStats{
    
    public int health;
    private int attackDamage;
    private int jutsuDamage;

    private void Start() {
        health = 100;
        attackDamage = 300;
    }

    public void decreaseHealth(int damage){
        health-=damage;
    }

    public bool isDead(){
        return health<=0;
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
}
