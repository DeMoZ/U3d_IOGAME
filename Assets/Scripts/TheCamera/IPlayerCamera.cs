using System.Numerics;

namespace TheCamera
{
    /// <summary>
    /// The interface for camera implementation
    /// </summary>

    public interface IPlayerCamera
    {
        UnityEngine.Transform GetTransform { get; }
        void Init(UnityEngine.Transform follow, UnityEngine.Transform lookAt);

        void Rotate(UnityEngine.Vector2 vector);
        void Destroy();
    }
}