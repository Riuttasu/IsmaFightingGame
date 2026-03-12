using UnityEditor;
using UnityEngine;

public class PlayerHitBoxManager : MonoBehaviour
{
    [Header("Hitboxes")]
    [SerializeField] private GameObject _punchHitBox;
    public void SetHitBox(string _name, bool _enabled)
    {
        switch(_name)
        {
            case "Punch": _punchHitBox.SetActive(_enabled); break;
            default: Debug.LogWarning("No Hitbox associated with said name"); break;
        }
    }
}
