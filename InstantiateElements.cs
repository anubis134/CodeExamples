using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstantiateElements : MonoBehaviour
{
    #region Vars
    public GameObject PointForSpawn;
    public GameObject[] Prefabs;
    public int CountOfCells;
    private bool AllowCreating = true;
    public List<GameObject> CellsOfTable = new List<GameObject>();
    public Text TextForInformation;
    #endregion


    #region Functions
    private void Start()
    {
        StartCoroutine("SplashText");
        GameObject gm = Instantiate(Prefabs[Random.Range(0, 7)]);
        gm.transform.position = PointForSpawn.transform.position;
    }

    public void CreateNewSubject() {
        if (AllowCreating)
        {
            GameObject gm = Instantiate(Prefabs[Random.Range(0, 7)]);
            gm.transform.position = PointForSpawn.transform.position;
            CountOfCells += gm.GetComponent<DragAndDrop>().SizeOfElement;
        }

    }

    private void Update()
    {
        if (CountOfCells > 25) {
            AllowCreating = false;
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene("SampleScene");
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator SplashText() {
        while (TextForInformation.color.a > 0) {
            TextForInformation.color -= new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    #endregion
}
