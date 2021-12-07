using System;
using DefaultNamespace;
using Managers;
using Services;
using System.Collections;
using UnityEngine;

namespace EasterEgg
{
    public class EasterEggBehaviour : MonoBehaviour
    {
        private Animator animator;
        private Vector3 minScale;
        private Vector3 maxScale;
        private float scaleLimit = 8;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            minScale = transform.localScale;
            maxScale = new Vector3(minScale.x + scaleLimit, minScale.y + scaleLimit, minScale.z + scaleLimit);
        }
        
        public bool IsInGroup { get; set; } = false;
    
        private void OnTriggerEnter(Collider other)
        {
            if((other.CompareTag("EasterEgg") || other.CompareTag("Player")) && !IsInGroup)
            {
                IsInGroup = true;
                animator.enabled = false;
                EggStackManager.Instance.AddEasterEgg(gameObject);
            }
            else if (other.CompareTag("Breaker"))
            {
                EggStackManager.Instance.RemoveEasterEgg(gameObject);
            }
            else if (other.CompareTag("Modifier"))
            {
                GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(other.GetComponent<Modifier>().materialName);
            }
        }

        public void AddingAnimation()
        {
            StartCoroutine(StartResizing());
        }

        private IEnumerator StartResizing()
        {
            yield return Lerp(minScale,maxScale,0.1f,duration);
            yield return Lerp(maxScale,minScale,-0.1f,duration);
        }

        float speed = 4f;
        float duration = 0.5f;


        private IEnumerator Lerp(Vector3 a, Vector3 b, float y, float time)
        {
            float timeElapsed = 0f;
            float rate = (1f / time) * speed;
            Vector3 current = transform.position;
            float height;
            if (y > 0)
                height = y + current.y;
            else
                height = 0.3f;

            Vector3 newPos = new Vector3(current.x, height, current.z);

            while (timeElapsed < 1f)
            {
                timeElapsed += Time.deltaTime*rate;
                transform.localScale = Vector3.Lerp(a,b,timeElapsed);

                Vector3 nextPos = Vector3.Lerp(current, newPos, timeElapsed);
                transform.position = new Vector3(transform.position.x, nextPos.y, transform.position.z);
                yield return null;
            }
        }

        private void OnDestroy()
        {
            
        }
    }
}
