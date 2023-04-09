using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Task2
{
    internal class Window : GameWindow
    {
        private int _image;

        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
        {
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            //_image = LoadPinImage();

            GL.ClearColor( 1.0f, 1.0f, 1.0f, 1.0f );

            MouseDown += Window_MouseDown;
            MouseMove += Window_MouseMove;
            MouseUp += Window_MouseUp;
            KeyDown += Window_KeyDown;
            WindowState = WindowState.Maximized;
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            base.OnUpdateFrame( e );

            DrawFrame();
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );

            GL.Viewport( 0, 0, Width, Height );
            DrawFrame();
        }

        private void DrawFrame()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit );

            SetupProjectionMatrix( Width, Height );
            DrawImage( _image );
            DrawPin();
            DrawControlPoints();
            ConnectControlPoints();

            Context.SwapBuffers();
        }

        private static void SetupProjectionMatrix( int width, int height )
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadIdentity();

            double aspectRatio = (double) width / height;

            if ( aspectRatio > 1.0 )
            {
                GL.Ortho( -1.0 * aspectRatio, 1.0 * aspectRatio, -1.0, 1.0, -1.0, 1.0 );
            }
            else
            {
                GL.Ortho( -1.0, 1.0, -1.0 / aspectRatio, 1.0 / aspectRatio, -1.0, 1.0 );
            }

            GL.MatrixMode( MatrixMode.Modelview );
        }

        private static void DrawPin()
        {
            DrawBody();
            DrawFace();
            DrawHat();
            DrawGlasses();
        }

        private static void DrawBody()
        {
            GL.Color3( 0.0f, 0.0f, 0.0f );
            DrawFilledCircle( 0.0f, 0.05f, 0.55f );

            GL.Color3( 0.376, 0.369, 0.361 );
            DrawCircle( 0.0f, 0.05f, 0.55f, 0.05f );

            GL.Color3( 0.376, 0.369, 0.361 );
            DrawFilledBezierCurve(
                new Vector2( -0.415f, -0.37f ),
                new Vector2( -0.3f, -0.15f ),
                new Vector2( 0, 0.08f ),
                new Vector2( 0.3f, -0.15f ),
                new Vector2( 0.415f, -0.37f ) );

            DrawFilledBezierCurve(
                new Vector2( -0.415f, -0.37f ),
                new Vector2( 0, -0.7f ),
                new Vector2( 0.415f, -0.37f ) );

            GL.Color3( 0.82f, 0.808f, 0.812f );
            DrawFilledBezierCurve(
                new Vector2( -0.36f, -0.36f ),
                new Vector2( -0.27f, -0.17f ),
                new Vector2( 0, -0.02f ),
                new Vector2( 0.27f, -0.17f ),
                new Vector2( 0.36f, -0.36f ) );

            DrawFilledBezierCurve(
                new Vector2( -0.36f, -0.36f ),
                new Vector2( 0, -0.65f ),
                new Vector2( 0.36f, -0.36f ) );

            GL.Color3( 1.0f, 1.0f, 1.0f );
            DrawFilledBezierCurve(
                new Vector2( -0.325f, -0.305f ),
                new Vector2( -0.22f, -0.145f ),
                new Vector2( 0.05f, -0.05f ),
                new Vector2( 0.246f, -0.16f ),
                new Vector2( 0.36f, -0.36f ) );

            DrawFilledBezierCurve(
                new Vector2( -0.325f, -0.305f ),
                new Vector2( -0.023f, -0.55f ),
                new Vector2( 0.36f, -0.36f ) );
        }

        private static void DrawFace()
        {
            #region Eyes
            GL.Color3( 1.0f, 1.0f, 1.0f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.1415929f, 0.3411996f );
            GL.Vertex2( -0.1494592f, 0.463f );
            GL.Vertex2( -0.1769911f, 0.4591937f );
            GL.Vertex2( -0.2045231f, 0.4473943f );
            GL.Vertex2( -0.2261554f, 0.4336283f );
            GL.Vertex2( -0.2438545f, 0.4178958f );
            GL.Vertex2( -0.2576205f, 0.3982301f );
            GL.Vertex2( -0.2674533f, 0.3785644f );
            GL.Vertex2( -0.273353f, 0.3588987f );
            GL.Vertex2( -0.2772861f, 0.339233f );
            GL.Vertex2( -0.2792527f, 0.3156342f );
            GL.Vertex2( -0.2772861f, 0.2979351f );
            GL.Vertex2( -0.2713864f, 0.2822025f );
            GL.Vertex2( -0.2654867f, 0.2645034f );
            GL.Vertex2( -0.2536873f, 0.2448378f );
            GL.Vertex2( -0.2379548f, 0.2173058f );
            GL.Vertex2( -0.1514258f, 0.1858407f );
            GL.Vertex2( -0.07079646f, 0.207473f );
            GL.Vertex2( -0.003899712f, 0.2684364f );
            GL.Vertex2( -0.003899727f, 0.2881022f );
            GL.Vertex2( -0.003933182f, 0.3647983f );
            GL.Vertex2( -0.0117994f, 0.3962634f );
            GL.Vertex2( -0.01966569f, 0.415929f );
            GL.Vertex2( -0.03736478f, 0.4316615f );
            GL.Vertex2( -0.05899708f, 0.4454276f );
            GL.Vertex2( -0.08259587f, 0.4572271f );
            GL.Vertex2( -0.1101278f, 0.4611603f );
            GL.Vertex2( -0.1317601f, 0.463f );
            GL.Vertex2( -0.1514258f, 0.463f );
            GL.End();

            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0.1415929f, 0.3411996f );
            GL.Vertex2( 0.1494592f, 0.463f );
            GL.Vertex2( 0.1769911f, 0.4591937f );
            GL.Vertex2( 0.2045231f, 0.4473943f );
            GL.Vertex2( 0.2261554f, 0.4336283f );
            GL.Vertex2( 0.2438545f, 0.4178958f );
            GL.Vertex2( 0.2576205f, 0.3982301f );
            GL.Vertex2( 0.2674533f, 0.3785644f );
            GL.Vertex2( 0.273353f, 0.3588987f );
            GL.Vertex2( 0.2772861f, 0.339233f );
            GL.Vertex2( 0.2792527f, 0.3156342f );
            GL.Vertex2( 0.2772861f, 0.2979351f );
            GL.Vertex2( 0.2713864f, 0.2822025f );
            GL.Vertex2( 0.2654867f, 0.2645034f );
            GL.Vertex2( 0.2536873f, 0.2448378f );
            GL.Vertex2( 0.2379548f, 0.2173058f );
            GL.Vertex2( 0.1514258f, 0.1858407f );
            GL.Vertex2( 0.07079646f, 0.207473f );
            GL.Vertex2( 0.003899712f, 0.2684364f );
            GL.Vertex2( 0.003899727f, 0.2881022f );
            GL.Vertex2( 0.003933182f, 0.3647983f );
            GL.Vertex2( 0.0117994f, 0.3962634f );
            GL.Vertex2( 0.01966569f, 0.415929f );
            GL.Vertex2( 0.03736478f, 0.4316615f );
            GL.Vertex2( 0.05899708f, 0.4454276f );
            GL.Vertex2( 0.08259587f, 0.4572271f );
            GL.Vertex2( 0.1101278f, 0.4611603f );
            GL.Vertex2( 0.1317601f, 0.463f );
            GL.Vertex2( 0.1514258f, 0.463f );
            GL.End();

            GL.Color3( 0.0f, 0.0f, 0.0f );
            DrawFilledCircle( -0.05803048f, 0.3235005f, 0.044f, 0.056f );
            DrawFilledCircle( 0.05803048f, 0.3235005f, 0.044f, 0.056f );

            GL.Color3( 1.0f, 1.0f, 1.0f );
            DrawFilledCircle( -0.03343166f, 0.351f, 0.013f, 0.018f );
            DrawFilledCircle( 0.083f, 0.351f, 0.013f, 0.018f );
            #endregion

            #region Outline
            GL.Color3( 0.631f, 0.153f, 0.153f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.0f, 0.1266667f );
            GL.Vertex2( -0.001f, 0.2763028f );
            GL.Vertex2( -0.005899705f, 0.2743363f );
            GL.Vertex2( -0.03539823f, 0.2546706f );
            GL.Vertex2( -0.06489675f, 0.2350049f );
            GL.Vertex2( -0.09832841f, 0.2192724f );
            GL.Vertex2( -0.1297935f, 0.2094395f );
            GL.Vertex2( -0.159292f, 0.207473f );
            GL.Vertex2( -0.1907571f, 0.2114061f );
            GL.Vertex2( -0.2202557f, 0.2232055f );
            GL.Vertex2( -0.245821f, 0.238938f );
            GL.Vertex2( -0.273353f, 0.2566372f );
            GL.Vertex2( -0.2969518f, 0.2625369f );
            GL.Vertex2( -0.3205507f, 0.2566372f );
            GL.Vertex2( -0.346116f, 0.2428712f );
            GL.Vertex2( -0.3638151f, 0.2291052f );
            GL.Vertex2( -0.3775811f, 0.2153392f );
            GL.Vertex2( -0.3854474f, 0.1976401f );
            GL.Vertex2( -0.3913471f, 0.1779744f );
            GL.Vertex2( -0.3933136f, 0.1602753f );
            GL.Vertex2( -0.3933136f, 0.1386431f );
            GL.Vertex2( -0.3913471f, 0.120944f );
            GL.Vertex2( -0.3854474f, 0.1032448f );
            GL.Vertex2( -0.3756146f, 0.07964602f );
            GL.Vertex2( -0.3638151f, 0.0619469f );
            GL.Vertex2( -0.346116f, 0.04228122f );
            GL.Vertex2( -0.3303835f, 0.02851524f );
            GL.Vertex2( -0.3107178f, 0.01474926f );
            GL.Vertex2( -0.2910521f, 0.006882989f );
            GL.Vertex2( -0.2654867f, -0.0009832842f );
            GL.Vertex2( -0.2418879f, -0.004916421f );
            GL.Vertex2( -0.2222222f, -0.006882989f );
            GL.Vertex2( -0.1986234f, -0.008849557f );
            GL.Vertex2( -0.1750246f, -0.008849557f );
            GL.Vertex2( -0.1533923f, -0.006882989f );
            GL.Vertex2( -0.1297935f, -0.002949852f );
            GL.Vertex2( -0.1042281f, -0.0009832842f );
            GL.Vertex2( -0.07866274f, 0.0009832842f );
            GL.Vertex2( -0.01573255f, 0.004916421f );
            GL.Vertex2( -0.0f, 0.004916421f );

            GL.Vertex2( 0.001f, 0.2763028f );
            GL.Vertex2( 0.005899705f, 0.2743363f );
            GL.Vertex2( 0.03539823f, 0.2546706f );
            GL.Vertex2( 0.06489675f, 0.2350049f );
            GL.Vertex2( 0.09832841f, 0.2192724f );
            GL.Vertex2( 0.1297935f, 0.2094395f );
            GL.Vertex2( 0.159292f, 0.207473f );
            GL.Vertex2( 0.1907571f, 0.2114061f );
            GL.Vertex2( 0.2202557f, 0.2232055f );
            GL.Vertex2( 0.245821f, 0.238938f );
            GL.Vertex2( 0.273353f, 0.2566372f );
            GL.Vertex2( 0.2969518f, 0.2625369f );
            GL.Vertex2( 0.3205507f, 0.2566372f );
            GL.Vertex2( 0.346116f, 0.2428712f );
            GL.Vertex2( 0.3638151f, 0.2291052f );
            GL.Vertex2( 0.3775811f, 0.2153392f );
            GL.Vertex2( 0.3854474f, 0.1976401f );
            GL.Vertex2( 0.3913471f, 0.1779744f );
            GL.Vertex2( 0.3933136f, 0.1602753f );
            GL.Vertex2( 0.3933136f, 0.1386431f );
            GL.Vertex2( 0.3913471f, 0.120944f );
            GL.Vertex2( 0.3854474f, 0.1032448f );
            GL.Vertex2( 0.3756146f, 0.07964602f );
            GL.Vertex2( 0.3638151f, 0.0619469f );
            GL.Vertex2( 0.346116f, 0.04228122f );
            GL.Vertex2( 0.3303835f, 0.02851524f );
            GL.Vertex2( 0.3107178f, 0.01474926f );
            GL.Vertex2( 0.2910521f, 0.006882989f );
            GL.Vertex2( 0.2654867f, -0.0009832842f );
            GL.Vertex2( 0.2418879f, -0.004916421f );
            GL.Vertex2( 0.2222222f, -0.006882989f );
            GL.Vertex2( 0.1986234f, -0.008849557f );
            GL.Vertex2( 0.1750246f, -0.008849557f );
            GL.Vertex2( 0.1533923f, -0.006882989f );
            GL.Vertex2( 0.1297935f, -0.002949852f );
            GL.Vertex2( 0.1042281f, -0.0009832842f );
            GL.Vertex2( 0.07866274f, 0.0009832842f );
            GL.Vertex2( 0.01573255f, 0.004916421f );
            GL.Vertex2( 0.0f, 0.004916421f );
            GL.End();
            #endregion

            #region Fill
            GL.Color3( 0.761f, 0.149f, 0.129f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.001966568f, 0.1052114f );
            GL.Vertex2( -0.0f, 0.252704f );
            GL.Vertex2( -0.02163225f, 0.2468043f );
            GL.Vertex2( -0.0432645f, 0.2330384f );
            GL.Vertex2( -0.06293019f, 0.2232055f );
            GL.Vertex2( -0.07669616f, 0.2153392f );
            GL.Vertex2( -0.100295f, 0.2035398f );
            GL.Vertex2( -0.1258604f, 0.1976401f );
            GL.Vertex2( -0.1514258f, 0.193707f );
            GL.Vertex2( -0.1789577f, 0.1956736f );
            GL.Vertex2( -0.2045231f, 0.2015733f );
            GL.Vertex2( -0.2320551f, 0.2114061f );
            GL.Vertex2( -0.2517208f, 0.2271386f );
            GL.Vertex2( -0.273353f, 0.238938f );
            GL.Vertex2( -0.2930187f, 0.2428712f );
            GL.Vertex2( -0.3126844f, 0.2428712f );
            GL.Vertex2( -0.3284169f, 0.2350049f );
            GL.Vertex2( -0.3421829f, 0.2251721f );
            GL.Vertex2( -0.359882f, 0.2094395f );
            GL.Vertex2( -0.3716814f, 0.1897738f );
            GL.Vertex2( -0.3756146f, 0.1681416f );
            GL.Vertex2( -0.3756146f, 0.1465093f );
            GL.Vertex2( -0.373648f, 0.1288102f );
            GL.Vertex2( -0.3657817f, 0.107178f );
            GL.Vertex2( -0.3579154f, 0.08947886f );
            GL.Vertex2( -0.346116f, 0.07374632f );
            GL.Vertex2( -0.3362832f, 0.0619469f );
            GL.Vertex2( -0.3284169f, 0.05211406f );
            GL.Vertex2( -0.314651f, 0.04031465f );
            GL.Vertex2( -0.3028515f, 0.03441495f );
            GL.Vertex2( -0.2910521f, 0.02851524f );
            GL.Vertex2( -0.2792527f, 0.02261554f );
            GL.Vertex2( -0.259587f, 0.01474926f );
            GL.Vertex2( -0.2438545f, 0.008849557f );
            GL.Vertex2( -0.2320551f, 0.008849557f );
            GL.Vertex2( -0.2182891f, 0.008849557f );
            GL.Vertex2( -0.2025565f, 0.008849557f );
            GL.Vertex2( -0.186824f, 0.01081613f );
            GL.Vertex2( -0.1671583f, 0.01081613f );
            GL.Vertex2( -0.1435595f, 0.01278269f );
            GL.Vertex2( -0.1199607f, 0.01474926f );
            GL.Vertex2( -0.09832841f, 0.0186824f );
            GL.Vertex2( -0.0, 0.02064897f );

            GL.Vertex2( 0.0f, 0.252704f );
            GL.Vertex2( 0.02163225f, 0.2468043f );
            GL.Vertex2( 0.0432645f, 0.2330384f );
            GL.Vertex2( 0.06293019f, 0.2232055f );
            GL.Vertex2( 0.07669616f, 0.2153392f );
            GL.Vertex2( 0.100295f, 0.2035398f );
            GL.Vertex2( 0.1258604f, 0.1976401f );
            GL.Vertex2( 0.1514258f, 0.193707f );
            GL.Vertex2( 0.1789577f, 0.1956736f );
            GL.Vertex2( 0.2045231f, 0.2015733f );
            GL.Vertex2( 0.2320551f, 0.2114061f );
            GL.Vertex2( 0.2517208f, 0.2271386f );
            GL.Vertex2( 0.273353f, 0.238938f );
            GL.Vertex2( 0.2930187f, 0.2428712f );
            GL.Vertex2( 0.3126844f, 0.2428712f );
            GL.Vertex2( 0.3284169f, 0.2350049f );
            GL.Vertex2( 0.3421829f, 0.2251721f );
            GL.Vertex2( 0.359882f, 0.2094395f );
            GL.Vertex2( 0.3716814f, 0.1897738f );
            GL.Vertex2( 0.3756146f, 0.1681416f );
            GL.Vertex2( 0.3756146f, 0.1465093f );
            GL.Vertex2( 0.373648f, 0.1288102f );
            GL.Vertex2( 0.3657817f, 0.107178f );
            GL.Vertex2( 0.3579154f, 0.08947886f );
            GL.Vertex2( 0.346116f, 0.07374632f );
            GL.Vertex2( 0.3362832f, 0.0619469f );
            GL.Vertex2( 0.3284169f, 0.05211406f );
            GL.Vertex2( 0.314651f, 0.04031465f );
            GL.Vertex2( 0.3028515f, 0.03441495f );
            GL.Vertex2( 0.2910521f, 0.02851524f );
            GL.Vertex2( 0.2792527f, 0.02261554f );
            GL.Vertex2( 0.259587f, 0.01474926f );
            GL.Vertex2( 0.2438545f, 0.008849557f );
            GL.Vertex2( 0.2320551f, 0.008849557f );
            GL.Vertex2( 0.2182891f, 0.008849557f );
            GL.Vertex2( 0.2025565f, 0.008849557f );
            GL.Vertex2( 0.186824f, 0.01081613f );
            GL.Vertex2( 0.1671583f, 0.01081613f );
            GL.Vertex2( 0.1435595f, 0.01278269f );
            GL.Vertex2( 0.1199607f, 0.01474926f );
            GL.Vertex2( 0.09832841f, 0.0186824f );
            GL.Vertex2( 0.0f, 0.02064897f );
            GL.End();
            #endregion

            #region Beak outline
            GL.Color3( 0.631f, 0.153f, 0.153f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.003933137f, 0.1307768f );
            GL.Vertex2( -0.300885f, 0.2035398f );
            GL.Vertex2( -0.3107178f, 0.1897738f );
            GL.Vertex2( -0.3166175f, 0.1701082f );
            GL.Vertex2( -0.3126844f, 0.1484759f );
            GL.Vertex2( -0.3067847f, 0.1347099f );
            GL.Vertex2( -0.2969518f, 0.120944f );
            GL.Vertex2( -0.2812193f, 0.107178f );
            GL.Vertex2( -0.2694198f, 0.0993117f );
            GL.Vertex2( -0.2517208f, 0.093412f );
            GL.Vertex2( -0.2340216f, 0.08947886f );
            GL.Vertex2( -0.2182891f, 0.08554573f );
            GL.Vertex2( -0.20059f, 0.07964602f );
            GL.Vertex2( -0.186824f, 0.07374632f );
            GL.Vertex2( -0.1710915f, 0.06784661f );
            GL.Vertex2( -0.1553589f, 0.05801377f );
            GL.Vertex2( -0.1376598f, 0.04818092f );
            GL.Vertex2( -0.1258604f, 0.03441495f );
            GL.Vertex2( -0.1120944f, 0.0186824f );
            GL.Vertex2( -0.1022616f, -0.002949852f );
            GL.Vertex2( -0.09046214f, -0.02064897f );
            GL.Vertex2( -0.0747296f, -0.03441495f );
            GL.Vertex2( -0.05899705f, -0.04424779f );
            GL.Vertex2( -0.04719764f, -0.05014749f );
            GL.Vertex2( -0.03343166f, -0.05408063f );
            GL.Vertex2( -0.01376598f, -0.05801377f );
            GL.Vertex2( 0.003933142f, -0.0560472f );
            GL.Vertex2( 0.01376598f, -0.05408063f );
            GL.Vertex2( 0.03146509f, -0.05211406f );
            GL.Vertex2( 0.04523107f, -0.04818092f );
            GL.Vertex2( 0.06489675f, -0.03834808f );
            GL.Vertex2( 0.07866274f, -0.02654867f );
            GL.Vertex2( 0.09046214f, -0.01081613f );
            GL.Vertex2( 0.09832841f, 0.0009832842f );
            GL.Vertex2( 0.1061947f, 0.01474926f );
            GL.Vertex2( 0.1179941f, 0.03244838f );
            GL.Vertex2( 0.1317601f, 0.04621436f );
            GL.Vertex2( 0.1533923f, 0.05998033f );
            GL.Vertex2( 0.1671583f, 0.06784661f );
            GL.Vertex2( 0.1828908f, 0.07374632f );
            GL.Vertex2( 0.2025565f, 0.08161259f );
            GL.Vertex2( 0.2202557f, 0.08751229f );
            GL.Vertex2( 0.2438545f, 0.093412f );
            GL.Vertex2( 0.2615536f, 0.1012783f );
            GL.Vertex2( 0.2772861f, 0.107178f );
            GL.Vertex2( 0.2890855f, 0.1150442f );
            GL.Vertex2( 0.300885f, 0.1288102f );
            GL.Vertex2( 0.3067847f, 0.1425762f );
            GL.Vertex2( 0.3107178f, 0.1583087f );
            GL.Vertex2( 0.3107178f, 0.1720747f );
            GL.Vertex2( 0.3067847f, 0.1897738f );
            GL.Vertex2( 0.2969518f, 0.2055064f );
            GL.End();
            #endregion

            #region Beak fill
            GL.Color3( 0.761f, 0.149f, 0.129f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.005899705f, 0.1720747f );
            GL.Vertex2( -0.29f, 0.2035398f );
            GL.Vertex2( -0.2969518f, 0.1996067f );
            GL.Vertex2( -0.3048181f, 0.1858407f );
            GL.Vertex2( -0.3048181f, 0.166175f );
            GL.Vertex2( -0.3028515f, 0.1543756f );
            GL.Vertex2( -0.2949852f, 0.1386431f );
            GL.Vertex2( -0.2851524f, 0.1268437f );
            GL.Vertex2( -0.273353f, 0.1170108f );
            GL.Vertex2( -0.2615536f, 0.1130777f );
            GL.Vertex2( -0.2536873f, 0.1091445f );
            GL.Vertex2( -0.2438545f, 0.107178f );
            GL.Vertex2( -0.2281219f, 0.1032448f );
            GL.Vertex2( -0.2123894f, 0.0993117f );
            GL.Vertex2( -0.1986234f, 0.093412f );
            GL.Vertex2( -0.1809243f, 0.08947886f );
            GL.Vertex2( -0.1671583f, 0.08161259f );
            GL.Vertex2( -0.1533923f, 0.07571288f );
            GL.Vertex2( -0.1396263f, 0.06784661f );
            GL.Vertex2( -0.1278269f, 0.05801377f );
            GL.Vertex2( -0.114061f, 0.04621436f );
            GL.Vertex2( -0.1042281f, 0.03441495f );
            GL.Vertex2( -0.09242871f, 0.02064897f );
            GL.Vertex2( -0.08259587f, 0.006882989f );
            GL.Vertex2( -0.0747296f, -0.004916421f );
            GL.Vertex2( -0.06686332f, -0.01671583f );
            GL.Vertex2( -0.05703048f, -0.0245821f );
            GL.Vertex2( -0.04719764f, -0.03048181f );
            GL.Vertex2( -0.0373648f, -0.03244838f );
            GL.Vertex2( -0.02556539f, -0.03441495f );
            GL.Vertex2( -0.01179941f, -0.03441495f );
            GL.Vertex2( -0.001966568f, -0.03441495f );
            GL.Vertex2( 0.01376598f, -0.03441495f );
            GL.Vertex2( 0.02949852f, -0.03244838f );
            GL.Vertex2( 0.04523107f, -0.02654867f );
            GL.Vertex2( 0.05703048f, -0.0186824f );
            GL.Vertex2( 0.06686332f, -0.004916421f );
            GL.Vertex2( 0.07866274f, 0.01081613f );
            GL.Vertex2( 0.08652901f, 0.0245821f );
            GL.Vertex2( 0.09439528f, 0.03441495f );
            GL.Vertex2( 0.1081613f, 0.04818092f );
            GL.Vertex2( 0.1219272f, 0.05998033f );
            GL.Vertex2( 0.1356932f, 0.06981318f );
            GL.Vertex2( 0.1474926f, 0.07767945f );
            GL.Vertex2( 0.1573255f, 0.08161259f );
            GL.Vertex2( 0.1750246f, 0.08947886f );
            GL.Vertex2( 0.186824f, 0.093412f );
            GL.Vertex2( 0.20059f, 0.09734514f );
            GL.Vertex2( 0.2163225f, 0.1032448f );
            GL.Vertex2( 0.2320551f, 0.1052114f );
            GL.Vertex2( 0.245821f, 0.1091445f );
            GL.Vertex2( 0.2615536f, 0.1170108f );
            GL.Vertex2( 0.2772861f, 0.1288102f );
            GL.Vertex2( 0.2910521f, 0.1445428f );
            GL.Vertex2( 0.2989184f, 0.1622419f );
            GL.Vertex2( 0.30f, 0.1779744f );
            GL.Vertex2( 0.298f, 0.1917404f );
            GL.Vertex2( 0.2930187f, 0.2055064f );
            GL.End();
            #endregion
        }

        private static void DrawHat()
        {
            GL.Color3( 0.478f, 0.333f, 0.141f );

            #region Left part outline
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.4542773f, 0.5221239f );
            GL.Vertex2( -0.3972468f, 0.5358899f );
            GL.Vertex2( -0.4070796f, 0.5476893f );
            GL.Vertex2( -0.4129794f, 0.5634218f );
            GL.Vertex2( -0.4110128f, 0.5771878f );
            GL.Vertex2( -0.4070796f, 0.5909538f );
            GL.Vertex2( -0.3972468f, 0.6027532f );
            GL.Vertex2( -0.3854474f, 0.612586f );
            GL.Vertex2( -0.373648f, 0.6224189f );
            GL.Vertex2( -0.3657817f, 0.6381514f );
            GL.Vertex2( -0.3697149f, 0.653884f );
            GL.Vertex2( -0.3834808f, 0.6656834f );
            GL.Vertex2( -0.4031465f, 0.6656834f );
            GL.Vertex2( -0.4247788f, 0.6597838f );
            GL.Vertex2( -0.4405113f, 0.6538838f );
            GL.Vertex2( -0.4542774f, 0.6460177f );
            GL.Vertex2( -0.4660767f, 0.6342183f );
            GL.Vertex2( -0.4798427f, 0.626352f );
            GL.Vertex2( -0.4896755f, 0.6145526f );
            GL.Vertex2( -0.5014749f, 0.59882f );
            GL.Vertex2( -0.5113078f, 0.5830875f );
            GL.Vertex2( -0.519174f, 0.5634218f );
            GL.Vertex2( -0.5231069f, 0.545723f );
            GL.Vertex2( -0.5270403f, 0.5319567f );
            GL.Vertex2( -0.5290068f, 0.5240905f );
            GL.Vertex2( -0.5270403f, 0.5103245f );
            GL.Vertex2( -0.5231072f, 0.4906588f );
            GL.Vertex2( -0.5172074f, 0.4729597f );
            GL.Vertex2( -0.5113078f, 0.4572271f );
            GL.Vertex2( -0.505408f, 0.4473943f );
            GL.Vertex2( -0.4955752f, 0.4336283f );
            GL.Vertex2( -0.4916421f, 0.4237955f );
            GL.Vertex2( -0.4798427f, 0.4119961f );
            GL.Vertex2( -0.4700098f, 0.4021632f );
            GL.Vertex2( -0.460177f, 0.3962635f );
            GL.End();

            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.3795477f, 0.4611603f );
            GL.Vertex2( -0.3225172f, 0.5712881f );
            GL.Vertex2( -0.3362832f, 0.5594887f );
            GL.Vertex2( -0.3500492f, 0.5476893f );
            GL.Vertex2( -0.359882f, 0.539823f );
            GL.Vertex2( -0.3716814f, 0.5339233f );
            GL.Vertex2( -0.3834808f, 0.5339233f );
            GL.Vertex2( -0.4011799f, 0.5378565f );
            GL.Vertex2( -0.4641101f, 0.5516224f );
            GL.Vertex2( -0.4641101f, 0.4001967f );
            GL.Vertex2( -0.4542773f, 0.3923304f );
            GL.Vertex2( -0.4424779f, 0.3883972f );
            GL.Vertex2( -0.4326451f, 0.3844641f );
            GL.Vertex2( -0.4228122f, 0.380531f );
            GL.Vertex2( -0.4149459f, 0.380531f );
            GL.Vertex2( -0.3992134f, 0.380531f );
            GL.Vertex2( -0.3854474f, 0.3785644f );
            GL.Vertex2( -0.3697149f, 0.3785644f );
            GL.Vertex2( -0.3559489f, 0.3824975f );
            GL.Vertex2( -0.3402163f, 0.3883972f );
            GL.Vertex2( -0.3225172f, 0.3962635f );
            GL.Vertex2( -0.3067847f, 0.4060964f );
            GL.Vertex2( -0.2910521f, 0.4178958f );
            GL.Vertex2( -0.2792527f, 0.4277286f );
            GL.Vertex2( -0.2654867f, 0.4434611f );
            GL.Vertex2( -0.2517208f, 0.4611603f );
            GL.Vertex2( -0.3185841f, 0.567355f );
            GL.Vertex2( -0.3225172f, 0.5712881f );
            GL.End();
            #endregion

            #region Middle part outline
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0f, 0.6027532f );
            GL.Vertex2( -0.2949852f, 0.6460177f );
            GL.Vertex2( -0.2910521f, 0.6597837f );
            GL.Vertex2( -0.287119f, 0.6715831f );
            GL.Vertex2( -0.2772861f, 0.685349f );
            GL.Vertex2( -0.2713864f, 0.6971485f );
            GL.Vertex2( -0.2635202f, 0.7089479f );
            GL.Vertex2( -0.2536873f, 0.7207473f );
            GL.Vertex2( -0.245821f, 0.7305802f );
            GL.Vertex2( -0.2379548f, 0.740413f );
            GL.Vertex2( -0.2281219f, 0.7522124f );
            GL.Vertex2( -0.2182891f, 0.7600787f );
            GL.Vertex2( -0.2084562f, 0.7718781f );
            GL.Vertex2( -0.1986234f, 0.7797443f );
            GL.Vertex2( -0.1887906f, 0.7915438f );
            GL.Vertex2( -0.1789577f, 0.79941f );
            GL.Vertex2( -0.1710915f, 0.8053097f );
            GL.Vertex2( -0.159292f, 0.8092428f );
            GL.Vertex2( -0.1474926f, 0.8171092f );
            GL.Vertex2( -0.1356932f, 0.8230088f );
            GL.Vertex2( -0.1258604f, 0.8249754f );
            GL.Vertex2( -0.1101278f, 0.8289086f );
            GL.Vertex2( -0.09636185f, 0.8348082f );
            GL.Vertex2( -0.0806293f, 0.8367748f );
            GL.Vertex2( -0.06489675f, 0.840708f );
            GL.Vertex2( -0.05506391f, 0.8426746f );
            GL.Vertex2( -0.04129793f, 0.8426746f );
            GL.Vertex2( -0.02556539f, 0.8446411f );
            GL.Vertex2( -0.01179941f, 0.8446411f );
            GL.Vertex2( 0.007866274f, 0.8446411f );
            GL.Vertex2( 0.02163225f, 0.8446411f );
            GL.Vertex2( 0.03343166f, 0.840708f );
            GL.Vertex2( 0.04719764f, 0.840708f );
            GL.Vertex2( 0.06293019f, 0.8387414f );
            GL.Vertex2( 0.07669616f, 0.8367748f );
            GL.Vertex2( 0.08849557f, 0.8348082f );
            GL.Vertex2( 0.1061947f, 0.826942f );
            GL.Vertex2( 0.1199607f, 0.8230088f );
            GL.Vertex2( 0.1337266f, 0.8171092f );
            GL.Vertex2( 0.1435595f, 0.8151426f );
            GL.Vertex2( 0.1573255f, 0.8072763f );
            GL.Vertex2( 0.1710915f, 0.8013766f );
            GL.Vertex2( 0.1828908f, 0.7915438f );
            GL.Vertex2( 0.1927237f, 0.7856441f );
            GL.Vertex2( 0.20059f, 0.7797443f );
            GL.Vertex2( 0.214356f, 0.7659784f );
            GL.Vertex2( 0.2261554f, 0.7541789f );
            GL.Vertex2( 0.2379548f, 0.740413f );
            GL.Vertex2( 0.2497542f, 0.726647f );
            GL.Vertex2( 0.2556539f, 0.7168142f );
            GL.Vertex2( 0.2635202f, 0.7050148f );
            GL.Vertex2( 0.2694198f, 0.6912488f );
            GL.Vertex2( 0.2772861f, 0.6755162f );
            GL.Vertex2( 0.2812193f, 0.6676499f );
            GL.Vertex2( 0.287119f, 0.653884f );
            GL.Vertex2( 0.2949852f, 0.6381514f );
            GL.Vertex2( 0.2989184f, 0.6184857f );

            GL.End();
            #endregion

            #region Right part outline
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0.4542774f, 0.3883973f );
            GL.Vertex2( 0.3264503f, 0.5732546f );
            GL.Vertex2( 0.3421829f, 0.5535892f );
            GL.Vertex2( 0.3500492f, 0.5457227f );
            GL.Vertex2( 0.359882f, 0.5358899f );
            GL.Vertex2( 0.3677483f, 0.5319567f );
            GL.Vertex2( 0.3775811f, 0.526057f );
            GL.Vertex2( 0.3874139f, 0.5221239f );
            GL.Vertex2( 0.3992134f, 0.5142576f );
            GL.Vertex2( 0.4110128f, 0.5103245f );
            GL.Vertex2( 0.4208456f, 0.5063913f );
            GL.Vertex2( 0.4306785f, 0.5024582f );
            GL.Vertex2( 0.4405113f, 0.4985251f );
            GL.Vertex2( 0.4503441f, 0.494592f );
            GL.Vertex2( 0.4582104f, 0.4926254f );
            GL.Vertex2( 0.4641101f, 0.4926254f );
            GL.Vertex2( 0.4719764f, 0.4926254f );
            GL.Vertex2( 0.4818093f, 0.4926254f );
            GL.Vertex2( 0.4955752f, 0.4926254f );
            GL.Vertex2( 0.505408f, 0.494592f );
            GL.Vertex2( 0.519174f, 0.4985251f );
            GL.Vertex2( 0.5309734f, 0.5044248f );
            GL.Vertex2( 0.5447394f, 0.5142576f );
            GL.Vertex2( 0.5545722f, 0.5221239f );
            GL.Vertex2( 0.5663717f, 0.5299902f );
            GL.Vertex2( 0.5742379f, 0.5378565f );
            GL.Vertex2( 0.5860373f, 0.5417896f );
            GL.Vertex2( 0.5939037f, 0.5437561f );
            GL.Vertex2( 0.6037365f, 0.539823f );
            GL.Vertex2( 0.6116028f, 0.5319567f );
            GL.Vertex2( 0.6175025f, 0.5240905f );
            GL.Vertex2( 0.6253688f, 0.5162242f );
            GL.Vertex2( 0.6234022f, 0.5024582f );
            GL.Vertex2( 0.619469f, 0.4906588f );
            GL.Vertex2( 0.6135693f, 0.4788594f );
            GL.Vertex2( 0.6076697f, 0.46706f );
            GL.Vertex2( 0.5978368f, 0.453294f );
            GL.Vertex2( 0.5899705f, 0.4414946f );
            GL.Vertex2( 0.5801376f, 0.4257621f );
            GL.Vertex2( 0.5683382f, 0.4159292f );
            GL.Vertex2( 0.5545722f, 0.4041298f );
            GL.Vertex2( 0.5388397f, 0.3942969f );
            GL.Vertex2( 0.5250737f, 0.3844641f );
            GL.Vertex2( 0.5093412f, 0.3765979f );
            GL.Vertex2( 0.4896755f, 0.366765f );
            GL.Vertex2( 0.4719764f, 0.3588987f );
            GL.Vertex2( 0.4523107f, 0.352999f );
            GL.Vertex2( 0.4346116f, 0.3510324f );
            GL.Vertex2( 0.4188791f, 0.3490659f );
            GL.Vertex2( 0.4031465f, 0.3490659f );
            GL.Vertex2( 0.3874139f, 0.3490659f );
            GL.Vertex2( 0.3677483f, 0.3510324f );
            GL.Vertex2( 0.346116f, 0.3588987f );
            GL.Vertex2( 0.3244838f, 0.3687316f );
            GL.Vertex2( 0.3067847f, 0.3824975f );
            GL.Vertex2( 0.2949852f, 0.3942969f );
            GL.Vertex2( 0.2851524f, 0.4080629f );
            GL.Vertex2( 0.273353f, 0.4178958f );
            GL.Vertex2( 0.259587f, 0.4316618f );
            GL.Vertex2( 0.2438545f, 0.4434611f );
            GL.Vertex2( 0.2261554f, 0.453294f );
            GL.Vertex2( 0.2045231f, 0.46706f );
            GL.Vertex2( 0.1769911f, 0.4788594f );
            GL.Vertex2( 0.23f, 0.58f );
            GL.Vertex2( 0.3264503f, 0.5732546f );
            GL.End();
            #endregion

            GL.Color3( 0.592f, 0.412f, 0.133f );

            #region Left part inner
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.48f, 0.53f );
            GL.Vertex2( -0.3756145f, 0.5162239f );
            GL.Vertex2( -0.3952802f, 0.5201573f );
            GL.Vertex2( -0.4110128f, 0.5280236f );
            GL.Vertex2( -0.4228122f, 0.5417896f );
            GL.Vertex2( -0.4247788f, 0.553589f );
            GL.Vertex2( -0.4267453f, 0.5732546f );
            GL.Vertex2( -0.4247788f, 0.5870206f );
            GL.Vertex2( -0.4188791f, 0.6007866f );
            GL.Vertex2( -0.4110128f, 0.6106195f );
            GL.Vertex2( -0.4011799f, 0.6184857f );
            GL.Vertex2( -0.3952802f, 0.6243855f );
            GL.Vertex2( -0.3834808f, 0.6342183f );
            GL.Vertex2( -0.3815143f, 0.6460177f );
            GL.Vertex2( -0.3874139f, 0.6519174f );
            GL.Vertex2( -0.4011799f, 0.653884f );
            GL.Vertex2( -0.4169125f, 0.6519174f );
            GL.Vertex2( -0.4287119f, 0.6440511f );
            GL.Vertex2( -0.4444444f, 0.6361849f );
            GL.Vertex2( -0.4582104f, 0.6243855f );
            GL.Vertex2( -0.4700098f, 0.612586f );
            GL.Vertex2( -0.4798427f, 0.59882f );
            GL.Vertex2( -0.4896755f, 0.5850541f );
            GL.Vertex2( -0.4995083f, 0.5693215f );
            GL.Vertex2( -0.505408f, 0.5516224f );
            GL.Vertex2( -0.5093412f, 0.539823f );
            GL.Vertex2( -0.5113078f, 0.526057f );
            GL.Vertex2( -0.5093412f, 0.5083579f );
            GL.Vertex2( -0.505408f, 0.4906588f );
            GL.Vertex2( -0.5014749f, 0.4749263f );
            GL.Vertex2( -0.4975418f, 0.4631268f );
            GL.Vertex2( -0.4896755f, 0.4513274f );
            GL.Vertex2( -0.4778756f, 0.4355948f );
            GL.Vertex2( -0.4700098f, 0.4237955f );
            GL.Vertex2( -0.4641101f, 0.4198623f );
            GL.Vertex2( -0.4542773f, 0.4119961f );
            GL.Vertex2( -0.4503441f, 0.4080629f );
            GL.Vertex2( -0.4405113f, 0.4041298f );
            GL.Vertex2( -0.4326451f, 0.3982301f );
            GL.Vertex2( -0.4188791f, 0.3962635f );
            GL.Vertex2( -0.4031465f, 0.3923304f );
            GL.Vertex2( -0.3874139f, 0.3923304f );
            GL.Vertex2( -0.3716814f, 0.3962635f );
            GL.Vertex2( -0.3539823f, 0.4021632f );
            GL.Vertex2( -0.3421829f, 0.4060964f );
            GL.Vertex2( -0.3756145f, 0.5162239f );
            GL.End();

            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.3657817f, 0.4591937f );
            GL.Vertex2( -0.4326451f, 0.3982301f );
            GL.Vertex2( -0.4247788f, 0.3962635f );
            GL.Vertex2( -0.4169125f, 0.3942969f );
            GL.Vertex2( -0.4051131f, 0.3923304f );
            GL.Vertex2( -0.3952802f, 0.3903638f );
            GL.Vertex2( -0.3854474f, 0.3903638f );
            GL.Vertex2( -0.3775811f, 0.3923304f );
            GL.Vertex2( -0.3657817f, 0.3942969f );
            GL.Vertex2( -0.3539823f, 0.3982301f );
            GL.Vertex2( -0.3402163f, 0.4041298f );
            GL.Vertex2( -0.33235f, 0.4080629f );
            GL.Vertex2( -0.3225172f, 0.4139626f );
            GL.Vertex2( -0.3126844f, 0.4178958f );
            GL.Vertex2( -0.3048181f, 0.4257621f );
            GL.Vertex2( -0.2949852f, 0.4336283f );
            GL.Vertex2( -0.287119f, 0.439528f );
            GL.Vertex2( -0.2772861f, 0.4513274f );
            GL.Vertex2( -0.2556539f, 0.4749263f );
            GL.Vertex2( -0.31f, 0.57f );
            GL.Vertex2( -0.3264503f, 0.5496559f );
            GL.Vertex2( -0.3362832f, 0.539823f );
            GL.Vertex2( -0.346116f, 0.5299902f );
            GL.Vertex2( -0.359882f, 0.5221239f );
            GL.Vertex2( -0.373648f, 0.5181907f );
            GL.Vertex2( -0.3893805f, 0.5181907f );
            GL.Vertex2( -0.4051131f, 0.5240905f );
            GL.Vertex2( -0.4188791f, 0.5378565f );
            GL.Vertex2( -0.4700098f, 0.4827925f );
            GL.Vertex2( -0.4326451f, 0.3982301f );
            GL.End();
            #endregion

            #region Middle part inner
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0f, 0.5850541f );
            GL.Vertex2( -0.2713864f, 0.653884f );
            GL.Vertex2( -0.2635202f, 0.6676499f );
            GL.Vertex2( -0.2576205f, 0.6814159f );
            GL.Vertex2( -0.2497542f, 0.6912488f );
            GL.Vertex2( -0.2418879f, 0.7030482f );
            GL.Vertex2( -0.2359882f, 0.7109144f );
            GL.Vertex2( -0.2281219f, 0.7227139f );
            GL.Vertex2( -0.2202557f, 0.7345133f );
            GL.Vertex2( -0.2104228f, 0.7463127f );
            GL.Vertex2( -0.2025565f, 0.7561455f );
            GL.Vertex2( -0.1946903f, 0.7640118f );
            GL.Vertex2( -0.186824f, 0.7738447f );
            GL.Vertex2( -0.1789577f, 0.7777778f );
            GL.Vertex2( -0.1710915f, 0.7836775f );
            GL.Vertex2( -0.1651917f, 0.7876106f );
            GL.Vertex2( -0.1533923f, 0.7954769f );
            GL.Vertex2( -0.1415929f, 0.79941f );
            GL.Vertex2( -0.1297935f, 0.8033432f );
            GL.Vertex2( -0.1179941f, 0.8092428f );
            GL.Vertex2( -0.1061947f, 0.8112094f );
            GL.Vertex2( -0.09832841f, 0.813176f );
            GL.Vertex2( -0.09046214f, 0.8151426f );
            GL.Vertex2( -0.0806293f, 0.8171092f );
            GL.Vertex2( -0.06882989f, 0.8190757f );
            GL.Vertex2( -0.06096362f, 0.8210423f );
            GL.Vertex2( -0.05309734f, 0.8230088f );
            GL.Vertex2( -0.0432645f, 0.8230088f );
            GL.Vertex2( -0.03343166f, 0.8230088f );
            GL.Vertex2( -0.02753196f, 0.8249754f );
            GL.Vertex2( -0.01573255f, 0.8249754f );
            GL.Vertex2( 0f, 0.8249754f );
            GL.Vertex2( 0.01179941f, 0.8249754f );
            GL.Vertex2( 0.02359882f, 0.8230088f );
            GL.Vertex2( 0.0373648f, 0.8210423f );
            GL.Vertex2( 0.05309734f, 0.8190757f );
            GL.Vertex2( 0.06686332f, 0.8171092f );
            GL.Vertex2( 0.0806293f, 0.813176f );
            GL.Vertex2( 0.09046214f, 0.8092428f );
            GL.Vertex2( 0.1042281f, 0.8033432f );
            GL.Vertex2( 0.1219272f, 0.7974436f );
            GL.Vertex2( 0.1317601f, 0.7954769f );
            GL.Vertex2( 0.1415929f, 0.7895772f );
            GL.Vertex2( 0.1533923f, 0.7856441f );
            GL.Vertex2( 0.1651917f, 0.7797443f );
            GL.Vertex2( 0.1750246f, 0.7718781f );
            GL.Vertex2( 0.1848574f, 0.7640118f );
            GL.Vertex2( 0.1927237f, 0.7581121f );
            GL.Vertex2( 0.1946903f, 0.7561455f );
            GL.Vertex2( 0.20059f, 0.7522124f );
            GL.Vertex2( 0.2104228f, 0.7443461f );
            GL.Vertex2( 0.2163225f, 0.7384464f );
            GL.Vertex2( 0.2241888f, 0.7286136f );
            GL.Vertex2( 0.2320551f, 0.7207473f );
            GL.Vertex2( 0.2359882f, 0.712881f );
            GL.Vertex2( 0.2399213f, 0.7030482f );
            GL.Vertex2( 0.245821f, 0.6951819f );
            GL.Vertex2( 0.2517208f, 0.6833825f );
            GL.Vertex2( 0.2576205f, 0.6715831f );
            GL.Vertex2( 0.2674533f, 0.640118f );
            GL.End();
            #endregion

            #region Right part inner
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0.4483775f, 0.4021632f );
            GL.Vertex2( 0.3205507f, 0.5575221f );
            GL.Vertex2( 0.3264503f, 0.5516224f );
            GL.Vertex2( 0.3343166f, 0.5417896f );
            GL.Vertex2( 0.3421829f, 0.5339233f );
            GL.Vertex2( 0.3500492f, 0.526057f );
            GL.Vertex2( 0.359882f, 0.5201573f );
            GL.Vertex2( 0.3677483f, 0.5162242f );
            GL.Vertex2( 0.3775811f, 0.5103245f );
            GL.Vertex2( 0.3854474f, 0.5044248f );
            GL.Vertex2( 0.3952802f, 0.5004916f );
            GL.Vertex2( 0.4031465f, 0.4965585f );
            GL.Vertex2( 0.4149459f, 0.4926254f );
            GL.Vertex2( 0.4228122f, 0.4886922f );
            GL.Vertex2( 0.4306785f, 0.4867257f );
            GL.Vertex2( 0.4405113f, 0.4827925f );
            GL.Vertex2( 0.4483776f, 0.4827925f );
            GL.Vertex2( 0.4562438f, 0.4788594f );
            GL.Vertex2( 0.4621436f, 0.4788594f );
            GL.Vertex2( 0.4700098f, 0.4788594f );
            GL.Vertex2( 0.4778761f, 0.4768928f );
            GL.Vertex2( 0.4936087f, 0.4768928f );
            GL.Vertex2( 0.5073747f, 0.4768928f );
            GL.Vertex2( 0.5231073f, 0.480826f );
            GL.Vertex2( 0.5309734f, 0.4867257f );
            GL.Vertex2( 0.5408063f, 0.4926254f );
            GL.Vertex2( 0.5486726f, 0.4985251f );
            GL.Vertex2( 0.5545722f, 0.5044248f );
            GL.Vertex2( 0.5624385f, 0.5103245f );
            GL.Vertex2( 0.5703048f, 0.5162242f );
            GL.Vertex2( 0.5762045f, 0.5201573f );
            GL.Vertex2( 0.5821042f, 0.5240905f );
            GL.Vertex2( 0.5880039f, 0.526057f );
            GL.Vertex2( 0.5939037f, 0.5240905f );
            GL.Vertex2( 0.5978368f, 0.5221239f );
            GL.Vertex2( 0.6017699f, 0.5162242f );
            GL.Vertex2( 0.6076697f, 0.5083579f );
            GL.Vertex2( 0.6076697f, 0.4985251f );
            GL.Vertex2( 0.6057031f, 0.4886922f );
            GL.Vertex2( 0.5998034f, 0.4768928f );
            GL.Vertex2( 0.5958703f, 0.4709931f );
            GL.Vertex2( 0.5899705f, 0.4611603f );
            GL.Vertex2( 0.5840707f, 0.4513274f );
            GL.Vertex2( 0.5742379f, 0.4414946f );
            GL.Vertex2( 0.5663717f, 0.4316618f );
            GL.Vertex2( 0.5585054f, 0.4237955f );
            GL.Vertex2( 0.5486726f, 0.4159292f );
            GL.Vertex2( 0.5408064f, 0.4100295f );
            GL.Vertex2( 0.5290068f, 0.4021632f );
            GL.Vertex2( 0.5211406f, 0.3982301f );
            GL.Vertex2( 0.5132743f, 0.3942969f );
            GL.Vertex2( 0.505408f, 0.3883972f );
            GL.Vertex2( 0.4955752f, 0.3844641f );
            GL.Vertex2( 0.4857424f, 0.3785644f );
            GL.Vertex2( 0.4719764f, 0.3746313f );
            GL.Vertex2( 0.460177f, 0.3706981f );
            GL.Vertex2( 0.446411f, 0.366765f );
            GL.Vertex2( 0.4326451f, 0.3628319f );
            GL.Vertex2( 0.4169125f, 0.3628319f );
            GL.Vertex2( 0.4051131f, 0.3628319f );
            GL.Vertex2( 0.3815143f, 0.3628319f );
            GL.Vertex2( 0.3638151f, 0.366765f );
            GL.Vertex2( 0.3520157f, 0.3726647f );
            GL.Vertex2( 0.3402163f, 0.3785644f );
            GL.Vertex2( 0.3225172f, 0.3903638f );
            GL.Vertex2( 0.3048181f, 0.4041298f );
            GL.Vertex2( 0.2930187f, 0.4178958f );
            GL.Vertex2( 0.2772861f, 0.4336283f );
            GL.Vertex2( 0.2556539f, 0.4493609f );
            GL.Vertex2( 0.2300885f, 0.4631268f );
            GL.Vertex2( 0.2084562f, 0.494592f );
            GL.Vertex2( 0.3205507f, 0.5575221f );
            GL.End();
            #endregion
        }

        private static void DrawGlasses()
        {
            #region Frame
            GL.Color3( 0.439f, 0.149f, 0.157f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0, 0.5201573f );
            GL.Vertex2( -0.001966568f, 0.640118f );
            GL.Vertex2( 0f, 0.6479843f );
            GL.Vertex2( 0.003933137f, 0.6578171f );
            GL.Vertex2( 0.009832842f, 0.6656834f );
            GL.Vertex2( 0.01376598f, 0.6735497f );
            GL.Vertex2( 0.02163225f, 0.6814159f );
            GL.Vertex2( 0.02949852f, 0.6892822f );
            GL.Vertex2( 0.0373648f, 0.6971485f );
            GL.Vertex2( 0.04523107f, 0.699115f );
            GL.Vertex2( 0.05113078f, 0.7030482f );
            GL.Vertex2( 0.06096362f, 0.7089479f );
            GL.Vertex2( 0.07079646f, 0.7109144f );
            GL.Vertex2( 0.08456244f, 0.7109144f );
            GL.Vertex2( 0.09439528f, 0.7109144f );
            GL.Vertex2( 0.1042281f, 0.7089479f );
            GL.Vertex2( 0.1160275f, 0.7069813f );
            GL.Vertex2( 0.1278269f, 0.7050148f );
            GL.Vertex2( 0.1396263f, 0.7030482f );
            GL.Vertex2( 0.1533923f, 0.699115f );
            GL.Vertex2( 0.1651917f, 0.6971485f );
            GL.Vertex2( 0.1769911f, 0.6932153f );
            GL.Vertex2( 0.1887906f, 0.6912488f );
            GL.Vertex2( 0.20059f, 0.6873156f );
            GL.Vertex2( 0.2084562f, 0.685349f );
            GL.Vertex2( 0.2202557f, 0.6814159f );
            GL.Vertex2( 0.2320551f, 0.6755162f );
            GL.Vertex2( 0.2438545f, 0.6715831f );
            GL.Vertex2( 0.2536873f, 0.6656834f );
            GL.Vertex2( 0.2654867f, 0.6617503f );
            GL.Vertex2( 0.2753195f, 0.6558505f );
            GL.Vertex2( 0.2851524f, 0.6519174f );
            GL.Vertex2( 0.2930187f, 0.6479843f );
            GL.Vertex2( 0.3028515f, 0.6420845f );
            GL.Vertex2( 0.3107178f, 0.6361849f );
            GL.Vertex2( 0.3185841f, 0.626352f );
            GL.Vertex2( 0.3225172f, 0.6165192f );
            GL.Vertex2( 0.3264503f, 0.6066864f );
            GL.Vertex2( 0.3264503f, 0.5948869f );
            GL.Vertex2( 0.3225172f, 0.5771878f );
            GL.Vertex2( 0.3205507f, 0.567355f );
            GL.Vertex2( 0.3185841f, 0.5555556f );
            GL.Vertex2( 0.314651f, 0.5339233f );
            GL.Vertex2( 0.3087512f, 0.5181907f );
            GL.Vertex2( 0.3028515f, 0.5044248f );
            GL.Vertex2( 0.2969518f, 0.4906588f );
            GL.Vertex2( 0.2910521f, 0.4788594f );
            GL.Vertex2( 0.287119f, 0.46706f );
            GL.Vertex2( 0.2792527f, 0.4591937f );
            GL.Vertex2( 0.2694198f, 0.4513274f );
            GL.Vertex2( 0.259587f, 0.4513274f );
            GL.Vertex2( 0.2477876f, 0.4552606f );
            GL.Vertex2( 0.2281219f, 0.4611603f );
            GL.Vertex2( 0.2104228f, 0.46706f );
            GL.Vertex2( 0.1927237f, 0.4709931f );
            GL.Vertex2( 0.173058f, 0.4749263f );
            GL.Vertex2( 0.1514258f, 0.4788594f );
            GL.Vertex2( 0.1317601f, 0.4827925f );
            GL.Vertex2( 0.1120944f, 0.4847591f );
            GL.Vertex2( 0.09046214f, 0.4867257f );
            GL.Vertex2( 0.07079646f, 0.4886922f );
            GL.Vertex2( 0.05113078f, 0.4906588f );
            GL.Vertex2( 0.03539823f, 0.4906588f );
            GL.Vertex2( 0.01966568f, 0.4926254f );
            GL.Vertex2( -0.001966568f, 0.4926254f );
            GL.Vertex2( -0.02163225f, 0.4926254f );
            GL.Vertex2( -0.03343166f, 0.4906588f );
            GL.Vertex2( -0.05309734f, 0.4906588f );
            GL.Vertex2( -0.07866274f, 0.4886922f );
            GL.Vertex2( -0.1061947f, 0.4867257f );
            GL.Vertex2( -0.1337266f, 0.4827925f );
            GL.Vertex2( -0.1553589f, 0.4788594f );
            GL.Vertex2( -0.1789577f, 0.4749263f );
            GL.Vertex2( -0.1986234f, 0.4709931f );
            GL.Vertex2( -0.2163225f, 0.46706f );
            GL.Vertex2( -0.2340216f, 0.4631268f );
            GL.Vertex2( -0.2497542f, 0.4552606f );
            GL.Vertex2( -0.2654867f, 0.4513274f );
            GL.Vertex2( -0.2792527f, 0.453294f );
            GL.Vertex2( -0.2890855f, 0.4611603f );
            GL.Vertex2( -0.2949852f, 0.4768928f );
            GL.Vertex2( -0.3028515f, 0.4926254f );
            GL.Vertex2( -0.3126844f, 0.5142576f );
            GL.Vertex2( -0.3205507f, 0.5417896f );
            GL.Vertex2( -0.3284169f, 0.5693215f );
            GL.Vertex2( -0.3303835f, 0.5870206f );
            GL.Vertex2( -0.33235f, 0.6047198f );
            GL.Vertex2( -0.3264503f, 0.6184857f );
            GL.Vertex2( -0.3185841f, 0.6322517f );
            GL.Vertex2( -0.3067847f, 0.640118f );
            GL.Vertex2( -0.2949853f, 0.6479843f );
            GL.Vertex2( -0.2831858f, 0.6558505f );
            GL.Vertex2( -0.2713864f, 0.6617503f );
            GL.Vertex2( -0.2556539f, 0.6676499f );
            GL.Vertex2( -0.245821f, 0.6735497f );
            GL.Vertex2( -0.2300885f, 0.6814159f );
            GL.Vertex2( -0.214356f, 0.685349f );
            GL.Vertex2( -0.1946903f, 0.6912488f );
            GL.Vertex2( -0.1789577f, 0.6951819f );
            GL.Vertex2( -0.1651917f, 0.6991151f );
            GL.Vertex2( -0.1494592f, 0.7010816f );
            GL.Vertex2( -0.1376598f, 0.7030482f );
            GL.Vertex2( -0.1238938f, 0.7050148f );
            GL.Vertex2( -0.1061947f, 0.7089479f );
            GL.Vertex2( -0.09046214f, 0.7089479f );
            GL.Vertex2( -0.0747296f, 0.7089479f );
            GL.Vertex2( -0.06489675f, 0.7069813f );
            GL.Vertex2( -0.05309734f, 0.7010816f );
            GL.Vertex2( -0.04129793f, 0.6951819f );
            GL.Vertex2( -0.03539823f, 0.6892822f );
            GL.Vertex2( -0.02556539f, 0.6814159f );
            GL.Vertex2( -0.01769911f, 0.6715831f );
            GL.Vertex2( -0.01179941f, 0.6597837f );
            GL.Vertex2( -0.005899705f, 0.6479843f );
            GL.Vertex2( 0.0f, 0.640118f );
            GL.End();
            #endregion

            #region Glass
            GL.Color3( 0.82f, 0.808f, 0.812f );
            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( -0.1573255f, 0.5850541f );
            GL.Vertex2( -0.1612586f, 0.6302851f );
            GL.Vertex2( -0.1494592f, 0.6342183f );
            GL.Vertex2( -0.1396263f, 0.6361849f );
            GL.Vertex2( -0.1278269f, 0.6381514f );
            GL.Vertex2( -0.1160275f, 0.6381514f );
            GL.Vertex2( -0.1081613f, 0.640118f );
            GL.Vertex2( -0.09832841f, 0.640118f );
            GL.Vertex2( -0.09046214f, 0.6361849f );
            GL.Vertex2( -0.08259587f, 0.6302851f );
            GL.Vertex2( -0.07276304f, 0.6165191f );
            GL.Vertex2( -0.06489675f, 0.5988199f );
            GL.Vertex2( -0.06293019f, 0.5870206f );
            GL.Vertex2( -0.06489675f, 0.5712881f );
            GL.Vertex2( -0.06882989f, 0.5594887f );
            GL.Vertex2( -0.07669616f, 0.5516224f );
            GL.Vertex2( -0.08849557f, 0.5476893f );
            GL.Vertex2( -0.100295f, 0.5476893f );
            GL.Vertex2( -0.114061f, 0.5437561f );
            GL.Vertex2( -0.1337266f, 0.5417896f );
            GL.Vertex2( -0.1514258f, 0.5378565f );
            GL.Vertex2( -0.1651917f, 0.5358899f );
            GL.Vertex2( -0.1809243f, 0.5319567f );
            GL.Vertex2( -0.1986234f, 0.5280236f );
            GL.Vertex2( -0.2104228f, 0.5240905f );
            GL.Vertex2( -0.2202557f, 0.5221239f );
            GL.Vertex2( -0.2281219f, 0.5221239f );
            GL.Vertex2( -0.2320551f, 0.526057f );
            GL.Vertex2( -0.2379548f, 0.5378565f );
            GL.Vertex2( -0.2438545f, 0.5555556f );
            GL.Vertex2( -0.2497542f, 0.571288f );
            GL.Vertex2( -0.2517208f, 0.5889872f );
            GL.Vertex2( -0.2497542f, 0.6047198f );
            GL.Vertex2( -0.2418879f, 0.6086529f );
            GL.Vertex2( -0.2300885f, 0.612586f );
            GL.Vertex2( -0.2202557f, 0.6184857f );
            GL.Vertex2( -0.2084562f, 0.6204523f );
            GL.Vertex2( -0.1986234f, 0.6243855f );
            GL.Vertex2( -0.186824f, 0.626352f );
            GL.Vertex2( -0.1750246f, 0.6302851f );
            GL.Vertex2( -0.1612586f, 0.6302851f );
            GL.End();

            GL.Begin( PrimitiveType.TriangleFan );
            GL.Vertex2( 0.1553589f, 0.5850541f );
            GL.Vertex2( 0.1042281f, 0.640118f );
            GL.Vertex2( 0.1179941f, 0.6381514f );
            GL.Vertex2( 0.1317601f, 0.6361849f );
            GL.Vertex2( 0.1474926f, 0.6342183f );
            GL.Vertex2( 0.1632252f, 0.6302851f );
            GL.Vertex2( 0.1789577f, 0.6283186f );
            GL.Vertex2( 0.1907571f, 0.6243855f );
            GL.Vertex2( 0.20059f, 0.6224189f );
            GL.Vertex2( 0.2104228f, 0.6184857f );
            GL.Vertex2( 0.2222222f, 0.6145526f );
            GL.Vertex2( 0.2300885f, 0.6106195f );
            GL.Vertex2( 0.2359882f, 0.6086529f );
            GL.Vertex2( 0.2399213f, 0.6047198f );
            GL.Vertex2( 0.2438545f, 0.59882f );
            GL.Vertex2( 0.245821f, 0.5889872f );
            GL.Vertex2( 0.2438545f, 0.5771878f );
            GL.Vertex2( 0.2418879f, 0.5653884f );
            GL.Vertex2( 0.2399213f, 0.553589f );
            GL.Vertex2( 0.2359882f, 0.5437561f );
            GL.Vertex2( 0.2300885f, 0.5319567f );
            GL.Vertex2( 0.2261554f, 0.5240905f );
            GL.Vertex2( 0.2222222f, 0.5221239f );
            GL.Vertex2( 0.2123894f, 0.5201573f );
            GL.Vertex2( 0.20059f, 0.526057f );
            GL.Vertex2( 0.1848574f, 0.5299902f );
            GL.Vertex2( 0.1710915f, 0.5339233f );
            GL.Vertex2( 0.1533923f, 0.5378565f );
            GL.Vertex2( 0.1297935f, 0.5417896f );
            GL.Vertex2( 0.1081613f, 0.5437561f );
            GL.Vertex2( 0.09046214f, 0.5476893f );
            GL.Vertex2( 0.07669616f, 0.5496559f );
            GL.Vertex2( 0.06686332f, 0.5555556f );
            GL.Vertex2( 0.05899705f, 0.5712881f );
            GL.Vertex2( 0.05703048f, 0.5830875f );
            GL.Vertex2( 0.05899705f, 0.59882f );
            GL.Vertex2( 0.06489675f, 0.6106195f );
            GL.Vertex2( 0.07079646f, 0.6243855f );
            GL.Vertex2( 0.0806293f, 0.6322517f );
            GL.Vertex2( 0.08652901f, 0.6381514f );
            GL.Vertex2( 0.09439528f, 0.640118f );
            GL.Vertex2( 0.1042281f, 0.640118f );

            GL.End();
            #endregion
        }

        private static void DrawCircle( float centerX, float centerY, float radius, float width )
        {
            const float step = (float) Math.PI / 180;

            GL.Begin( PrimitiveType.QuadStrip );
            for ( float angle = 0; angle < 2 * Math.PI; )
            {
                GL.Vertex2(
                    radius * (float) Math.Cos( angle ) + centerX,
                    radius * (float) Math.Sin( angle ) + centerY );

                angle += step;

                GL.Vertex2(
                    ( radius + width ) * (float) Math.Cos( angle ) + centerX,
                    ( radius + width ) * (float) Math.Sin( angle ) + centerY );

                angle += step;
            }
            GL.End();
        }

        private static void DrawFilledCircle( float centerX, float centerY, float radius )
        {
            DrawFilledCircle( centerX, centerY, radius, radius );
        }

        private static void DrawFilledCircle( float centerX, float centerY, float width, float height )
        {
            const float step = (float) Math.PI / 180;

            GL.Begin( PrimitiveType.TriangleFan );
            for ( float angle = 0; angle < 2 * Math.PI; angle += step )
            {
                GL.Vertex2(
                    width * (float) Math.Cos( angle ) + centerX,
                    height * (float) Math.Sin( angle ) + centerY );
            }
            GL.End();
        }

        private static void DrawFilledBezierCurve( params Vector2[] points )
        {
            BezierCurve bezierCurve = new BezierCurve( points );

            GL.Begin( PrimitiveType.TriangleFan );
            for ( float i = 0; i <= 1; i += 0.01f )
            {
                Vector2 point = bezierCurve.CalculatePoint( i );
                GL.Vertex2( point.X, point.Y );
            }
            GL.End();
        }

        // TODO: Delete after drawing actual character
        private static Vector2[] _points = new Vector2[] { };

        private void Window_KeyDown( object sender, KeyboardKeyEventArgs e )
        {
            switch ( e.Key )
            {
                case Key.S:
                    using ( StreamWriter sw = new StreamWriter( "Vertices.txt" ) )
                    {
                        foreach ( var point in _points )
                        {
                            string x = point.X.ToString( CultureInfo.GetCultureInfo( "en-US" ) );
                            string y = point.Y.ToString( CultureInfo.GetCultureInfo( "en-US" ) );
                            sw.WriteLine( $"GL.Vertex2( {x}f, {y}f );" );
                        }
                    }
                    Console.WriteLine( "Vertices saved!" );
                    break;
            }
        }

        private static int? _draggedIndex = null;

        private void Window_MouseDown( object sender, MouseButtonEventArgs e )
        {
            float centerX = (float) Width / 2;
            float centerY = (float) Height / 2;

            float mouseX = ( e.X - centerX ) / centerX;
            float mouseY = -( e.Y - centerY ) / centerY;

            float aspectRatio = GetAspectRatio();

            if ( aspectRatio >= 1.0 )
            {
                mouseX *= aspectRatio;
            }
            else
            {
                mouseY /= aspectRatio;
            }

            Console.WriteLine( $"X: {mouseX} " + $"Y: {mouseY}" );

            float pw = (float) 10 / Width;
            float ph = (float) 10 / Height;

            switch ( e.Button )
            {
                case MouseButton.Left:
                    for ( int i = 0; i <= _points.Length - 1; i++ )
                    {
                        Vector2 point = _points[ i ];
                        if ( point.X - pw < mouseX && mouseX < point.X + pw && point.Y - ph < mouseY && mouseY < point.Y + ph )
                        {
                            _draggedIndex = i;
                        }
                    }
                    break;
                case MouseButton.Right:
                    _points = _points.Append( new Vector2( mouseX, mouseY ) ).ToArray();
                    break;
            }
        }

        private void Window_MouseMove( object sender, MouseMoveEventArgs e )
        {
            float aspectRatio = GetAspectRatio();

            if ( _draggedIndex != null )
            {
                Vector2 point = _points[ (int) _draggedIndex ];

                float deltaX = e.XDelta;
                float deltaY = e.YDelta;

                if ( aspectRatio >= 1.0 )
                {
                    deltaX *= aspectRatio;
                }
                else
                {
                    deltaY /= aspectRatio;
                }

                point.X += deltaX / Width * 2;
                point.Y -= deltaY / Height * 2;

                _points[ (int) _draggedIndex ] = new Vector2( point.X, point.Y );
            }
        }

        private void Window_MouseUp( object sender, MouseButtonEventArgs e )
        {
            _draggedIndex = null;
        }

        private float GetAspectRatio()
        {
            return (float) Width / Height;
        }

        private int LoadPinImage()
        {
            Bitmap bitmap = new Bitmap( @"C:\Users\zombi\Downloads\Pin.jpg" );

            int tex;
            GL.Hint( HintTarget.PerspectiveCorrectionHint, HintMode.Nicest );

            GL.GenTextures( 1, out tex );
            GL.BindTexture( TextureTarget.Texture2D, tex );

            BitmapData data = bitmap.LockBits( new Rectangle( 0, 0, bitmap.Width, bitmap.Height ),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0 );
            bitmap.UnlockBits( data );


            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat );

            return tex;
        }

        public static void DrawImage( int image )
        {
            GL.Enable( EnableCap.Texture2D );
            GL.BindTexture( TextureTarget.Texture2D, image );

            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha );

            GL.Color4( 1.0, 1.0, 1.0, 1 );

            GL.Begin( PrimitiveType.Quads );

            GL.TexCoord2( 0, 0 );
            GL.Vertex2( -1, 1 );

            GL.TexCoord2( 1, 0 );
            GL.Vertex2( 1, 1 );

            GL.TexCoord2( 1, 1 );
            GL.Vertex2( 1, -1 );

            GL.TexCoord2( 0, 1 );
            GL.Vertex2( -1, -1 );

            GL.End();

            GL.Disable( EnableCap.Blend );

            GL.Disable( EnableCap.Texture2D );
        }

        private static void DrawControlPoints()
        {
            GL.PointSize( 10 );
            GL.Begin( PrimitiveType.Points );
            for ( int i = 0; i <= _points.Length - 1; i++ )
            {
                GL.Color3( 1.0f, 0.0f, 0.0f );
                GL.Vertex2( _points[ i ].X, _points[ i ].Y );
            }
            GL.End();
        }

        private static void ConnectControlPoints()
        {
            Vector2? prev = null;

            GL.PointSize( 1 );
            GL.Color3( 0.0f, 0.0f, 0.0f );
            GL.Begin( PrimitiveType.Points );
            foreach ( Vector2 p in _points )
            {
                if ( prev == null )
                {
                    GL.Vertex2( p.X, p.Y );
                }
                else
                {
                    for ( float t = 0; t <= 1; t += 0.0001f )
                    {
                        GL.Vertex2( Lerp( prev.Value.X, p.X, t ), Lerp( prev.Value.Y, p.Y, t ) );
                    }
                }
                prev = p;
            }
            GL.End();
        }

        static public float Lerp( float v0, float v1, float t )
        {
            return ( 1 - t ) * v0 + t * v1;
        }
    }
}
