using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Task1.Objects;
using System.Threading;

namespace Task1
{
    internal class Window : GameWindow
    {
        private const float FieldOfView = 60.0f * (float) Math.PI / 180.0f;
        private const float ZNear = 0.5f;
        private const float ZFar = 10.0f;
        private const float FrustumSize = 2.0f;
        private const float DistanceToOrigin = 2.0f;

        public float AspectRatio => (float) Width / Height;

        private Cube _cube = new Cube( 0.5f );
        private bool _leftButtonPressed = false;

        public Window( int width, int height, string title )
            : base( width, height, GraphicsMode.Default, title )
        {
            _cube.SetSideColor( CubeSide.NEGATIVE_X, 1.0f, 0.0f, 0.0f, 1.0f );
            _cube.SetSideColor( CubeSide.POSITIVE_X, 0.0f, 1.0f, 0.0f, 1.0f );
            _cube.SetSideColor( CubeSide.NEGATIVE_Y, 0.0f, 0.0f, 1.0f, 1.0f );
            _cube.SetSideColor( CubeSide.POSITIVE_Y, 1.0f, 1.0f, 0.0f, 1.0f );
            _cube.SetSideColor( CubeSide.NEGATIVE_Z, 0.0f, 1.0f, 1.0f, 1.0f );
            _cube.SetSideColor( CubeSide.POSITIVE_Z, 1.0f, 0.0f, 1.0f, 1.0f );
        }

        private bool _left = true;

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            GL.ClearColor( 0.0f, 0.0f, 0.0f, 1.0f );
            GL.Enable( EnableCap.CullFace );
            GL.CullFace( CullFaceMode.Back );
            GL.FrontFace( FrontFaceDirection.Ccw );
            GL.Enable( EnableCap.DepthTest );
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

            float frustumHeight = FrustumSize;
            float frustumWidth = FrustumSize * AspectRatio;

            if ( frustumWidth < FrustumSize && AspectRatio != 0 )
            {
                frustumWidth = FrustumSize;
                frustumHeight = FrustumSize / AspectRatio;
            }

            GL.MatrixMode( MatrixMode.Projection );
            //var fov = Matrix4.CreatePerspectiveFieldOfView( FieldOfView, AspectRatio, ZNear, ZFar );
            //GL.LoadMatrix( ref fov );
            GL.LoadIdentity();
            GL.Frustum(
                -frustumWidth * 0.5f, frustumWidth * 0.5f,
                -frustumHeight * 0.5f, frustumHeight * 0.5f,
                FrustumSize * 0.5f, FrustumSize * 20 );
            GL.MatrixMode( MatrixMode.Modelview );

            DrawFrame();
        }

        private void DrawFrame()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );

            //var view = Matrix4.LookAt(
            //    new Vector3( 1.0f, 2.0f, 0.2f ),
            //    new Vector3( 0.0f, 0.0f, 0.0f ),
            //    new Vector3( 0.0f, 0.0f, -1.0f ) );
            //GL.LoadMatrix( ref view );

            //GL.Color3( 1.0f, 0.0f, 0.0f );
            //GL.Rect( -1, -1, 1, 1 );

            //GL.Translate( 0.0f, 0.0f, 0.2f );
            //GL.Rotate( 60, 0.0f, 1.0f, 0.0f );

            //GL.Color3( 0.0f, 0.0f, 1.0f );
            //GL.Rect( -0.5f, -0.5f, 0.5f, 0.5f );

            var view = Matrix4.LookAt(
                new Vector3( 2.0f, 2.0f, 2.0f ),
                new Vector3( 0.0f, 0.0f, 0.0f ),
                new Vector3( 0.0f, 1.0f, 0.0f ) );
            GL.LoadMatrix( ref view );

            _cube.Draw();

            SwapBuffers();
        }

        private void SetupProjectionMatrix()
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadIdentity();

            if ( AspectRatio > 1.0 )
            {
                GL.Ortho( -1.0 * AspectRatio, 1.0 * AspectRatio, -1.0, 1.0, -1.0, 1.0 );
            }
            else
            {
                GL.Ortho( -1.0, 1.0, -1.0 / AspectRatio, 1.0 / AspectRatio, -1.0, 1.0 );
            }

            GL.MatrixMode( MatrixMode.Modelview );
        }
    }
}
