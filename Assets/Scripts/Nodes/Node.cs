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
        private Vector3 pos;

        private void Awake()
        {
            pos = transform.position;
        }

        private void Start()
        {
            AvailableDirections = new List<Vector2>();
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

        /***private void OnDrawGizmos()
        {
            if (AvailableDirections is null) return;
            if (AvailableDirections.Count == 0) return;
            foreach (var direction in AvailableDirections)
            {
                Gizmos.DrawLine(pos, pos+ (Vector3)direction);
            }
        }***/

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, GetComponent<CircleCollider2D>().radius);
            if (AvailableDirections is null) return;
            if (AvailableDirections.Count == 0) return;
            foreach (var direction in AvailableDirections)
            {
                Gizmos.DrawLine(pos, pos+ (Vector3)direction);
            }
        }
    }
}
