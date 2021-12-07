using System;
using UnityEngine;

public sealed class NPCController : MonoBehaviour
{
    private static NPCController instance 
    {
        get
        {
            if (m_instance == null)
            {
                var go = new GameObject("[NPC Controller]");
                m_instance = go.AddComponent<NPCController>();
            }

            return m_instance;
        }
    }

    private static NPCController m_instance;

    private GameObject[] NPCs;

    private void Awake()
    {
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
    }

    private void SendHelp()
    {
        foreach (var npc in NPCs)
        {
            var NPC = npc.GetComponent<NPC>();
            NPC.HelpFriend();
        }
    }

    public static void AskForHelp()
    {
        instance.SendHelp();
    }
}