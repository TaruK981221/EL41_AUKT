Shader "Custom/OutLine2D"
{
	Properties
	{
		_mainColor("mainColor", Color) = (1,1,1,1)
		_lineColor("lineColor", Color) = (0,0,0,1)
		_Range("Range", Range(0,1)) = 1
		_Alpha("Alpha", Range(0,1)) = 0.5
		[PerRendererData] _MainTex("Sprite MainTex", 2D) = ""{}//描画結果
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" 
			"Queue" = "Transparent" 
			"IgnoreProjector" = "True"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"}

			Blend SrcAlpha OneMinusSrcAlpha
		//Tags { "Queue" = "Transparent" }
		LOD 100

		Pass
		{
				CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert_img
			#pragma geometry geom
				#pragma fragment frag

			float _Alpha;
			float _Range;
			fixed4 _mainColor;
			fixed4 _lineColor;
			sampler2D _MainTex;
			// 頂点の入力: 位置、 UV
			struct appdata {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2g
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			struct g2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 rect : TEXCOORD1;
			};

			v2g vert(appdata v)
			{
				v2g o;
				o.pos = v.vertex;
				o.uv = v.texcoord;
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
			{
				g2f o;
				//中心の座標とuv座標を求める
				o.pos.x = IN[0].pos.x;
				o.pos.y = IN[0].pos.x;
				o.pos.z = IN[0].pos.y;
				o.pos.w = IN[0].pos.y;

				o.rect.x = IN[0].uv.x;
				o.rect.y = IN[0].uv.x;
				o.rect.z = IN[0].uv.y;
				o.rect.w = IN[0].uv.y;

				for (int i = 1; i < 3; i++)
				{
					o.pos.x = min(o.pos.x, IN[i].pos.x);
					o.pos.y = max(o.pos.y, IN[i].pos.x);
					o.pos.z = min(o.pos.z, IN[i].pos.y);
					o.pos.w = max(o.pos.w, IN[i].pos.y);
					//uv座標の端の値を出す
					o.rect.x = min(o.rect.x, IN[i].uv.x);
					o.rect.y = max(o.rect.y, IN[i].uv.x);
					o.rect.z = min(o.rect.z, IN[i].uv.y);
					o.rect.w = max(o.rect.w, IN[i].uv.y);
				}
				//中心の座標とuv座標
				float2 Center = float2((o.pos.y+ o.pos.x)*0.5f, (o.pos.w+ o.pos.z)*0.5f);
				float2 CenterUV = float2((o.rect.y + o.rect.x)*0.5f, (o.rect.w + o.rect.z)*0.5f);

				//描画される領域を広げる
				for (int i = 0; i < 3; i++)
				{
					o.uv = IN[i].uv+_Range*(IN[i].uv - CenterUV);
					o.pos = IN[i].pos;
					o.pos.rg = IN[i].pos.rg + _Range* (IN[i].pos.rg - Center);
					triStream.Append(o);
				}
			}

			fixed4 frag(g2f i) : SV_Target
			{
			int tick = 1;
			//元のuv座標の外は端の値におさめる;
			float2 l_uv = i.uv;
			l_uv.x = max(l_uv.x, i.rect.x);
			l_uv.x = min(l_uv.x, i.rect.y);
			l_uv.y = max(l_uv.y, i.rect.z);
			l_uv.y = min(l_uv.y, i.rect.w);


			/*if ((l_uv.x<i.rect.x)|| (l_uv.x > i.rect.y)|| (l_uv.y < i.rect.z)|| (l_uv.y > i.rect.w)) {
				discard;
			}*/
			fixed4 color = tex2D(_MainTex, l_uv);
			//透過しているところの周りに透過してない所があればその透過値で描画する
			if (color.a <= _Alpha) {
				for (int x = -tick; x <= tick; x++) {
					for (int y = -tick; y <= tick; y++) {
						//8方向全てで4方向の端を調べるのは遅いのでで1つ1つ書いた方が良い
						l_uv = i.uv + normalize(float2(x, y)) *_Range*0.1;
						l_uv.x = max(l_uv.x, i.rect.x);
						l_uv.x = min(l_uv.x, i.rect.y);
						l_uv.y = max(l_uv.y, i.rect.z);
						l_uv.y = min(l_uv.y, i.rect.w);
						color.a = max(color.a, tex2D(_MainTex, l_uv).a);
					}
				}
				fixed4 output = fixed4(_lineColor.x, _lineColor.y, _lineColor.z, color.a*3.0f);
				return output;
			}
			return _mainColor;
			}
			ENDCG
		}
	}
}