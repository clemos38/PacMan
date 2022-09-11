using System;
using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace CCLH
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private float boxcastSize = 0.23f;
        public List<Vector2> AvailableDirections { get; private set; }
        
        private void Start()
        {
            AddAvailableDir(Vector2.up);
            AddAvailableDir(Vector2.down);
            AddAvailableDir(Vector2.left);
            AddAvailableDir(Vector2.right);
        }

        private void AddAvailableDir(Vector2 dir)
        {
            var hit = Physics2D.BoxCast(transform.position, Vector2.one * boxcastSize, 0.0f, dir, 0.9f, obstacleLayer);
            if(hit.collider is  null)
            {
                AvailableDirections.Add(dir);
            }
        }
    }
}
