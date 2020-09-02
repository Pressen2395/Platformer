using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	[SerializeField]
    private float bgSpeed;
    public Renderer bgRend;

	private void Update()
	{
		bgRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
	}
}
