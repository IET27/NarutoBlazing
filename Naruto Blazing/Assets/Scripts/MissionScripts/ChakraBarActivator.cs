using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraBarActivator : MonoBehaviour{

    private GameObject unit;
    private int chakra;
    private Animator selected,notchFlash;

    public void activateChakraBar(){
        if(chakra<8){
            transform.GetChild(8).GetChild(chakra).gameObject.SetActive(true);
            chakra++;
        }
    }

    public void playActiveAnim(){
        selected.gameObject.SetActive(true);
        selected.Play("selectedSpin");
    }

    public void stopActiveAnim(){
        selected.gameObject.SetActive(false);
    }

    public void deactivateUsedChakraBars(){
        int lastChakraBarToDeactivate = chakra - 4;
        for(; chakra>=lastChakraBarToDeactivate; --chakra){
            transform.GetChild(8).GetChild(chakra).gameObject.SetActive(false);
        }
        chakra++;
    }

    public GameObject getUnit(){
        return unit;
    }

    public void setUnit(GameObject unit){
        this.unit = unit;
    }

    private void Start() {
        selected = transform.GetChild(2).gameObject.GetComponent<Animator>();
        notchFlash = transform.GetChild(11).gameObject.GetComponent<Animator>();
        chakra = 3;
        notchFlash.enabled = false;
    }

    private void Update() {
        notchFlash.enabled = chakra>=4;
        notchFlash.gameObject.SetActive(chakra>=4);
    }
}
