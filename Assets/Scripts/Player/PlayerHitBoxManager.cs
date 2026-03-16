using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHitBoxManager : MonoBehaviour
{
    [Header("HurtBox")]
    [SerializeField] private BoxCollider2D PlayerHurtBox;
    [Header("Hitboxes")]
    [SerializeField] private GameObject PunchHitBox;
    [SerializeField] private GameObject CrushHitBox;
    private Vector2 _originalHurtBoxSize;
    private Vector2 _originalHurtBoxOffset;
    private void Start()
    {
        _originalHurtBoxSize = PlayerHurtBox.size;
        _originalHurtBoxOffset = PlayerHurtBox.offset;
    }
    public void SetHitBox(string _name, bool _enabled)
    {
        switch(_name)
        {
            case "Punch": PunchHitBox.SetActive(_enabled); break;
            case "Crush": 
                if (_enabled)
                {
                    ChangeHurtBox(new Vector2(1,0.5f),new Vector2(0,-0.25f));
                }
                else
                {
                    OriginalHurtbox();
                }
                CrushHitBox.SetActive(_enabled);
                break;
            default: Debug.LogWarning("No Hitbox associated with said name"); break;
        }
    }
    private void OriginalHurtbox()
    {
        PlayerHurtBox.size = _originalHurtBoxSize;
        PlayerHurtBox.offset = _originalHurtBoxOffset;
    }
    private void ChangeHurtBox(Vector2 size, Vector2 offset)
    {
        PlayerHurtBox.size = size;
        PlayerHurtBox.offset = offset;
    }
}
