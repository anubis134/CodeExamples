using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operations;
using UnityEngine.UI;


public class DragAndDrop : MonoBehaviour
{
    #region
    private GameObject StartPos;
    public bool DragAllow = false;
    public int SizeOfElement;
    public bool StartMovement = true;
    private Vector3 VectorForMovement;
    public  List<GameObject> AllSprites = new List<GameObject>();
    [SerializeField]
    public bool IsTouch = false;
    private InstantiateElements IE;
    private GameObject ForInformation;
    #endregion


    #region
    private void Awake()
    {
        ForInformation = GameObject.Find("ForInformation");
        IE = Camera.main.GetComponent<InstantiateElements>();
        StartPos = GameObject.Find("StartPosition");
        VectorForMovement = new Vector3(StartPos.transform.position.x + Random.Range(-3f, 3f), StartPos.transform.position.y, -5f);
    }
    private void OnMouseDown()
    {
        StartMovement = false;
        IsTouch = false;
        DragAllow = true;
        foreach (GameObject gm in AllSprites) { 
        gm.GetComponent<CollisionDetectForDragCells>().PositionInTable = new Vector3(gm.transform.position.x,gm.transform.position.y, -5f);
        }
        
    }

    private void OnMouseUp()
    {
        foreach (GameObject gm in AllSprites) {
            if (gm.GetComponent<CollisionDetectForDragCells>().Return) {
                gameObject.transform.position = Logic.SaveStartPosition(VectorForMovement);
                foreach (GameObject gameobject in AllSprites) {
                    gameobject.GetComponent<CollisionDetectForDragCells>().Return = false;
                }
            }
        }
        DragAllow = false;
        IsTouch = Logic.CheckTouch(AllSprites);

        if (!IsTouch) {
            gameObject.transform.position = Logic.SaveStartPosition(VectorForMovement);
        }
        if (IsTouch) {
            IE.CreateNewSubject();
        }
        
    }

    private void Update()
    {
        CheckWin();
        if (StartMovement) gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, VectorForMovement , 0.05f);
     
        if (DragAllow) {
            Vector2 CursorPos = Input.mousePosition;
            CursorPos = Camera.main.ScreenToWorldPoint(CursorPos);
            gameObject.transform.position = new Vector3(CursorPos.x, CursorPos.y, -5f);

        }
    }


    private void CheckWin() {
        int Number = 0;
        foreach (GameObject gm in IE.CellsOfTable) {
           
            if (gm.GetComponent<CellID>().Isbusy) {
                Number++;
            }
        }
        Debug.Log(Number);
        if (Number > 20) {
            ForInformation.GetComponent<Text>().color += new Color(0, 0, 0, 255);

              ForInformation.GetComponent<Text>().text = "YouWin";
        }
    }
    #endregion
}
