using System;
using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject NPCModel;
    [SerializeField] public ParticleSystem AttackParticle;

    [Header("Waypoints")] 
    [SerializeField] private GameObject[] waypoints;
    
    [Header("NPC Stats")]
    [SerializeField] private int strength;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementAccuracy;
    [SerializeField] private float attackRange;

    private CharacterController _characterController;
    
    public Coroutine ActiveCoroutine;

    public Player Player => player;
    public GameObject[] Waypoints => waypoints;
    public int Strength => strength;
    public float Speed => speed;
    public float RotationSpeed => rotationSpeed;
    public float MovementAccuracy => movementAccuracy;
    public float AttackRange => attackRange;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void RotateTo(Vector3 direction)
    {
        NPCModel.transform.rotation = Quaternion.Slerp(NPCModel.transform.rotation,
            Quaternion.LookRotation(direction), 
            rotationSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        _characterController.Move(direction.normalized * (speed * Time.deltaTime));
    }
    
    public IEnumerator RotateToAsync(Vector3 direction)
    {
        var angle = Vector3.Angle(NPCModel.transform.forward, direction);
        while (angle > 2f)
        {
            NPCModel.transform.rotation = Quaternion.Slerp(NPCModel.transform.rotation,
                Quaternion.LookRotation(direction), 
                rotationSpeed * Time.deltaTime);

            Debug.Log("govno");
            
            angle = Vector3.Angle(NPCModel.transform.forward, direction);
            yield return null;
        }
    }

    public IEnumerator MoveToAsync(Vector3 point)
    {
        var direction = point - transform.position;
        while (Vector3.Distance(point, transform.position) > movementAccuracy / 2)
        {
            _characterController.Move(direction.normalized * (speed * Time.deltaTime));
            yield return null;
        }
    }

    public void HelpFriend()
    {
        gameObject.GetComponent<Animator>().SetBool("SomeoneAsksForHelp", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            RotateTo(-transform.forward);
        }
    }
}