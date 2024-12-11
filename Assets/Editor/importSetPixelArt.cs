using UnityEngine;
using UnityEditor;

public class importPixelArt : AssetPostprocessor
{
	void OnPreprocessTexture()
	{
		TextureImporter textureImporter = (TextureImporter)assetImporter;
		textureImporter.spritePixelsPerUnit = 1024;
		textureImporter.textureCompression = TextureImporterCompression.CompressedHQ;
		textureImporter.filterMode = FilterMode.Trilinear;
	}
}