// Assets/Editor/SpriteSheetImporter.cs
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;

public class SpriteSheetImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        // atlas로 쓸 PNG 파일명에 맞춰 필터링
        if (!assetPath.EndsWith("sheet.png")) return;

        var ti = (TextureImporter)assetImporter;
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.isReadable = true;  // 높이 계산을 위해 필요

        // XML 메타파일 경로 (sheet.png → sheet.xml)
        string xmlPath = Path.ChangeExtension(assetPath, ".xml");
        var xmlAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(xmlPath);
        if (xmlAsset == null)
        {
            Debug.LogError($"[SpriteSheetImporter] XML not found at {xmlPath}");
            return;
        }

        // 텍스처 높이 가져오기
        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        int texHeight = tex != null ? tex.height : 0;

        // XML 파싱
        var doc = new XmlDocument();
        doc.LoadXml(xmlAsset.text);
        var nodes = doc.GetElementsByTagName("SubTexture");

        var metas = new SpriteMetaData[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
        {
            var n = nodes[i];
            string name = n.Attributes["name"].Value;
            int x = int.Parse(n.Attributes["x"].Value);
            int y = int.Parse(n.Attributes["y"].Value);
            int w = int.Parse(n.Attributes["width"].Value);
            int h = int.Parse(n.Attributes["height"].Value);
            // XML origin이 top-left일 경우
            int uy = texHeight - y - h;

            metas[i] = new SpriteMetaData()
            {
                name = name,
                rect = new Rect(x, uy, w, h),
                pivot = new Vector2(0.5f, 0.5f),
                alignment = (int)SpriteAlignment.Center
            };
        }
    }
}
