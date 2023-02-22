using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRegularAttack : AttackBase{

    public override IEnumerator Attack(List<BattleUnit> detectedEnemies,Action onAttackComplete){
        List<BattleUnit> deadEnemies = new List<BattleUnit>();
        
        for(int i=0;i<detectedEnemies.Count;++i){
            
            activeUnit.regularAttack(detectedEnemies[i],isLastEnemyAttacked(i,detectedEnemies));
            
            float animLen = activeUnit.GetComponent<CharacterBase>().findAnimLen("attack");
            yield return new WaitForSeconds(animLen);
            
            if(detectedEnemies[i].GetComponent<EnemyStats>().isDead()){
                deadEnemies.Add(detectedEnemies[i]);
            }
        }

        clearDeadEnemies(deadEnemies);
        chakraBarActivator.stopActiveAnim();
        onAttackComplete();
    }
}
