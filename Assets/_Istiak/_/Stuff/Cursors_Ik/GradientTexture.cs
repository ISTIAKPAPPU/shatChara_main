using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cursors_Ik
{
    public class GradientTexture : MonoBehaviour
    {
        public enum Res
        {
            Pix16 = 16,
            Pix32 = 32,
            Pix64 = 64,
            Pix128 = 128,
            Pix256 = 256,
            Pix512 = 512
        }

        public Gradient gradient = new Gradient();

        public new string name = "Gradient";

        public Res resolution = Res.Pix128;

        private Texture2D _texture;
        private static readonly Dictionary<int, Texture2D> TextureCache = new Dictionary<int, Texture2D>();

        private Texture2D Generate()
        {
            var res = (int) resolution;
            var tex = new Texture2D(res, 1, TextureFormat.ARGB32, false, true)
            {
                name = name,
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp
            };

            var colors = new Color[res];
            float div = res;
            for (var i = 0; i < res; i++)
            {
                float t = i / div;
                colors[i] = gradient.Evaluate(t);
            }

            tex.SetPixels(colors);
            tex.Apply(false, true);

            return tex;
        }

        private void Start()
        {
            var hash = name.GetHashCode();
            if (!TextureCache.TryGetValue(hash, out _texture))
            {
                _texture = Generate();
                TextureCache.Add(hash, _texture);
            }

            Apply();
        }

        private void OnValidate()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (_texture != null)
                DestroyImmediate(_texture);
            _texture = Generate();
            Apply();
        }

        private void Apply()
        {
            var r = GetComponent<RawImage>();
            if (r == null) throw new Exception("GradientTexture must be on an UI element with RawImage component.");
            r.texture = _texture;
        }
    }
}