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
    public Texture2D lastTexture;
    public MeshRenderer lastPicCanvas;

    public TMP_Text currencyTxt;
    int currencyVal;

    public TMP_Text weekText;
    int weekTime = 1;

    public GameObject[] panelTabs;

    public bool sendImageToServer = true;

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

    public void UpdateWeekCount ()
    {
        weekTime++;
        weekText.text = new string ("Week " + weekTime.ToString());
    }

    public void NewDay()
    {
        float fadeTime = 2f;
        gameObject.GetComponent<BlackoutEvent>().DoFade(fadeTime);
        StartCoroutine(DoInDarkness(fadeTime));
        jobLocked = true;
        ToggleLock();

        if (sendImageToServer)
        {
            GameObject.FindObjectOfType<Client>().sendImage(jReader.curr.Prompts[jReader.curr.Progress]);
        }
    }

    IEnumerator DoInDarkness(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        jReader.accept();
        CashIn(Mathf.FloorToInt(jReader.curr.Emails[jReader.curr.Progress].Payment));
        UpdateWeekCount();
    }

    public void DisplayNewPicture(Texture2D texture)
    {
        lastTexture = texture;
        lastPicture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    public void EnablePanelTab (int num)
    {
        foreach (GameObject tab in panelTabs) { 
            tab.SetActive(false);
        }
        panelTabs[num].SetActive(true);
    }

    public void PicturePasteLatest()
    {
        if (lastTexture == null) { return; }
        lastPicCanvas.material.mainTexture = lastTexture;
    }
}
