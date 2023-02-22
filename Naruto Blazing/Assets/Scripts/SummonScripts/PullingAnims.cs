using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class PullingAnims : MonoBehaviour{
    private SummonBase summonedUnits;
    private SummoningAssets summoningAssets; 
    private GatheredUnits gatheredUnits = GatheredUnits.getInstance();
    [SerializeField]
    private Animator[] contracts, contractToUnitTransitions; 
    
    [SerializeField]
    // SS means SingleSummons
    private Animator SSContract, SSContractTransition;
    [SerializeField]
    private Image SSUnitTemplate, SSUnitBackgroundTemplates;

    [SerializeField]
    private Image[] unitTemplates, unitBackgroundTemplates;
    [SerializeField]
    private Animator firstTimeSummonAnim;
    [SerializeField]
    private GameObject UI, background;
    public TextMeshProUGUI nextTxt;
    private int contractToActivateIndex = 0, unitToShow = 0, numberOfSummonedUnits;
    private bool lastClick = false;

    private void Start() {
        summonedUnits = GetComponent<SummonBase>();      
        summoningAssets = GetComponent<SummoningAssets>(); 
        numberOfSummonedUnits = summonedUnits.getNumberOfSummonedUnits() - 1;
        if(numberOfSummonedUnits == 0){
            contracts[0] = SSContract;
            contractToUnitTransitions[0] = SSContractTransition;
            unitTemplates[0] = SSUnitTemplate;
            unitBackgroundTemplates[0] = SSUnitBackgroundTemplates;
        }
        for(int i=0; i<=numberOfSummonedUnits; ++i){
            changeContractRarity(i);
            Invoke("activateContract", i * 0.1f);
        }
    }

    private void changeContractRarity(int currentContract){
        SpriteRenderer contract = contracts[currentContract].GetComponent<SpriteRenderer>();
        int summonedUnitRarityID = summonedUnits.getSummonedUnitRarityID(currentContract);
        
        switch(summonedUnitRarityID){
            case 3:
                contract.sprite = summoningAssets.threeStarContract;
                unitBackgroundTemplates[currentContract].sprite = summoningAssets.threeStarUnitBackground;
                break;
            case 4:
                contract.sprite = summoningAssets.fourStarContract;
                unitBackgroundTemplates[currentContract].sprite = summoningAssets.fourStarUnitBackground;
                break;
            case 5:
                contract.sprite = summoningAssets.fiveStarContract;
                unitBackgroundTemplates[currentContract].sprite = summoningAssets.fiveStarUnitBackground;
                break;
            default: return;
        }   
    }

    private void activateContract(){
        contracts[contractToActivateIndex].gameObject.SetActive(true);
        contracts[contractToActivateIndex].Play("spawnIn");
        ++contractToActivateIndex;
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(lastClick){
                SceneManager.LoadScene("summons");
            }else{
                playUnitSpawnAnimAndShowUnit();
                if(unitToShow == numberOfSummonedUnits){
                    nextTxt.gameObject.SetActive(true);
                    lastClick = true;
                }
            }
        }
    }

    private void playUnitSpawnAnimAndShowUnit(){
        playContractToUnitTransition();
        Invoke("showUnit",0.5f);
    }

    private void playContractToUnitTransition(){
        contractToUnitTransitions[unitToShow].gameObject.SetActive(true);
        contractToUnitTransitions[unitToShow].Play("contract");
        contracts[unitToShow].gameObject.SetActive(false);
    }

    private IEnumerator showUnit(){
        Sprite unit = summonedUnits.getSummonedUnit(unitToShow);
        unitTemplates[unitToShow].sprite = unit;
        if(!gatheredUnits.gatheredUnits.Contains(unit)){
            UI.SetActive(false);
            background.SetActive(true);
            firstTimeSummonAnim.gameObject.SetActive(true);
            firstTimeSummonAnim.Play("firstSummonAnimRare");
            yield return new WaitForSeconds(3);
        }
        gatheredUnits.gatheredUnits.Add(unit);
        unitTemplates[unitToShow].gameObject.transform.parent.gameObject.SetActive(true);
        ++unitToShow;
    }
}
