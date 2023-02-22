using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitJutsuAttack : AttackBase{

    public override IEnumerator Attack(List<BattleUnit> detectedEnemies,Action onAttackComplete){
        List<BattleUnit> deadEnemies = new List<BattleUnit>(); 

        if(detectedEnemies.Count > 0){
            activeUnit.jutsuAttack(detectedEnemies[0]);
            if(detectedEnemies[0].GetComponent<EnemyStats>().isDead())deadEnemies.Add(detectedEnemies[0]);
        
            float animLen = activeUnit.GetComponent<CharacterBase>().findAnimLen("ninjutsu");
            yield return new WaitForSeconds(animLen);
        
            chakraBarActivator.deactivateUsedChakraBars();
        }
        
        clearDeadEnemies(deadEnemies);
        CameraControler.transformCameraFocus = null;
        chakraBarActivator.stopActiveAnim();
        onAttackComplete();
    }
}

