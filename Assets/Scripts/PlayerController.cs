//#define INSTALL_CAMERA
using Mirror;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : NetworkBehaviour
{
#if INSTALL_CAMERA
    [SerializeField] GameObject _playerCameraPrefab;
#endif
    Transform _cameraT;

    IInput _input;
    IMove _move;

    NavMeshAgent _navMeshAgent;

    private void Start()
    {
        Initialize();

        //    InvokeRepeating("Jump", 1, 3);
    }



    private void Initialize()
    {
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

#if INSTALL_CAMERA
        if (!_playerCameraPrefab)
            throw new System.Exception($"_ Camera prefab not set for {this}");

        // instantiate player camera
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.LookRotation(transform.position - position, Vector3.up);
        GameObject cameraGO = Instantiate(_playerCameraPrefab, position, rotation);

        _cameraT = _playerCameraPrefab.transform;
#else
        _cameraT = Camera.main.transform;
#endif
    }


    private void Update()
    {
        // movement for local player
        if (!isLocalPlayer)
            return;

        Vector3 velocity = VelocityFromInput(_input.GetInput());

        _move.Move(velocity);

    }

    private Vector3 VelocityFromInput(Vector3 input)
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = input.x;
        velocity.z = input.y;

        // camera forward and right vectors:
        Vector3 forward = _cameraT.forward;
        Vector3 right = _cameraT.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        velocity = forward * velocity.z + right * velocity.x;

        // velocity magnitude not more than 1
        velocity = Vector3.ClampMagnitude(velocity, 1);
        return velocity;
    }
}
