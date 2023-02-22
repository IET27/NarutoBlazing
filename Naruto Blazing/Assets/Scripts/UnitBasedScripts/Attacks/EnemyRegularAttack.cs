using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyRegularAttack : AttackBase{

    public override IEnumerator Attack(List<BattleUnit> detectedEnemies,Action onAttackComplete){
        float animLen;
        foreach (BattleUnit enemy in Enemies){
            if (enemy.turn == 0){  
                enemy.setNewStartPosition(detectedEnemies[0].gameObject.transform.position);
                enemy.teleportToNewPosition();
                // script is waiting for teleporting to be complete because
                // enemy first needs to teleport and then get the units to attack
                animLen = enemy.GetComponent<CharacterBase>().findAnimLen("jumpUp");
                yield return new WaitForSeconds(animLen);
                
                detectedEnemies = enemy.transform.GetChild(4).GetComponent<DetectEnemy>().getDetectedEnemies();
                int currentAttackedEnemyIndex = 0;

                foreach(BattleUnit detectedEnemy in detectedEnemies){
                    enemy.regularAttack(detectedEnemy, isLastEnemyAttacked(currentAttackedEnemyIndex,detectedEnemies));

                    animLen = enemy.GetComponent<CharacterBase>().findAnimLen("attack");
                    yield return new WaitForSeconds(animLen);
                    ++currentAttackedEnemyIndex;
                }
                
                enemy.setNewStartPosition(detectedEnemies[detectedEnemies.Count - 1].gameObject.transform.position);
                enemy.turn = UnityEngine.Random.Range(0, 3);
            }else{
                enemy.turn--;
            }
            enemy.transform.GetChild(4).GetComponent<DetectEnemy>().clearDetectedEnemies();
        }
        onAttackComplete();
    }
    
    
}
