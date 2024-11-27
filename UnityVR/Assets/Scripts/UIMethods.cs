using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMethods : MonoBehaviour
{
    jsonReader jReader;
    public Button previous, toggleLock, next;
    public GameObject highlight, imageWindow;
    bool jobLocked = false;
    public Image lastPicture;


    public TMP_Text currencyTxt;
    int currencyVal;

    public GameObject[] panelTabs;

    // Start is called before the first frame update
    void Start()
    {
        jReader = gameObject.GetComponent<jsonReader>();
    }

    public void ToggleLock ()
    {
        jobLocked = !jobLocked;

        previous.gameObject.SetActive(!jobLocked);
        next.gameObject.SetActive(!jobLocked);
        highlight.SetActive(jobLocked);
        imageWindow.SetActive(jobLocked);

        TMP_Text lockText = toggleLock.gameObject.GetComponentInChildren<TMP_Text>();
        Image lockImg = toggleLock.gameObject.GetComponent<Image>();
        if (jobLocked) {
            lockText.text = "Unlock";
            lockImg.color = Color.yellow;
        } else {
            lockText.text = "Lock";
            lockImg.color = new Color(0.58f, 0.86f, 0.52f);
        }

    }

    public void CashIn(int amount)
    {
        currencyVal += amount;
        currencyTxt.text = currencyVal.ToString() + "g";
    }

    private void NewDay()
    {
        jobLocked = true;
        ToggleLock();
    }

    public void DisplayNewPicture(Texture2D texture)
    {
        lastPicture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    public void EnablePanelTab (int num)
    {
        foreach (GameObject tab in panelTabs) { 
            tab.SetActive(false);
        }
        panelTabs[num].SetActive(true);
    }
}
