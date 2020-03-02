using SuperTanks.Core;
using SuperTanks.Tanks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Props
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] float speedRotation = 10f;
        [SerializeField] float numberOfAdditive = 5f;

        public GameSuporter gameSuporter { get; set; }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.IsGamePause)
                return;

            transform.Rotate(new Vector3(0, 0, 36 * speedRotation * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Fire>() != null)
            {
                AudioManager.PlayPickUpAudio();

                collision.GetComponent<Fire>().ObtainAmmo(numberOfAdditive);
                gameSuporter.RemoveItem(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
