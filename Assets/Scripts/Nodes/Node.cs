using System;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace CCLH
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Node : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private NodeData data;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (data is null) throw new Exception("Data is null ! Set it in the inspector.");
            SetSprite();
        }

        private void SetSprite() => _spriteRenderer.sprite = data.sprite;

        public void ChangeType([NotNull] NodeData nodeData)
        {
            if (nodeData is null) throw new Exception("nodeData shouldn't be null !");
            data = nodeData;
            SetSprite(); //Check if this could be an issue with None or Empty. 
        }
    }
}
