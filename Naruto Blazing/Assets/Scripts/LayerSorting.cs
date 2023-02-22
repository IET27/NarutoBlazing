using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LayerSorting{
    private static LayerSorting layerSorting;
    private List<GameObject> charactersOnScreen = new List<GameObject>();
    private Dictionary<GameObject,SortingGroup> ordersInLayer = new Dictionary<GameObject, SortingGroup>();
    private const int newOrderInLayerMultiplier = 100;

    public void addCharacterToCharacters(GameObject character){
        charactersOnScreen.Add(character);
        ordersInLayer[character] = character.GetComponent<SortingGroup>();
    }
    
    public void removeCharacterFromCharacters(GameObject character){
        charactersOnScreen.Remove(character);
        ordersInLayer.Remove(character);
    }
    
    public static LayerSorting getInstance(){
        if(layerSorting == null){
            layerSorting = new LayerSorting();
        }
        return layerSorting;
    }
    
    public void reorderOrderInLayer(){
        sortByYPosition();
        setNewOrderInLayer();
    }
    
    private void sortByYPosition(){
        for(int i=0;i<charactersOnScreen.Count;++i){
            for(int j=0;j<charactersOnScreen.Count;++j){
                if(charactersOnScreen[i].transform.position.y > charactersOnScreen[j].transform.position.y){
                    GameObject go = charactersOnScreen[i];
                    charactersOnScreen[i] = charactersOnScreen[j];
                    charactersOnScreen[j] = go;
                }
            }
        }
    }
    
    private void setNewOrderInLayer(){
        int i = 0;
        foreach(var character in charactersOnScreen){
            SortingGroup characterSortingGroup = character.GetComponent<SortingGroup>();
            characterSortingGroup.sortingOrder = i * newOrderInLayerMultiplier;
            ++i;
        }
    }

    private LayerSorting(){}

}
