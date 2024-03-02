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

    public void UpdateInventoryUI(Ressource ressource)
    {
        switch (ressource)
        {
            case Ressource.Minerals:
                CreditsCount.text = GameManager.Instance.playerInventory.GetRessourceAmount(Ressource.Credits).ToString();
                break;
            case Ressource.Credits:
                MineralsCount.text = GameManager.Instance.playerInventory.GetRessourceAmount(Ressource.Minerals).ToString();
                break; ;
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

