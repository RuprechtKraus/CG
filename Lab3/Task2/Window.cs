using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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

            _image = LoadPinImage();

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

            DrawControlPoints();
            ConnectControlPoints();
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
            const float step = (float) Math.PI / 180;

            GL.Begin( PrimitiveType.TriangleFan );
            for ( float angle = 0; angle < 2 * Math.PI; angle += step )
            {
                GL.Vertex2(
                    radius * (float) Math.Cos( angle ) + centerX,
                    radius * (float) Math.Sin( angle ) + centerY );
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

            GL.Color4( 1.0, 1.0, 1.0, 0.4 );

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
