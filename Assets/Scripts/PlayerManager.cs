//#define INSTANTIATE_CAMERA
using UnityEngine;

using TheCamera;
using TheInput;
using TheControllable;
//[RequireComponent(typeof(IInputSystem))]
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

    IInputSystem _inputSystem;

    IInputSystem GetInputSystem
    {
        get
        {
            if (_inputSystem == null)
            {
                _inputSystem = GetComponent<IInputSystem>();
                Debug.Log($"1 GetInputSystem= {_inputSystem != null} ");

            }
            return _inputSystem;
        }
    }

    private IControllable _controllable;

    public void SetControllable(IControllable controllable)
    {
        Debug.Log($"{this} SetControllable");

        _controllable = controllable;

        Debug.Log($"2 GetInputSystem= {GetInputSystem!=null} ");

        GetInputSystem.UnsubscribeAll();
        
        _controllable.Init(GetInputSystem, GetPlayerCamera);

        GetPlayerCamera.Init(controllable.GetTransform, controllable.GetTransform);

        GetInputSystem.SubscribeVector2Event(InputGlobals.EventsV2Enum.Look, GetPlayerCamera.Rotate);
    }
}
