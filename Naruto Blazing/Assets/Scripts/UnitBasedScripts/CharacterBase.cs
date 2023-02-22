using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBase : MonoBehaviour{
    
    private Animator unitAnimations;
    
    private void Awake() {
        unitAnimations = GetComponent<Animator>();
    }

    public void stopAnim(){
        unitAnimations.speed = 0;
    }

    public float findAnimLen(string animName){
        foreach(AnimationClip clip in unitAnimations.runtimeAnimatorController.animationClips){
            if(clip.name == animName){
                return clip.length;
            }
        }
        return 0;
    }

    public void playAnim(string animation){
        unitAnimations.speed = 1;
        unitAnimations.Play(animation,0,0f);
    }

    public void moveAnim(){
        unitAnimations.Play("jumpUp");
    }
}
