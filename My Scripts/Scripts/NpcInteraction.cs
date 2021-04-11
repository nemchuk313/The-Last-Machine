using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{

    [SerializeField]
    private GameObject npcText;
    [SerializeField]
    private GameObject npcDialogueWingow;

    private GameObject _npc;
    private bool _trigger;


     void Start()
    {
        npcDialogueWingow.SetActive(false);
    }
    void Update()
    {
        if(_trigger)
        {
            npcText.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                npcText.SetActive(false);
                npcDialogueWingow.SetActive(true);
            }
        }
        else
        {
            npcText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            _trigger = true;
            _npc = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            _trigger = false;
            _npc = null;
        }
    }

    public void ClearDialoge()
    {
        npcDialogueWingow.SetActive(false);
    }
}
