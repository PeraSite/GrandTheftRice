using System.Collections;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.UI;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using Sirenix.Utilities;

namespace MoreMountains.CorgiEngine
{
    /// <summary>
    /// Add this class to a GUI object, and it'll handle the instantiation and management of hearts based on the current lives of the player character
    /// It's best used on a HorizontalLayoutGroup that will handle correct positioning natively
    /// </summary>
    public class HealthGUI : MonoBehaviour
    {
        /// the sprite to use when the heart is full
        [Tooltip("the sprite to use when the heart is full")]
        public Sprite HeartFull;
        /// the sprite to use when the heart is empty
        [Tooltip("the sprite to use when the heart is empty")]
        public Sprite HeartEmpty;
        /// the size of the heart to display
        [Tooltip("the size of the heart to display")]
        public Vector2 HeartSize = new Vector2(50, 50);

        [Tooltip("the character id you want your health to be displayed from")]
        public string PlayerID = "Player1";

        [Tooltip("show empty hearts or remove them?")]
        public bool showEmptyHearts;

        /// the number of hearts to provision (if you know you'll never have more than, say, 5 hearts in your game, set it to 5.
        //[Tooltip("the number of hearts to provision (if you know you'll never have more than, say, 5 hearts in your game, set it to 5.")]


        private Health _health;
        private int MaxHealth;
        private int CurrentHealth;

        protected List<Image> Hearts;

        /// <summary>
        /// On Start we initialize our hearts
        /// </summary>
        protected virtual IEnumerator Start()
        {
            yield return new WaitUntil(() => LevelManager.Instance.Players != null);
            DrawHearts();
        }

        /// <summary>
        /// Draws as many hearts as provisioned
        /// </summary>
        protected virtual void DrawHearts() {

            _health = LevelManager.Instance.Players.Find(c => c.PlayerID.Equals(PlayerID)).GetComponent<Health>();
            MaxHealth = (int) _health.MaximumHealth;

            // we init our list
            Hearts = new List<Image>();

            // we clean any existing hearts
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }

            // we draw as many hearts as needed
            for (int i = 0; i < MaxHealth; i++)
            {
                GameObject heart = new GameObject();
                heart.transform.SetParent(this.transform);
                heart.name = "Heart" + i;

                Image heartImage = heart.AddComponent<Image>();
                heartImage.sprite = HeartFull;
                heartImage.preserveAspect = true;

                heart.MMGetComponentNoAlloc<RectTransform>().localScale = Vector3.one;
                heart.MMGetComponentNoAlloc<RectTransform>().sizeDelta = HeartSize;

                Hearts.Add(heartImage);
            }
        }

        /// <summary>
        /// On Update we'll keep our hearts updated
        /// </summary>
        protected virtual void Update() {
            if (_health.SafeIsUnityNull()) return;
            MaxHealth = (int) _health.MaximumHealth;
            CurrentHealth = (int) _health.CurrentHealth;

            UpdateHearts(MaxHealth, CurrentHealth);
        }

        /// <summary>
        /// Every frame we make sure all our hearts are in the desired state
        /// </summary>
        protected virtual void UpdateHearts(int MaxHealth, int CurrentHealth)
        {
            for (int i = 0; i < MaxHealth; i++)
            {
                if ((i < MaxHealth) && (Hearts[i].sprite != HeartEmpty))
                {
                    Hearts[i].sprite = HeartEmpty;

                    if (!showEmptyHearts)
                    {
                        Hearts[i].color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                    }

                }

                if ((i < CurrentHealth) && (Hearts[i].sprite != HeartFull))
                {
                    Hearts[i].sprite = HeartFull;

                    if (!showEmptyHearts)
                    {
                        Hearts[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                }

                if ((i < MaxHealth) && (Hearts[i].enabled == false))
                {
                    Hearts[i].enabled = true;
                }

                if ((i >= MaxHealth) && (Hearts[i].enabled != false))
                {
                    Hearts[i].enabled = false;
                }
            }
        }

    }
}
