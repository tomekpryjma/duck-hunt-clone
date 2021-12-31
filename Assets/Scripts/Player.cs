using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private CursorMode cursorMode;
    private Vector2 hotSpot = new Vector2(15, 15);
    private bool canShoot = true;
    private Weapon weapon;
    public GameObject weaponPrefab;

    private const int MOUSE_PRIMARY = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        GameObject weap = Instantiate(weaponPrefab, new Vector3(0, -3.8f, 0), Quaternion.identity);
        weapon = weap.GetComponent<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        weapon.Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(MOUSE_PRIMARY) && canShoot)
        {
            Shoot();
        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(weapon.GetCooldown());
        canShoot = true;
    }

    private void Shoot()
    {
        canShoot = false;
        Progress.AddToStat("shots");
        weapon.Shoot();

        if (!canShoot)
        {
            StartCoroutine("Cooldown");
        }
    }
}
