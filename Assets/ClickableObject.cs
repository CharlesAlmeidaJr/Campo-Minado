using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler {
 
    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.gm.gameRunning){
            if (eventData.button == PointerEventData.InputButton.Left)
                this.GetComponent<Slot>().Reveal();
            else if (eventData.button == PointerEventData.InputButton.Right)
                this.GetComponent<Slot>().Flag();
        }
    }
}
