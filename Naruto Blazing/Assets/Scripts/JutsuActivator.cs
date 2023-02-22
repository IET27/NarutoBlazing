using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JutsuActivator : MonoBehaviour{

    [SerializeField]
    private List<ChakraBarActivator> bottomUI;

    public void activateChackraBar(BattleUnit activeUnit){
        ChakraBarActivator chakraBarActivator = null;

        foreach (ChakraBarActivator chakrBarAct in bottomUI){
            if (activeUnit.gameObject == chakrBarAct.getUnit()){
                chakrBarAct.activateChakraBar();
                chakrBarAct.playActiveAnim();
                chakraBarActivator = chakrBarAct;
                break;
            }
        }
        
        GetComponent<AttackBase>().setChakraBarActivator(chakraBarActivator);
    }
    
    public bool canJutsuBeActivated(BattleUnit activeUnit){
        Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);
        bool isJutsuActivated = false;

        if(cubeHit.transform.gameObject != null && cubeHit.transform.gameObject.tag == "UnitIcon"){
            ChakraBarActivator s = cubeHit.transform.gameObject.GetComponent<ChakraBarActivator>();

            if(s.getUnit() == activeUnit.gameObject){
                isJutsuActivated = true;
            }
        }
        return isJutsuActivated;
    }

    public bool isJutsuActivated(AttackBase characterAttack){
        return characterAttack as UnitJutsuAttack;
    }
}
