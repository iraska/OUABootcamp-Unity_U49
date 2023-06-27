using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Transform hexagon;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject projectile;
    private bool isActiveHexagon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isActiveHexagon = !isActiveHexagon;
            hexagon.gameObject.SetActive(isActiveHexagon);
        }
        if (isActiveHexagon)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            Vector3 hexagonPos = new Vector3(mousePos.x, hexagon.position.y, mousePos.z);
            hexagon.position = hexagonPos;
            if (Input.GetMouseButtonDown(0))
            {
                isActiveHexagon = false;
                hexagon.gameObject.SetActive(false);
                GameObject projectile = Instantiate(this.projectile, hexagonPos + Vector3.up * 10, Quaternion.identity);
            }
        }
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
