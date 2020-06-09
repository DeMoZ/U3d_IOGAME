//#define INSTANTIATE_CAMERA
using TheCamera;
using UnityEngine;

[RequireComponent(typeof(PlayerInputSystem))]
/// <summary>
/// on Player assigment, sets all the dependendies on it
/// </summary>
public class PlayerManager : MonoBehaviour
{
#if INSTANTIATE_CAMERA
    [SerializeField] GameObject _playerCameraPrefab;

    IPlayerCamera _playerCamera;
    IPlayerCamera GetPlayerCamera
    {
        get
        {
            if (!_playerCameraPrefab)
                throw new System.Exception($"Player camera prefab not set in {this}");

            if (_playerCamera == null)
                _playerCamera = Instantiate(_playerCameraPrefab).GetComponent<IPlayerCamera>();

            return _playerCamera;
        }
    }
#else
    [SerializeField] GameObject _playerCameraGO;

    IPlayerCamera _playerCamera;
    IPlayerCamera GetPlayerCamera
    {
        get
        {
            if (!_playerCameraGO)
                throw new System.Exception($"Player camera game object not linked in {this}");

            if (_playerCamera == null)
            {
                _playerCamera = _playerCameraGO.GetComponent<IPlayerCamera>();

                if (_playerCamera == null)
                    throw new System.Exception($"Linked camera for {this} doesn't have IPlayerCamera component");
            }

            return _playerCamera;
        }
    }
#endif

    PlayerInputSystem _playerInputSystem;

    PlayerInputSystem GetPlayerInputSystem
    {
        get
        {
            if (_playerInputSystem == null)
                _playerInputSystem = GetComponent<PlayerInputSystem>();

            return _playerInputSystem;
        }
    }

    private IControllable _controllable;

    public void SetControllable(IControllable controllable)
    {
        Debug.Log($"{this} SetControllable");

        _controllable = controllable;

        GetPlayerInputSystem.UnsubscribeAll();

        _controllable.Init(GetPlayerInputSystem, GetPlayerCamera);

        GetPlayerCamera.Init(controllable.GetTransform, controllable.GetTransform);
    }
}
