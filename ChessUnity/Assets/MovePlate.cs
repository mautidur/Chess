using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //posisi papan catur,bukan posisi di unity nya
    int matrixX;
    int matrixY;

    //false = movement, true = eat another piece

    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject cp = controller.GetComponent<Instantiate>().Getposition(matrixX, matrixY);
            if (cp.name == "white_king") controller.GetComponent<Instantiate>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Instantiate>().Winner("white");
            Destroy(cp);
        }

        controller.GetComponent<Instantiate>().Setpositionempty(reference.GetComponent<Chessman>().GetXboard(),
               reference.GetComponent<Chessman>().GetYboard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Instantiate>().Setposisiton(reference);

        controller.GetComponent<Instantiate>().Nexturn();

        reference.GetComponent<Chessman>().DestroyMoveplate();

    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;

    }

    public void Setreference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject Getreference()
    {
        return reference;
    }






}
