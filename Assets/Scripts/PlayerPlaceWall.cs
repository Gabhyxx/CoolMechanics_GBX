using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaceWall : MonoBehaviour, IHasCooldown //TO USE A COOLDOWN, YOU NEED TO INHERIT FROM: IHasCooldown
{
    [Header("Raycast")]
    [SerializeField] Camera cam;
    [SerializeField] LayerMask buildableLayer;
    [SerializeField] float rayLength;
    Ray ray;
    RaycastHit hit;

    [Header("test | references")]
    //[SerializeField] private Transform prefabSpawnPoint = null;
    //[SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private CooldownSystem cooldownSystem = null;

    [Header("Settings")]
    [SerializeField] private int id = 1;
    [SerializeField] private float cooldownDuration = 5f;

    [Header("Input References")]
    private PlayerInput input;
    private InputAction interact;

    [Header("Ability 1")]
    [SerializeField] private GameObject wall;

    [SerializeField] float wallDuration;
    [SerializeField] float wallDestroyDuration;

    [Header("Cooldown Management")] // EVERYTHING IN THIS HEADER RELATES TO THE COOLDOWNS. THIS INHERITS FROM: IHasCooldown
    public int Id => id;
    public float CooldownDuration => cooldownDuration;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        interact = input.actions.FindAction("Ability1");
    }

    private void Update()
    {
        AbilityOne_PlaceWall();
    }
    private void Shot() // TEST, GUARDARLO POR SI ACASO
    {
        //if (!Keyboard.current.spaceKey.wasPressedThisFrame) { return; }

        //if (cooldownSystem.IsOnCooldown(id)) { return; }

        //GameObject projectileInstance = Instantiate(projectilePrefab, prefabSpawnPoint.position, prefabSpawnPoint.rotation);

        //if(projectileInstance.TryGetComponent<Rigidbody>(out var rb))
        //{
        //    rb.AddForce(projectileInstance.transform.forward * 5, ForceMode.VelocityChange);
        //}

        //cooldownSystem.PutOnCooldown(this);
    }

    void AbilityOne_PlaceWall()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2,0));
        Debug.DrawRay(cam.transform.position, ray.direction * rayLength, Color.red);

        if (Physics.Raycast(cam.transform.position, ray.direction, out  hit, rayLength, buildableLayer))
        {
            if (interact.WasPressedThisFrame())
            {
                if (cooldownSystem.IsOnCooldown(id)) 
                {
                    Debug.Log("YOU'RE ON COOLDOWN, WAIT PLEASE");
                    return;
                }
                else 
                {
                    Debug.Log("YOU'RE NOT ON COOLDOWN!");
                    // START YOUR FUNCTION/METHOD/CORRUTINE HERE!
                    InstantiateWall();
                    cooldownSystem.PutOnCooldown(this);
                }
            }
        }
    }

    private void InstantiateWall() 
    {
        Vector3 instantiatePosition = new Vector3(hit.point.x, hit.point.y + wall.transform.localScale.y / 2, hit.point.z);
        Quaternion playerRotation = this.gameObject.transform.rotation;
        GameObject wallInstance = Instantiate(wall, instantiatePosition, playerRotation);
        Destroy(wallInstance, wallDuration);
    }
}
