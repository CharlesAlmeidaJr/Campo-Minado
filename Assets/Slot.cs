using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    bool bomba, flagged = false, revealed = false, done = false;

    public Sprite[] sprites;
    Sprite spriteReveal;

    int bombsAround, posX, posY;

    List<Slot> slotsAround = new List<Slot>(0);

    public void SetBomba (bool bomba){
        this.bomba = bomba;
        spriteReveal = sprites[9];
    }

    public bool GetBomba(){
        return this.bomba;
    }

    public void SetBombsAround (int bombsAround){
        this.bombsAround = bombsAround;
        spriteReveal = sprites[bombsAround];
    }

    public void SetPosition(int x, int y){
        posX = x;
        posY = y;
    }

    public bool GetRevealed(){
        return revealed;
    }

    // Start is called before the first frame update

    public void SlotsAround(){
        slotsAround = GameManager.gm.SlotsAround(posX, posY);

        int aux = 0;

        foreach(Slot slot in slotsAround){
            if(slot.GetBomba()){
                aux++;
            }
        }

        SetBombsAround(aux);
    }

    public void Initiate(){
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        if(revealed && !done && !bomba){

            if(bombsAround == 0){
                foreach(Slot slot in slotsAround){
                    slot.Reveal();
                }
                done = true;
            }
            else{
                done = true;
            }
            
        }
    }

    public void Reveal(){
        if(!revealed){
            GetComponent<Image>().sprite = spriteReveal;
            GetComponent<Button>().enabled = false;
            revealed = true;

            if(bomba){
                GameManager.gm.GameOver();
            }
            else{
                GameManager.gm.Verifica();
            }
        }
    }

    public void Flag(){
        if(!revealed){
            if(!flagged){
                GetComponent<Image>().sprite = sprites[10];
                flagged = true;
                GameManager.gm.SetNBombas(false);
            }
            else{
                GetComponent<Image>().sprite = sprites[11];
                flagged = false;
                GameManager.gm.SetNBombas(true);
            }
        }
    }

    public void Clicked(){
        if(GameManager.gm.flagOn){
            Flag();
        }
        else{
            Reveal();
        }
    }

    
}

