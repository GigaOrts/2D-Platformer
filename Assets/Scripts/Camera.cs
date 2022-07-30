using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _position;
    private readonly float cameraPozitionZ = -10f;
    private readonly float _cameraSpeed = 2f;

    private void Update()
    {
        _position = _player.position;
        _position.z = cameraPozitionZ;

        transform.position = Vector3.Lerp(transform.position, _position, _cameraSpeed * Time.deltaTime);
    }
}