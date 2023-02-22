using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour{

    private List<BattleUnit> detectedEnemies;
    [SerializeField]
    private string enemyTag;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == enemyTag){
            detectedEnemies.Add(other.gameObject.GetComponent<BattleUnit>());
            if(Input.GetMouseButton(0) && enemyTag != "Team"){
                other.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(detectedEnemies.Contains(other.gameObject.GetComponent<BattleUnit>())){
            detectedEnemies.Remove(other.gameObject.GetComponent<BattleUnit>());
            other.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    public List<BattleUnit> getDetectedEnemies(){
        return detectedEnemies;
    }

    public void clearDetectedEnemies(){
        detectedEnemies.Clear();
    }

    private void Start(){
        detectedEnemies = new List<BattleUnit>();
    }

    
}
