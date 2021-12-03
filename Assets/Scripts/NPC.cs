using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int strength;

    public GameObject Player => player;
    public int Strength => strength;

    public void Rotate(Vector3 direction)
    {
        
    }
}