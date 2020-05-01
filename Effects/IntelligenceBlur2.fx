sampler uImage0 : register(s0); // The contents of the screen.
sampler uImage1 : register(s1); // Up to three extra textures you can use for various purposes (for instance as an overlay).
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition; // The position of the camera.
float2 uTargetPosition; // The "target" of the shader, what this actually means tends to vary per shader.
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect; // Doesn't seem to be used, but included for parity.
float2 uZoom;

float Pixels[13] =
{
   -12,
   -10,
   -8,
   -6,
   -4,
   -2,
    0,
    2,
    4,
    6,
    8,
    10,
    12,
};

float BlurWeights[13] =
{
   0.002216,
   0.008764,
   0.026995,
   0.064759,
   0.120985,
   0.176033,
   0.199471,
   0.176033,
   0.120985,
   0.064759,
   0.026995,
   0.008764,
   0.002216,
};


float4 GaussianPixelShader(float2 TextureCoordinate : TEXCOORD0) : COLOR
{
    // Pixel width
	// uColor.r = radius
	// uColor.g = x center
	// uColor.b = y center
	
	float pixelWidth2 = 1 / (float)uScreenResolution.y;

    float4 color = {0, 0, 0, 1};

    float2 blur;
    blur.x = TextureCoordinate.x;
	
	float centreCoords = (TextureCoordinate - 0.5f) * 2;
	float mask = dot(abs(0.5f - TextureCoordinate), abs(0.5f - TextureCoordinate));
	
	float distance;

    for (int i = 0; i < 13; i++) 
    {
        if (mask > uColor.r)
        {
	    	blur.y = TextureCoordinate.y + Pixels[i] * pixelWidth2;
		    color += tex2D(uImage0, blur.xy) * BlurWeights[i];
        }
        else
        {
		    color = tex2D(uImage0, TextureCoordinate.xy);
        }
    }  

    return color;
}

technique Technique1
{
    pass IntelligenceBlur2
    {
        PixelShader = compile ps_2_0 GaussianPixelShader();
    }
}