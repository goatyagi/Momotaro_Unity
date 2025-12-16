using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] private float shotRange = 500f;
	[SerializeField] private float rate = 0.15f;

	// enemy effect
	[SerializeField] private ParticleSystem deathEffect;
	
	// Gun effect
	[SerializeField] private ParticleSystem muzzleFlash;

	// Hit effect
	[SerializeField] private ParticleSystem hit_Rock;
	[SerializeField] private int scorePoint = 10;
	[SerializeField] private Transform shotPos;

	private bool isWaiting = false;


	void Start () {
		//　カーソルを自前のカーソルに変更
		Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
	}

	void FixedUpdate () {
		//　マウスの左クリックで撃つ
		if(Input.GetButton("Fire1")) {
			if(!isWaiting) {
				muzzleFlash.Play();
				shot();
				StartCoroutine(ShootTimer());
			}
			
		}
	}

	//　敵を撃つ
	void shot() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, shotRange, LayerMask.GetMask("Enemy"))) {

			// the occur of the enemy's death effect
			Transform deathPos = hit.collider.gameObject.transform.Find("effectPos");
			
			Destroy(hit.collider.gameObject);

			ParticleSystem death = Instantiate(deathEffect, deathPos.transform.position, deathEffect.transform.rotation);
			BoatController.Instance.score += scorePoint;
		} else if (Physics.Raycast(ray, out hit, shotRange, LayerMask.GetMask("Rock"))) {

			ParticleSystem hitToRock = Instantiate(hit_Rock, hit.point, hit_Rock.transform.rotation);

		}
	}

	IEnumerator ShootTimer() {
		isWaiting = true;
		yield return new WaitForSeconds(rate);
		isWaiting = false;
	}
}
