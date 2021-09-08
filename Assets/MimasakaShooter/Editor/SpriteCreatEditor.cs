using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MimasakaShooterEditor
{
    public class SpriteCreateEditor
    {
        [MenuItem("美作シューター/スプライト移動 全部")]
        private static void SpriteSave1()
        {
            var spriteCreateEditor = new SpriteCreateEditor();
            spriteCreateEditor.targetPath = "Assets/stg_basic_pics/pics";
            spriteCreateEditor.savaPath = "Assets/MimasakaShooter/Resources/Sprites";

            spriteCreateEditor.CreateTexture();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private string targetPath;
        private string savaPath;

        private void CreateTexture()
        {
            var sprites = AssetDatabase
                .FindAssets("t:sprite", new string[] {targetPath})
                .Select(AssetDatabase.GUIDToAssetPath)
                .SelectMany(AssetDatabase.LoadAllAssetsAtPath)
                .OfType<Sprite>();

            foreach (var sprite in sprites)
            {
                var texture = sprite.texture;
                var colors = new List<Color>();
                for (var y = (int) sprite.textureRect.yMin; y < (int) sprite.textureRect.yMax; y++)
                {
                    for (var x = (int) sprite.textureRect.xMin; x < (int) sprite.textureRect.xMax; x++)
                    {
                        colors.Add(texture.GetPixel(x, y));
                    }
                }

                var result = new Texture2D((int) sprite.textureRect.width, (int) sprite.textureRect.height);
                result.SetPixels(colors.ToArray());
                System.IO.File.WriteAllBytes($"{savaPath}/{sprite.name}.png", result.EncodeToPNG());
            }
        }
    }
}