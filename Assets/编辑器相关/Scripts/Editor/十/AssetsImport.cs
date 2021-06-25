using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//对导入的纹理贴图资源进行一定的自动设置
public class AssetsImport : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        Debug.Log("OnPreprocessTexture:" + this.assetPath);
        TextureImporter importer = this.assetImporter as TextureImporter;
        importer.textureType = TextureImporterType.Sprite;
        importer.maxTextureSize = 512;
        importer.mipmapEnabled = false;
    }

    public void OnPostprocessTexture(Texture2D tex)
    {
        Debug.Log("OnPostprocessTexture:" + this.assetPath);
    }
}
