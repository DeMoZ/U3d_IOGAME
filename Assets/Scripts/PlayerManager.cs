//#define INSTALL_CAMERA
using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
#if INSTALL_CAMERA
    [SerializeField] GameObject _playerCameraPrefab;
#endif
    Transform _cameraT;

    IInput _input;
    IMove _move;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();

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
        if (isLocalPlayer)
        {
            Vector3 dirtVelocity = _input.GetInput();
            Vector3 velocity = Vector3.zero;
            velocity.x = dirtVelocity.x;
            velocity.z = dirtVelocity.y;

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

            _move.Move(velocity);
        }

    }
}
