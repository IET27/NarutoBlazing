using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStart : MonoBehaviour{
    
    private LayerSorting layerSorting;
    [SerializeField]
    private List<GameObject> unitsToSpawn, enemiesToSpawn;
    [SerializeField]
    private Animator spawnAnim;
    [SerializeField]
    private List<ChakraBarActivator> bottomUI;
    private Vector3 unitSpawnPosition = new Vector3(-7.75f,2.46f,0); 
    private Vector3 enemySpawnPosition = new Vector3(-3.2f,1.6f,0);
    private IEnumerator Start() {
        int i = 0;
        layerSorting = LayerSorting.getInstance();
        foreach(var unit in unitsToSpawn){
            GameObject spawnAnimGO = Instantiate(spawnAnim.gameObject,unitSpawnPosition,Quaternion.identity);
            spawnAnimGO.GetComponent<Animator>().Play("unitSpawnInSmoke");
            
            float animLen = spawnAnim.runtimeAnimatorController.animationClips[0].length;
            yield return new WaitForSeconds(animLen/2);

            GameObject spawnedUnit = Instantiate(unit,unitSpawnPosition,Quaternion.identity);
            unitSpawnPosition.y -= 1.5f;

            yield return new WaitForSeconds(animLen/2);
            Destroy(spawnAnimGO);
            
            bottomUI[i++].setUnit(spawnedUnit);
            GetComponent<Mission1Handler>().addUnitToUnits(spawnedUnit.GetComponent<BattleUnit>());
            layerSorting.addCharacterToCharacters(spawnedUnit);
        }

        foreach(var enemy in enemiesToSpawn){
            GameObject spawnAnimGO = Instantiate(spawnAnim.gameObject,enemySpawnPosition,Quaternion.identity);
            spawnAnimGO.GetComponent<Animator>().Play("unitSpawnInSmoke");
            
            float animLen = spawnAnim.runtimeAnimatorController.animationClips[0].length;
            yield return new WaitForSeconds(animLen/2);
            
            GameObject spawnedUnit = Instantiate(enemy,enemySpawnPosition,Quaternion.identity);
            enemySpawnPosition.y -= 1.6f;

            yield return new WaitForSeconds(animLen/2);
            Destroy(spawnAnimGO);

            GetComponent<AttackBase>().addEnemyToEnemies(spawnedUnit.GetComponent<BattleUnit>());
            layerSorting.addCharacterToCharacters(spawnedUnit);
        }
        GetComponent<Mission1Handler>().startMission();
    }


}
