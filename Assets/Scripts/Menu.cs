using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public GridLayoutGroup LevelSelector;
    void Start()
    {
        foreach(Button b in LevelSelector.GetComponentsInChildren<Button>())
        {
            string level = b.GetComponentInChildren<TextMeshProUGUI>().text;
            Image im = b.transform.Find("Lock").GetComponent<Image>();

            if (PlayerPrefs.HasKey(level))
            {
                if (PlayerPrefs.GetInt(level) == 1)
                {
                    b.interactable = true;
                    im.gameObject.SetActive(false);
                }
                else
                {
                    b.interactable = false;
                    im.gameObject.SetActive(true);
                }
            }
            else
            {
                b.interactable = false;
                im.gameObject.SetActive(true);
            }
        }
    }
    public void btnPlay()
    {
        SceneManager.LoadScene("Level1");
    }

    public void btnExit()
    {
        Application.Quit();
    }

    public void goToLvl(string num)
    {
        SceneManager.LoadScene("Level" + num);
    }
}
