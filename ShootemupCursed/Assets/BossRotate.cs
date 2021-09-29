
using UnityEngine;

public class BossRotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, 0f, 200f*Time.deltaTime);
    }
}
