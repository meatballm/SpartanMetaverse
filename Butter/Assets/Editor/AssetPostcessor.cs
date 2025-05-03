// Assets/Editor/SpriteSheetImporter.cs
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;

public class SpriteSheetImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        // atlas�� �� PNG ���ϸ� ���� ���͸�
        if (!assetPath.EndsWith("sheet.png")) return;

        var ti = (TextureImporter)assetImporter;
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.isReadable = true;  // ���� ����� ���� �ʿ�

        // XML ��Ÿ���� ��� (sheet.png �� sheet.xml)
        string xmlPath = Path.ChangeExtension(assetPath, ".xml");
        var xmlAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(xmlPath);
        if (xmlAsset == null)
        {
            Debug.LogError($"[SpriteSheetImporter] XML not found at {xmlPath}");
            return;
        }

        // �ؽ�ó ���� ��������
        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        int texHeight = tex != null ? tex.height : 0;

        // XML �Ľ�
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
            // XML origin�� top-left�� ���
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
