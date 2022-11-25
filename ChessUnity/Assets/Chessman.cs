using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //refrence
    public GameObject controller;
    public GameObject moveplates;


    //position 
    private int xboard = -1;
    private int yboard = -1;

    //player variable
    private string player;

    //karakter catur hitam
    public Sprite black_queen, black_knight, black_pawn, black_bishop, black_king, black_rook;

    //karakter catur putih
    public Sprite white_queen, white_knight, white_pawn, white_bishop, white_king, white_rook;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //set koordinat dan mengatur tempat instantiate
        SetCoords();

        // memanggil semua bidak catur
        switch (this.name)
        {
            //black chesspiece
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                player = "black";
               break;

            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                player = "black";
                break;

            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                player = "black";
                break;

            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                player = "black";
                break;

            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                player = "black";
                break;

            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                player = "black";
                break;





            //white chesspiece
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                player = "white";
                break;

            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                player = "white";
                break;

            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                player = "white";
                break;

            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                player = "white";
                break;

            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                player = "white";
                break;

            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                player = "white";
                break;

        }
    }
    public void SetCoords()
    {
        float x = xboard;
        float y = yboard;

        x *= 2.33f;
        y *= 2.33f;

        x += -8.1f;
        y += -8.1f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXboard()
    {
        return xboard;
    }

    public int GetYboard()
    {
        return yboard;
    }

    public void SetXBoard(int x)
    {
        xboard = x;
    }

    public void SetYBoard(int y)
    {
        yboard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Instantiate>().Isgameover() && controller.GetComponent<Instantiate>().Getcurrentplayer() == player)
        {
            DestroyMoveplate();

            initiateMoveplate();
        }
    }

    public void DestroyMoveplate()
    {
        GameObject[] moveplates = GameObject.FindGameObjectsWithTag("Moveplate");
        for (int i = 0; i < moveplates.Length; i++)
        {
            Destroy(moveplates[i]);
        }
        
    }

    public void initiateMoveplate()
    {
        /*if (this.name == "white_queen")
        {
            linemoveplate(1, 0);
            linemoveplate(0, 1);
            linemoveplate(-1, 0);
            linemoveplate(0, -1);
        }*/



        switch (this.name)
        {
            case "black_knight":
            case "white_knight":
                Lmoveplate();

                break;
            case "black_bishop":
            case "white_bishop":
                linemoveplate(1, 1);
                linemoveplate(1, -1);
                linemoveplate(-1, 1);
                linemoveplate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                surroundmoveplate();
                break ;
            case "black_rook":
            case "white_rook":
                linemoveplate(1, 0);
                linemoveplate(0, 1);
                linemoveplate(-1, 0);
                linemoveplate(0, -1);
                break ;
            case "black_pawn":
                pawnmoveplate(xboard, yboard - 1);
                break;
            case "white_pawn":
                pawnmoveplate(xboard, yboard + 1);
                break;
        }

    }
    public void linemoveplate(int xincrement, int yincrement)
    {
        Instantiate sc = controller.GetComponent<Instantiate>();

        int x = xboard + xincrement;
        int y = yboard + yincrement;

        while (sc.PositiononBoard(x, y) && sc.Getposition(x,y) == null)
        {
            moveplatespawn(x, y);
            x += xincrement;
            y += yincrement;
        }

        if (sc.PositiononBoard(x,y) && sc.Getposition(x,y).GetComponent<Chessman>().player != player)
        {
            moveplateattackspawn(x, y);
        } 

    }

    public void Lmoveplate()
    {
        pointmoveplate(xboard + 1, yboard + 2);
        pointmoveplate(xboard - 1, yboard + 2);
        pointmoveplate(xboard - 1, yboard - 1);
        pointmoveplate(xboard - 1, yboard - 0);
        pointmoveplate(xboard - 1, yboard + 1);
        pointmoveplate(xboard + 1, yboard - 1);
        pointmoveplate(xboard + 1, yboard - 0);
        pointmoveplate(xboard + 1, yboard + 1);
    }

    public void surroundmoveplate()
    {
        pointmoveplate(xboard, yboard + 1);
        pointmoveplate(xboard, yboard - 1);
        pointmoveplate(xboard + 1, yboard -1);
        pointmoveplate(xboard + 1, yboard + 2);
        pointmoveplate(xboard + 1, yboard + 2);
        pointmoveplate(xboard + 1, yboard + 2);
        pointmoveplate(xboard + 1, yboard + 2);
        pointmoveplate(xboard + 1, yboard + 2);
    }

    public void pointmoveplate(int x, int y)
    {
        Instantiate sc = controller.GetComponent<Instantiate>();
        if (sc.PositiononBoard(x, y))
        {
            GameObject cp = sc.Getposition(x, y);
            
            if(cp == null)
            {
                moveplatespawn(x, y);
            }
            
            else if (cp.GetComponent<Chessman>().player != player)
            {
                moveplateattackspawn(x, y);
            }


        }
    }

    public void pawnmoveplate(int x, int y)
    {
        Instantiate sc = controller.GetComponent<Instantiate>();
        if (sc.PositiononBoard(x, y))
        {
            if (sc.Getposition(x,y) == null)
            {
                moveplatespawn(x, y);
            }

            if (sc.PositiononBoard(x + 1, y) && sc.Getposition(x+1 , y) != null 
                && sc.Getposition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                moveplateattackspawn(x + 1, y);

            }

            if (sc.PositiononBoard(x - 1, y) && sc.Getposition(x - 1, y) != null
                && sc.Getposition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                moveplateattackspawn(x - 1, y);

            }
            


        }
    }

    public void moveplatespawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 2.33f;
        y *= 2.33f;

        x += -8.1f;
        y += -8.1f;

        GameObject mp = Instantiate(moveplates, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpscript = mp.GetComponent<MovePlate>();
        mpscript.Setreference(gameObject);
        mpscript.SetCoords(matrixX, matrixY);


    }

    public void moveplateattackspawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 2.33f;
        y *= 2.33f;

        x += -8.1f;
        y += -8.1f;

        GameObject mp = Instantiate(moveplates, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpscript = mp.GetComponent<MovePlate>();
        mpscript.attack = true;
        mpscript.Setreference(gameObject);
        mpscript.SetCoords(matrixX, matrixY);
    }


















}
