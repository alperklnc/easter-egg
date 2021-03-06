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
        
        

        public bool IsInGroup { get; set; } = false;
        
        public Chocolate ChocolateType { get; set; }
        public Pattern PatternType { get; set; }
        
        [SerializeField] private GameObject ribbon;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            minScale = transform.localScale;
            maxScale = new Vector3(minScale.x + scaleLimit, minScale.y + scaleLimit, minScale.z + scaleLimit);
        }
        
        public void ActivateRibbon()
        {
            ribbon.SetActive(true);
            ResizingAnimation(1.25f);
        }

        #region Stack Addition Animation

        float throwSpeed = 3f;
        float throwDuration = 2f;

        public void ThrowingAnimation(Vector3 newPos)
        {
            StartCoroutine(ThrowEgg(newPos, throwDuration));
        }

        private IEnumerator ThrowEgg(Vector3 toPos,float time)
        {
            float timeElapsed = 0f;
            float rate = (1f / time) * throwSpeed;

            while (timeElapsed < 1f)
            {
                timeElapsed += Time.deltaTime * rate;

                Vector3 nextPos = Vector3.Lerp(transform.position, toPos, timeElapsed);
                gameObject.transform.position = nextPos;
                yield return null;
            }
            yield return new WaitForEndOfFrame();
        }

        float resizeSpeed = 4f;
        float resizeDuration = 0.5f;

        public void ResizingAnimation(float mult)
        {
            maxScale *= mult;
            StartCoroutine(StartResizing());
        }

        private IEnumerator StartResizing()
        {
            yield return Lerp(minScale,maxScale,0.1f, resizeDuration);
            yield return Lerp(maxScale,minScale,-0.1f, resizeDuration);
        }

        private IEnumerator Lerp(Vector3 a, Vector3 b, float y, float time)
        {
            float timeElapsed = 0f;
            float rate = (1f / time) * resizeSpeed;
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
        
        #endregion

        public void ChangeMaterial(Pattern pattern, Chocolate chocolate)
        {
            ChocolateType = chocolate;
            String chocolateName = ChocolateType.ToString();
            PatternType = pattern;
            String patternName = PatternType.ToString();
            
            if(ChocolateType == Chocolate.None) chocolateName = String.Empty;

            var materialName = patternName + chocolateName;

            GetComponent<MeshRenderer>().material = ResourceService.GetEggMaterial(materialName);
        }

        public void ChangeOnlyPattern(Pattern pattern)
        {
            ChangeMaterial(pattern, ChocolateType);
        }

        public void ChangeOnlyChocolate(Chocolate chocolate)
        {
            ChangeMaterial(PatternType, chocolate);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if((other.gameObject.CompareTag("EasterEgg") || other.gameObject.CompareTag("Player")) && !IsInGroup)
            {
                IsInGroup = true;
                animator.enabled = false;
                EggStackManager.Instance.AddEasterEgg(gameObject);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            var pattern = other.GetComponent<Modifier>().PatternType;
            ChangeOnlyPattern(pattern);
        }

    }
}
