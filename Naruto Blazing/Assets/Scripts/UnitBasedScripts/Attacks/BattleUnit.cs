using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class BattleUnit : MonoBehaviour{
      
    public int turn {get; set;}
    public TextMeshProUGUI onScreenTurnText;
    private Vector3 newStartPosition;
    private Vector3 targetPosition;
    private CharacterBase currentPlayer;
    private GameObject camFollowObject;

    public ShadowClone createShadowClone(){
        ShadowClone shadowClone = Instantiate(this.GetComponent<ShadowClone>(), transform.position, Quaternion.identity);
        return shadowClone;
    }

    public void teleportToNewPosition(){
        currentPlayer.playAnim("jumpUp");
        transform.position = newStartPosition;
        currentPlayer.playAnim("jumpDown");
    }

    public void setNewStartPosition(Vector3 newStartPosition){
        this.newStartPosition = newStartPosition;
    }
    
    public void jutsuAttack(BattleUnit target){
        target.GetComponent<IStats>().decreaseHealth(this.GetComponent<IStats>().getJutsuDamage());
        CameraControler.transformCameraFocus = camFollowObject.transform;
        transform.position = target.transform.position;
        currentPlayer.playAnim("ninjutsu");
    }

    public void regularAttack(BattleUnit target, bool isLastUnitAttackComplete){
        targetPosition = target.transform.position;
        target.GetComponent<IStats>().decreaseHealth(this.GetComponent<IStats>().getAttackDamage());
        transform.position = targetPosition;
        currentPlayer.playAnim("attack");
        StartCoroutine(teleportToNewPositionAfterAttack(isLastUnitAttackComplete));
    }

    private IEnumerator teleportToNewPositionAfterAttack(bool isLastUnitAttackComplete){
        float animLen = currentPlayer.findAnimLen("attack");
        yield return new WaitForSeconds(animLen);
        
        currentPlayer.playAnim("jumpUp");
        Vector3 newPosition = newStartPosition;
        if(!isLastUnitAttackComplete){
            newPosition = targetPosition;
        }
        transform.position = newPosition;
        currentPlayer.playAnim("jumpDown");
    }

    private void Start() {
        currentPlayer = GetComponent<CharacterBase>();
        camFollowObject = transform.GetChild(3).gameObject;
        newStartPosition = transform.position;
        turn = UnityEngine.Random.Range(0,3);
    }
}
