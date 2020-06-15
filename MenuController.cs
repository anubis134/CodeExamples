using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject RulesMenu;


   public void PlayButtonClick() {
        SceneManager.LoadScene("SampleScene");
    }

    public void RulesButtonClick() {
        RulesMenu.SetActive(true);
    }

    public void CloseButtonRulesMenu() {
        RulesMenu.SetActive(false);
    }
}
