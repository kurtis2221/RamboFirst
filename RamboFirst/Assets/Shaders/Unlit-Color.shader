// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/Color" {
Properties {
	_Color ("Color", Color) = (1,1,1)
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {
		Lighting Off
		Color [_Color]
	}
}
}
