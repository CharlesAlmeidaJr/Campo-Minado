using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menu, endGame, flagButton;
    public Text contador;
    public bool gameRunning = false;
    public static GameManager gm;
    public GameObject quadrado;
    public RectTransform fundo;
    Slot[,] mapa;
    List<Slot> bombas = new List<Slot>();

    public Sprite[] spritesButton;
    public bool flagOn = false;

    int dificudade, nBombas;

    RectTransform cv;

    void Start(){
        if(gm == null){
            gm = this;
        }

        cv = GameObject.Find("Canvas").GetComponent<RectTransform>();

        AttHUD();

        DeviceChange.OnOrientationChange += MyOrientationChangeCode;
        DeviceChange.OnResolutionChange += MyResolutionChangeCode;

        gameRunning = false;
        flagButton.GetComponent<Button>().enabled = false;
        menu.SetActive(true);
        endGame.SetActive(false);

    }

    void FixedUpdate(){
        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
    }

    void MyOrientationChangeCode(DeviceOrientation orientation) {
        AttHUD();
    }
 
    void MyResolutionChangeCode(Vector2 resolution) {
        AttHUD();
    }

    void AttHUD(){

        float x, y;

        GameObject.Find("background").GetComponent<RectTransform>().sizeDelta = cv.sizeDelta;

        if(cv.sizeDelta.x > cv.sizeDelta.y){
            GameObject.Find("Bombas").GetComponent<RectTransform>().anchorMax = new Vector2(1,0.5f);
            GameObject.Find("Bombas").GetComponent<RectTransform>().anchorMin = new Vector2(1,0.5f);
            GameObject.Find("Bombas").GetComponent<RectTransform>().anchoredPosition = new Vector3((-120f/1280f)*cv.sizeDelta.x, 0f, 0f);
        
            x = (300f/1280f)*cv.sizeDelta.x;
            y = (-30f/720f)*cv.sizeDelta.y;
        }else{
            GameObject.Find("Bombas").GetComponent<RectTransform>().anchorMax = new Vector2(1,0);
            GameObject.Find("Bombas").GetComponent<RectTransform>().anchorMin = new Vector2(1,0);
            GameObject.Find("Bombas").GetComponent<RectTransform>().anchoredPosition = new Vector3((-120f/720f)*cv.sizeDelta.x, (120f/1280f)*cv.sizeDelta.y, 0f);
        
            x = (300f/720f)*cv.sizeDelta.x;
            y = (-30f/1280f)*cv.sizeDelta.y;
        }

        GameObject.Find("Bombas").GetComponent<RectTransform>().sizeDelta = new Vector2(x,x/3);
        GameObject.Find("Contador").GetComponent<RectTransform>().sizeDelta = new Vector2(x,x/3);
        GameObject.Find("Contador").GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, y, 0f);

        x = GameObject.Find("Bombas").GetComponent<RectTransform>().sizeDelta.y*(32f/100f);

        GameObject.Find("Bombas").GetComponent<Text>().fontSize = (int)x;
        GameObject.Find("Contador").GetComponent<Text>().fontSize = (int)x;



        if(cv.sizeDelta.x > cv.sizeDelta.y){
            flagButton.GetComponent<RectTransform>().anchorMax = new Vector2(0,0.5f);
            flagButton.GetComponent<RectTransform>().anchorMin = new Vector2(0,0.5f);
            flagButton.GetComponent<RectTransform>().anchoredPosition = new Vector3((110f/1280f)*cv.sizeDelta.x, 0, 0f);
            flagButton.GetComponent<RectTransform>().sizeDelta = new Vector2((110f/1280f)*cv.sizeDelta.x, (110f/1280f)*cv.sizeDelta.x);
        } else{
            flagButton.GetComponent<RectTransform>().anchorMax = new Vector2(0,0);
            flagButton.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
            flagButton.GetComponent<RectTransform>().anchoredPosition = new Vector3((110f/720f)*cv.sizeDelta.x, (110f/1280f)*cv.sizeDelta.y, 0f);
            flagButton.GetComponent<RectTransform>().sizeDelta = new Vector2((110f/720f)*cv.sizeDelta.x, (110f/720f)*cv.sizeDelta.x);
        }



        if(cv.sizeDelta.x > cv.sizeDelta.y){
            fundo.rotation = Quaternion.Euler(new Vector3(0,0,90f));
            fundo.anchoredPosition = new Vector2(0,0);
            x = (504f/720f)*cv.sizeDelta.y;
            y = (756f/504f)*x;
            fundo.sizeDelta = new Vector2(x, y);
        } else{
            fundo.rotation = Quaternion.Euler(new Vector3(0,0,0));
            fundo.anchoredPosition = new Vector2(0,(115f/1280f)*cv.sizeDelta.y);
            x = (504f/720f)*cv.sizeDelta.x;
            y = (756f/504f)*x;
            fundo.sizeDelta = new Vector2(x, y);
        }

        if(cv.sizeDelta.x > cv.sizeDelta.y){
            menu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
            menu.GetComponent<RectTransform>().sizeDelta = new Vector2(cv.sizeDelta.x * (400f/1280f), cv.sizeDelta.y * (600f/720f));
        }else{
            menu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,cv.sizeDelta.y * (70f/1280f));
            menu.GetComponent<RectTransform>().sizeDelta = new Vector2(cv.sizeDelta.x * (600f/720f), cv.sizeDelta.y * (900f/1280f));
        }

        y = (-85f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y;

        menu.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((350f/400f)*menu.GetComponent<RectTransform>().sizeDelta.x, (100f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y);
        menu.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
        menu.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().fontSize = (int) ((45f/100f)*menu.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().sizeDelta.y);

        y = y - (141f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y;
        
        menu.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((250f/400f)*menu.GetComponent<RectTransform>().sizeDelta.x, (70f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y);
        menu.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
        menu.GetComponent<RectTransform>().GetChild(1).GetChild(0).GetComponent<Text>().fontSize = (int) ((22f/70f)*menu.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().sizeDelta.y);

        y = y - (106f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y;

        menu.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2((250f/400f)*menu.GetComponent<RectTransform>().sizeDelta.x, (70f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y);
        menu.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
        menu.GetComponent<RectTransform>().GetChild(2).GetChild(0).GetComponent<Text>().fontSize = (int) ((22f/70f)*menu.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().sizeDelta.y);

        y = y - (106f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y;
        
        menu.GetComponent<RectTransform>().GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2((250f/400f)*menu.GetComponent<RectTransform>().sizeDelta.x, (70f/600f)*menu.GetComponent<RectTransform>().sizeDelta.y);
        menu.GetComponent<RectTransform>().GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
        menu.GetComponent<RectTransform>().GetChild(3).GetChild(0).GetComponent<Text>().fontSize = (int) ((22f/70f)*menu.GetComponent<RectTransform>().GetChild(3).GetComponent<RectTransform>().sizeDelta.y);

        if(cv.sizeDelta.x > cv.sizeDelta.y){
            endGame.GetComponent<RectTransform>().sizeDelta = new Vector2(cv.sizeDelta.x * (450f/1280f), cv.sizeDelta.y * (400f/720f));
        }else{
            endGame.GetComponent<RectTransform>().sizeDelta = new Vector2(cv.sizeDelta.x * (600f/720f), cv.sizeDelta.y * (550f/1280f));
        }

        x = endGame.GetComponent<RectTransform>().sizeDelta.x;
        y = endGame.GetComponent<RectTransform>().sizeDelta.y;

        endGame.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((350f/450f)*x, (100f/400f)*y);
        endGame.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (68f/400f)*y);
        endGame.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().fontSize = (int) ((45f/100f)*endGame.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>().sizeDelta.y);

        endGame.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2((300f/450f)*x, (100f/400f)*y);
        endGame.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-48f/400f)*y);
        endGame.GetComponent<RectTransform>().GetChild(1).GetChild(0).GetComponent<Text>().fontSize = (int) ((29f/100f)*endGame.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().sizeDelta.y);

        if(mapa != null){
            float posX, posY;

            posX = (226.8f/504f)*fundo.sizeDelta.x;
            posY = (352.89f/756f)*fundo.sizeDelta.y;

            for(int x0 = 0; x0 < 10; x0++){
                for(int y0 = 0; y0 < 15; y0++){
                    mapa[x0,y0].GetComponent<RectTransform>().sizeDelta = new Vector2(fundo.sizeDelta.x/10f, fundo.sizeDelta.x/10f);
                    mapa[x0,y0].GetComponent<RectTransform>().anchoredPosition = new Vector2(posX,posY);
                    if(cv.sizeDelta.x > cv.sizeDelta.y){
                        mapa[x0,y0].GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0,0, -90f));
                    }
                    else{
                        mapa[x0,y0].GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0,0, 0));
                    }
                    
                    posY -= (fundo.sizeDelta.x/10f);
                }
                posX -= (fundo.sizeDelta.x/10f);
                posY = (352.89f/756f)*fundo.sizeDelta.y;
            }
        }
    
    
    }

    public void Facil(){
        dificudade = 10;
        StartGame();
    }

    public void Medio(){
        dificudade = 20;
        StartGame();
    }

    public void Dificil(){
        dificudade = 30;
        StartGame();
    }

    public void SetNBombas(bool add){
        if(add){
            nBombas++;
        }
        else{
            nBombas--;
        }
        contador.text = nBombas+"";
    }

    public void StartGame()
    {
        if(mapa!=null)
            if(mapa.Length>0)
                foreach(Slot slot in mapa){

                    Destroy(slot.gameObject);

                }
            
        menu.SetActive(false);

        SpawnaSlots();
        DefineBombas(dificudade);
        AchaSemBombas();

        nBombas = dificudade;
        contador.text = nBombas+"";
        
        gameRunning = true;
        flagButton.GetComponent<Button>().enabled = true;
    }

    public void Verifica(){
        int v = 0;

        foreach(Slot slot in mapa){
            if(!slot.GetBomba() && slot.GetRevealed()){
                v++;
            }
        }

        if(v == (150-dificudade)){
            endGame.SetActive(true);
            endGame.transform.GetChild(0).GetComponent<Text>().text = "GANHOU!";
            gameRunning = false;
            flagButton.GetComponent<Button>().enabled = false;
            foreach(Slot slot in mapa){
                slot.GetComponent<Button>().enabled = false;
            }
        }
    }

    void SpawnaSlots(){

        mapa = new Slot[10,15];

        float posX, posY;

        posX = (226.8f/504f)*fundo.sizeDelta.x;
        posY = (352.89f/756f)*fundo.sizeDelta.y;

        for(int x = 0; x < 10; x++){
            for(int y = 0; y < 15; y++){
                GameObject aux = Instantiate(quadrado, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
                aux.GetComponent<RectTransform>().parent = fundo;
                aux.GetComponent<RectTransform>().sizeDelta = new Vector2(fundo.sizeDelta.x/10f, fundo.sizeDelta.x/10f);
                aux.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX,posY);
                mapa[x,y] = aux.GetComponent<Slot>();
                mapa[x,y].SetPosition(x,y);
                mapa[x,y].Initiate();
                posY -= (fundo.sizeDelta.x/10f);
            }
            posX -= (fundo.sizeDelta.x/10f);
            posY = (352.89f/756f)*fundo.sizeDelta.y;
        }

    }

    void DefineBombas(int nBombas){
        
        int b = 0;

        do{
            mapa[(int) Random.Range(0,10), (int) Random.Range(0,15)].SetBomba(true);

            b = 0;

            foreach(Slot q in mapa){
                if(q.GetBomba()){
                    b++;
                    bombas.Add(q);
                }
            }

        } while (b != nBombas);
    }

    void AchaSemBombas(){
        for(int x = 0; x < 10; x++){
            for(int y = 0; y < 15; y++){
                if(!mapa[x,y].GetBomba()){
                    NumeraSlot(x, y);
                }                
            }
        }

        foreach(Slot slot in mapa){
            if(!slot.GetBomba()){
                slot.SlotsAround();
            }
        }
    }

    void NumeraSlot(int x, int y){
        int b = 0;

        if((x-1)>-1 && (y-1)>-1)
            if(mapa[x-1,y-1].GetBomba())
                b++;
        
        if((x-1)>-1)
            if(mapa[x-1,y].GetBomba())
                b++;

        if((x-1)>-1 && (y+1)<15)
            if(mapa[x-1,y+1].GetBomba())
                b++;

        if((y-1)>-1)
            if(mapa[x,y-1].GetBomba())
                b++;

        if((y+1)<15)
            if(mapa[x,y+1].GetBomba())
                b++;

        if((x+1)<10 && (y-1)>-1)
            if(mapa[x+1,y-1].GetBomba())
                b++;

        if((x+1)<10)
            if(mapa[x+1,y].GetBomba())
                b++;

        if((x+1)<10 && (y+1)<15)
            if(mapa[x+1,y+1].GetBomba())
                b++;

        mapa[x,y].SetBombsAround(b);
    }

    public List<Slot> SlotsAround(int x, int y){
        List<Slot> slotsAround = new List<Slot>();

        if((x-1)>-1 && (y-1)>-1)
            slotsAround.Add(mapa[x-1,y-1]);
        
        if((x-1)>-1)
            slotsAround.Add(mapa[x-1,y]);

        if((x-1)>-1 && (y+1)<15)
            slotsAround.Add(mapa[x-1,y+1]);

        if((y-1)>-1)
            slotsAround.Add(mapa[x,y-1]);

        if((y+1)<15)
            slotsAround.Add(mapa[x,y+1]);

        if((x+1)<10 && (y-1)>-1)
            slotsAround.Add(mapa[x+1,y-1]);

        if((x+1)<10)
            slotsAround.Add(mapa[x+1,y]);

        if((x+1)<10 && (y+1)<15)
            slotsAround.Add(mapa[x+1,y+1]);

        return slotsAround;
    }

    public List<Slot> GetBombas(){
        return bombas;
    }

    public void GameOver(){
        foreach(Slot slot in mapa){
            if(slot.GetBomba()){
                slot.Reveal();
            }
        }
        gameRunning = false;
        flagButton.GetComponent<Button>().enabled = false;

        foreach(Slot slot in mapa){
            slot.GetComponent<Button>().enabled = false;
        }

        endGame.SetActive(true);
        endGame.transform.GetChild(0).GetComponent<Text>().text = "PERDEU!";

    }

    public void Restart(){
        endGame.SetActive(false);
        menu.SetActive(true);
    }

    public void FlagSwitch(){
        if(flagOn){
            flagOn = false;
            flagButton.GetComponent<Image>().sprite = spritesButton[0];
        }
        else{
            flagOn = true;
            flagButton.GetComponent<Image>().sprite = spritesButton[1];
        }
    }

    
}
