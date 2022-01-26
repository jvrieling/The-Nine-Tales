using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderInteractive
{
    public class FoliageInteractiveDetector : MonoBehaviour
    {
        public class DataHolder
        {
            public Vector2 direction;
            public float strength;
            public float elapsedTime;
            public Vector2 currentDirection;

            public DataHolder(Vector2 _direction, float _strength, float _elapsedTime = 0)
            {
                this.direction = _direction;
                this.strength = _strength;
                this.elapsedTime = _elapsedTime;
            }
        }

        public float strength;
        public float recoverRatio;
        public float waitTime = 1;

        private List<Material> changedMats = new List<Material>();
        private Dictionary<Material, DataHolder> recoverMatDic = new Dictionary<Material, DataHolder>();

        private void Update()
        {
            if (recoverMatDic.Count > 0)
            {
                foreach (KeyValuePair<Material, DataHolder> kvp in recoverMatDic)
                {
                    if (kvp.Value.elapsedTime < waitTime)
                    {
                        //kvp.Key.SetVector("_WindDirection", Vector2.Lerp(kvp.Value.currentDirection, kvp.Value.direction, kvp.Value.elapsedTime / waitTime));
                        kvp.Key.SetFloat("_WindStrength", Mathf.Lerp(strength, kvp.Value.strength, kvp.Value.elapsedTime / waitTime));
                        kvp.Value.elapsedTime += Time.deltaTime / recoverRatio;
                    }
                    else
                    {
                        recoverMatDic.Remove(kvp.Key);
                        return;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Foliage"))
            {
                Vector2 currentPos = transform.position;
                Material targetMat = collision.gameObject.GetComponent<SpriteRenderer>().material;
                if (!changedMats.Contains(targetMat))
                {
                    if (!recoverMatDic.ContainsKey(targetMat))
                    {
                        DataHolder tempHolder = new DataHolder(targetMat.GetVector("_WindDirection"), targetMat.GetFloat("_WindStrength"));
                        recoverMatDic.Add(targetMat, tempHolder);
                    }
                    else
                    {
                        recoverMatDic[targetMat].elapsedTime = 0;
                    }
                    Vector2 forceDirection = ((Vector2)collision.gameObject.transform.position - currentPos).normalized;
                    //targetMat.SetVector("_WindDirection", forceDirection);
                    recoverMatDic[targetMat].currentDirection = forceDirection;
                    targetMat.SetFloat("_WindStrength", strength);
                    changedMats.Add(targetMat);
                    //StartCoroutine(ResetWindData(targetMat));

                    //Fall off vfx imp
                    if (collision.gameObject.GetComponent<FallOffHolder>())
                    {
                        collision.gameObject.GetComponent<FallOffHolder>().TriggerVFX();
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Foliage"))
            {
                Material targetMat = collision.gameObject.GetComponent<SpriteRenderer>().material;
                if (changedMats.Contains(targetMat))
                {
                    changedMats.Remove(targetMat);
                }
            }
        }
    }
}

