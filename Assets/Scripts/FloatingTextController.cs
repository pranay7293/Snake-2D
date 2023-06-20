
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    [SerializeField] private float secondToDestroy = 3f;

    private void Start()
    {
        Destroy(gameObject, secondToDestroy);
    }

}