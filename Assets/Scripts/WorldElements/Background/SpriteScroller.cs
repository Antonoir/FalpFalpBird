using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class SpriteScroller : MonoBehaviour
    {
        [Header("Visuals")] 
        [SerializeField] private Sprite sprite = null;
        [Range(0,10)] [SerializeField] private uint nbTiles = 3;
        [Header("Behaviour")] 
        [SerializeField] private string sortingLayerName;
        [SerializeField] private Vector2 speed = Vector2.left * 0.1f;
        

        private int spriteWidth;
        private float currentOffSet;
        private GameController gameController;
        private bool isMoving;
        
        private void Awake()
        {
            Debug.Assert(sprite != null, "No sprite provided.");
            gameController = Finder.GameController;
            spriteWidth = sprite.texture.width;
            currentOffSet = 0;
            isMoving = true;
        }

        private void Start()
        {
            Vector3 tileSize = sprite.bounds.size;
            for (uint i = 0; i < nbTiles; i++)
            {
                var tile = new GameObject(i.ToString());
                tile.transform.parent = transform;
                tile.transform.localPosition = new Vector3((tileSize.x * i) -0.5f, transform.position.y);//tileSize.x * i * Vector3.right;
                
                
                var spriteRenderer = tile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingLayerName = sortingLayerName;
            }
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += Stop;
        }
        
        private void OnDisable()
        {
            gameController.OnGameStateChanged -= Stop;
        }

        private void Update()
        {
            //transform.Translate(speed * Time.deltaTime);

            if (isMoving)
            {
                currentOffSet += speed.x * Time.deltaTime;
                currentOffSet %= spriteWidth / sprite.pixelsPerUnit;

                transform.position = new Vector2(currentOffSet, transform.position.z);
            }

            /*
            if (transform.position.x <= sprite.texture * -1)
            {
                var position = transform.position;
                position.x = 0;
                transform.position = position;
            }
            */



            /*foreach (SpriteRenderer sprite in spriteRenderers)
            {
                if (sprite.transform.position.x < sprite.size.x * -1.5f )
                {
                    var position = transform.position;
                    position.x = sprite.transform.position.x + sprite.size.x * spriteRenderers.Length;
                    sprite.transform.position = position;
                }
            }*/
        }

        private void Stop(GameState state)
        {
            if (state == GameState.GameOver)
                isMoving = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var size = sprite == null? Vector3.one : sprite.bounds.size;
            var center = transform.position;
            
            Gizmos.DrawWireCube(center, size);
        }
#endif
    }
}