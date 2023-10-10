using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CameraSystem
{
    public class FadingObject : MonoBehaviour
    {
        [SerializeField] private List<Renderer> renderers;
        [SerializeField] private List<Material> materials;
        [SerializeField] private float fadeSpeed = 5f;
        [SerializeField] private float fadeAmount = 0f;
        
        public bool doFade;
        private float originalAlpha;

        private void Awake()
        {
            LoadRenderers();
            LoadMaterials();
            originalAlpha = materials[0].color.a;
        }

        private void Update()
        {
            if (doFade)
            {
                foreach (var material in materials)
                {
                    material.color = new Color(material.color.r, material.color.g, material.color.b,
                        Mathf.Lerp(material.color.a, fadeAmount, fadeSpeed * Time.deltaTime));
                }
            }
            else
            {
                foreach (var material in materials)
                {
                    material.color = new Color(material.color.r, material.color.g, material.color.b,
                        Mathf.Lerp(material.color.a, originalAlpha, fadeSpeed * Time.deltaTime));
                }
            }
        }

        private void LoadRenderers()
        {
            if(renderers.Count > 0) return;
            renderers = GetComponentsInChildren<Renderer>().ToList();
        }

        private void LoadMaterials()
        {
            if(materials.Count > 0) return;

            foreach (var render in renderers)
            {
                foreach (var material in render.materials)
                {
                    if (!materials.Contains(material))
                    {
                        materials.Add(material);
                    }
                }
            }
        }
    }
}
