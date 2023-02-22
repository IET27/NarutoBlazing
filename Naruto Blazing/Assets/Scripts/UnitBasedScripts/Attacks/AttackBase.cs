using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour{

    protected static List<BattleUnit> Enemies = new List<BattleUnit>(10);
    [SerializeField]
    protected GameObject deathAnim;
    protected static BattleUnit activeUnit;
    protected static ChakraBarActivator chakraBarActivator;
    private static LayerSorting layerSorting = LayerSorting.getInstance();

    public virtual IEnumerator Attack(List<BattleUnit> detectedEnemies,Action onAttackComplete) { yield return -1; }

    public void setActiveUnit(BattleUnit newActiveUnit){
        activeUnit = newActiveUnit;
    }

    public void setChakraBarActivator(ChakraBarActivator newChakraBarActivator){
        chakraBarActivator = newChakraBarActivator;
    }

    public void addEnemyToEnemies(BattleUnit enemy){
        Enemies.Add(enemy);
    }

    protected void clearDeadEnemies(List<BattleUnit> deadEnemies){
        for(int i=0;i<deadEnemies.Count;++i){
            EnemyStats enemy = deadEnemies[i].GetComponent<EnemyStats>();
            if(enemy.isDead()){
                GameObject deathAnimGO = Instantiate(deathAnim,deadEnemies[i].transform.position,Quaternion.identity);
                StartCoroutine(deathAnimGO.GetComponent<DeathAndSpawnAnimHandler>().playDeathAnim());
                Enemies.Remove(deadEnemies[i]);
                layerSorting.removeCharacterFromCharacters(enemy.gameObject);
                Destroy(enemy.gameObject);
            }
        }
    }

    protected bool isLastEnemyAttacked(int n,List<BattleUnit> detectedEnemies){
        return n+1 == detectedEnemies.Count;
    }

}
