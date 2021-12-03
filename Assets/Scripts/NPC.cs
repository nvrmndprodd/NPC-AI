using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CharacterController characterController;
    [SerializeField] public ParticleSystem AttackParticle;
    
    [Header("NPC Stats")]
    [SerializeField] private int strength;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementAccuracy;
    [SerializeField] private float attackRange;

    public Player Player => player;
    public CharacterController CharacterController => characterController;
    public int Strength => strength;
    public float Speed => speed;
    public float RotationSpeed => rotationSpeed;
    public float MovementAccuracy => movementAccuracy;
    public float AttackRange => attackRange;
}