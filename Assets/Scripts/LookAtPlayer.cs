using UnityEngine;
/// <summary>
/// 
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform objPos;
    private void Start()
    {
        if (objPos == null) { Debug.LogWarning("No object to follow"); Destroy(this); }
    }
    private void Update()
    {
        if (transform.position.x < objPos.position.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    }
}
