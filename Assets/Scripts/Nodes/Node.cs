using System;
using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace CCLH
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Node : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private NodeData data;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private float boxcastSize = 0.23f;
        public List<Vector2> availableDirections { get; private set; }


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (data is null) throw new Exception("Data is null ! Set it in the inspector.");
            SetSprite();
        }

        private void Start()
        {
            checkDirectionAvailability(Vector2.up);
            checkDirectionAvailability(Vector2.down);
            checkDirectionAvailability(Vector2.left);
            checkDirectionAvailability(Vector2.right);
        }

        private void SetSprite() => _spriteRenderer.sprite = data.sprite;

        public void ChangeType([NotNull] NodeData nodeData)
        {
            if (nodeData is null) throw new Exception("nodeData shouldn't be null !");
            data = nodeData;
            SetSprite(); //Check if this could be an issue with None or Empty. 
        }

        private void checkDirectionAvailability(Vector2 dir)
        {
            var hit = Physics2D.BoxCast(transform.position, Vector2.one * boxcastSize, 0.0f, dir, 1.0f, obstacleLayer);
            if(hit.collider is  null)
            {
                availableDirections.Add(dir);
            }
        }
    }
}
