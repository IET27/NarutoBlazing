using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShadowClone : MonoBehaviour{

    private Vector3 mousePosition;
    private Rigidbody2D _rigidBody;
    public GameObject range;
    private float moveSpeed = 100f;
    private CharacterBase characterBase;
    
    private void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        range = transform.GetChild(transform.childCount - 1).gameObject;
        characterBase = GetComponent<CharacterBase>();
    }

    public void stopJutsuFocus(){
        characterBase.playAnim("idle");
    }

    public void playJutsuFocus(){
        characterBase.playAnim("focus");
    }

    public void move(){
        // calculate Shadow Clone position to move based on mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        _rigidBody.velocity = new Vector2(direction.x * moveSpeed,direction.y * moveSpeed);
        characterBase.moveAnim();
    }

    public void stopMoving(){
        _rigidBody.velocity = Vector2.zero;
        foreach(BattleUnit bu in range.GetComponent<DetectEnemy>().getDetectedEnemies()){
            bu.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        Destroy(gameObject,0.01f);
    }

    public void showRange(){
        range.SetActive(true);
    }

    
}
