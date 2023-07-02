using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Transform hexagon;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float coolDown;
    [SerializeField] private float mana;
    private SpriteRenderer hexagonSprite;

    private bool isActiveHexagon, isActiveSkill = true;

    private Color hexagonColor = new Color(0.5f, 0.06368f, 0.06368f, 0.7f);
    private void Start()
    {
        hexagonSprite= hexagon.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && isActiveSkill)
        {
            isActiveHexagon = !isActiveHexagon;
            hexagonSprite.color = Color.gray;
            hexagon.gameObject.SetActive(true);
        }
        if (isActiveHexagon)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            Vector3 hexagonPos = new Vector3(mousePos.x, hexagon.position.y, mousePos.z);
            hexagon.position = hexagonPos;
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.Player.GetComponent<PlayerStats>().SpendMana(mana);
                isActiveHexagon = false;
                isActiveSkill = false;
                Invoke(nameof(ActivateSkill), coolDown);
                hexagonSprite.DOColor(hexagonColor, 3f).OnComplete(() =>
                {
                    hexagon.gameObject.SetActive(false);
                });
                GameObject projectile = Instantiate(this.projectile, hexagonPos + Vector3.up * 10, Quaternion.identity);
                projectile.GetComponent<SkillProjectile>().Damage = GetComponent<PlayerStats>().Damage;
            }
        }
    }
    private void ActivateSkill()
    {
        isActiveSkill = true;
    }
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
