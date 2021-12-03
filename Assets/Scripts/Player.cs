using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int strength;

    public int Strength => strength;
}