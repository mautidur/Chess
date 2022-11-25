using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instantiate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bidak_Catur;

    //player dan posisi di masing masing bidak
    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] playerwhite = new GameObject[16];
    private GameObject[] playerblack = new GameObject[16];

    private string currentplayer = "white";
    private bool gameOver = false;

    void Start()
    {
        playerwhite = new GameObject[]
        {
            Create("white_rook", 0, 0),
            Create("white_knight", 1, 0), Create("white_bishop", 2, 0),
            Create("white_queen", 3, 0),Create("white_king", 4, 0), Create("white_bishop", 5, 0),
            Create("white_knight", 6, 0), Create("white_rook", 7, 0), Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1), Create("white_pawn", 4, 1),
            Create("white_pawn", 5, 1), Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)
        };

        playerblack = new GameObject[]
        {
            Create("black_rook", 0, 7),
            Create("black_knight", 1, 7), Create("black_bishop", 2, 7),
            Create("black_queen", 3, 7),Create("black_king", 4, 7), Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7), Create("black_rook", 7, 7), Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6), Create("black_pawn", 4, 6),
            Create("black_pawn", 5, 6), Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        // Set all piece posititions on the posisition board
        for (int i = 0; i < playerblack.Length; i++)
        {
            Setposisiton(playerblack[i]);
            Setposisiton(playerwhite[i]);
        }



    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(Bidak_Catur, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;

    }

    public void Setposisiton(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXboard(), cm.GetYboard()] = obj;
    }

    public void Setpositionempty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject Getposition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositiononBoard(int x, int y)
    {
        if(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
            return false;
        return true;
    }

    public string Getcurrentplayer()
    {
        return currentplayer;
    }

    public bool Isgameover()
    {
        return gameOver;
    }

    public void Nexturn()
    {
        if (currentplayer == "white")
        {
            currentplayer = "black";
        }
        else
        {
            currentplayer = "white";
        }
    }

    public void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Instantiate");
        }
    }












}


