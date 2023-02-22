using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mission1Handler : MonoBehaviour{

    private const int UNITS_ON_SCREEN = 3;
    private int  activatedUnitCount = 0, health;
    private List<BattleUnit> Units = new List<BattleUnit>(UNITS_ON_SCREEN);
    private AttackBase characterAttack;
    private JutsuActivator jutsuActivator;
    private BattleUnit activeUnit;
    private ShadowClone shadowClone;
    private LayerSorting layerSorting = LayerSorting.getInstance();
    private State state = State.WaitingForPlayer;
    private bool canStart = false, canChangeUnit = false;
    [SerializeField]
    private HealthBar healthBar;

    private void Update(){
        if(!canStart)return;
        if (state == State.WaitingForPlayer){
            if (shadowClone == null){
                createShadowCloneAndActivateChakraBar();

            }else if(Input.GetMouseButtonDown(0)){
                activateJutsuIfJutsuCanBeActivated();

            }else if(Input.GetMouseButton(0) && Input.mousePosition.y > 375){
                shadowClone.move(); 
                canChangeUnit = true;
            }else if(Input.GetMouseButtonUp(0) && canChangeUnit){
                state = State.Busy;         

                shadowClone.stopMoving();
                canChangeUnit = false;

                // set new starting position of active unit to last position of shadow clone
                activeUnit.setNewStartPosition(shadowClone.gameObject.transform.position);
                List<BattleUnit> detectedEnemies = shadowClone.range.GetComponent<DetectEnemy>().getDetectedEnemies();
                    
                StartCoroutine(characterAttack.Attack(detectedEnemies,()=>{
                    activeUnit.teleportToNewPosition();
                    layerSorting.reorderOrderInLayer();
                    chooseNextActiveUnit();
                }));
            }
        }
    }

    private void createShadowCloneAndActivateChakraBar(){
        jutsuActivator.activateChackraBar(activeUnit);
        shadowClone = activeUnit.createShadowClone();
        shadowClone.showRange();
    }

    private void activateJutsuIfJutsuCanBeActivated(){
        if(Input.mousePosition.y <= 375){
            if(jutsuActivator.canJutsuBeActivated(activeUnit)){
                if(!jutsuActivator.isJutsuActivated(characterAttack)){
                    characterAttack = GetComponent<UnitJutsuAttack>();
                    shadowClone.playJutsuFocus();
                }else{
                    characterAttack = GetComponent<UnitRegularAttack>();
                    shadowClone.stopJutsuFocus();
                }
            }
        }
    }

    private void chooseNextActiveUnit(){
        if (activatedUnitCount < UNITS_ON_SCREEN){
            setActiveUnit(Units[activatedUnitCount]);
            activatedUnitCount++;
        }else{
            characterAttack = GetComponent<EnemyRegularAttack>();
            
            List<BattleUnit> detectedEnemies = new List<BattleUnit>();
            int attackedUnit = Random.Range(0,Units.Count-1);

            detectedEnemies.Add(Units[attackedUnit]);
            StartCoroutine(characterAttack.Attack(detectedEnemies,()=>{
                calculateCurrentHealth();
                healthBar.setHealth(health);
                activatedUnitCount = 0;
                layerSorting.reorderOrderInLayer();
                chooseNextActiveUnit();
            }));
        }
    }

    private void setActiveUnit(BattleUnit activeUnit){
        this.activeUnit = activeUnit;
        characterAttack = GetComponent<UnitRegularAttack>();
        characterAttack.setActiveUnit(activeUnit);
        state = State.WaitingForPlayer;
    }

    private void calculateCurrentHealth(){
        health = 0;
        foreach(BattleUnit unit in Units){
            health+=unit.GetComponent<UnitStats>().getHealth();
        }
    }

    public void startMission(){
        chooseNextActiveUnit();
        calculateCurrentHealth();
        healthBar.setMaxHealth(health);
        jutsuActivator = GetComponent<JutsuActivator>();
        canStart = true;
    }

    public void addUnitToUnits(BattleUnit newUnit){
        Units.Add(newUnit);
    }

    private enum State{
        WaitingForPlayer,
        Busy
    }
}
