using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TMP_Text CreditsCount;
    [SerializeField] TMP_Text MineralsCount;

    [SerializeField] float RessourceTweenDuration;
    [SerializeField] float RessourceTweenStrength;
    [SerializeField] int RessourceTweenVirbatio;

    [SerializeField] Ease ease;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        UpdateCompleteInventoryUI();
    }

    public void UpdateInventoryUI(Ressource ressource)
    {
        TMP_Text myText = null;

        switch (ressource)
        {
            case Ressource.Minerals:
                myText = MineralsCount;
                MineralsCount.text = "" + GameManager.Instance.playerInventory.GetRessourceAmount(Ressource.Minerals).ToString() + " / " + GameManager.Instance.playerInventory.GetCapacity(Ressource.Minerals);
                break;
            case Ressource.Credits:
                myText = CreditsCount;
                CreditsCount.text = "" + GameManager.Instance.playerInventory.GetRessourceAmount(Ressource.Credits).ToString() + " / " + GameManager.Instance.playerInventory.GetCapacity(Ressource.Credits);
                break; ;
        }
        if(myText != null)
        {
            myText.transform.DOPunchRotation(Vector3.back * RessourceTweenStrength, RessourceTweenDuration);
        }
    }

    public void UpdateCompleteInventoryUI()
    {
        foreach(InventoryEntry inventoryEntry in GameManager.Instance.playerInventory.MyInventoryEntries)
        {
            UpdateInventoryUI(inventoryEntry.RessourceType);
        }
    }

}

