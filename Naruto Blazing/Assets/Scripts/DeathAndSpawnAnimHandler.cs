using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndSpawnAnimHandler : MonoBehaviour{

    public IEnumerator playDeathAnim(){
        Animator a = GetComponent<Animator>();
        a.Play("unitSpawnInSmoke");
        float animLen = a.runtimeAnimatorController.animationClips[0].length;
        yield return new WaitForSeconds(animLen);
        Destroy(gameObject);
    }

}
