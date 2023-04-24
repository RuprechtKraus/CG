using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Task1.Objects;

namespace Task1
{
    internal class Window : GameWindow
    {
        private const float FieldOfView = 60.0f * (float) Math.PI / 180.0f;
        private const float ZNear = 0.5f;
        private const float ZFar = 10.0f;
        private const float FrustumSize = 2.0f;
        private const float DistanceToOrigin = 2.0f;
        private const float CameraSpeed = 0.01f;

        public float AspectRatio => (float) Width / Height;

        private Cube _cube = new Cube( 0.5f );

        private Vector3 position = new Vector3( 0.0f, 0.0f, 1.0f );
        private Vector3 front = new Vector3( 0.0f, 0.0f, -1.0f );
        private Vector3 up = new Vector3( 0.0f, 1.0f, 0.0f );

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

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            GL.ClearColor( 0.0f, 0.0f, 0.0f, 1.0f );
            GL.Enable( EnableCap.CullFace );
            GL.CullFace( CullFaceMode.Back );
            GL.FrontFace( FrontFaceDirection.Ccw );
            GL.Enable( EnableCap.DepthTest );

            Matrix4 view = Matrix4.LookAt( position, position + front, up );
            GL.LoadMatrix( ref view );

            MouseMove += Window_MouseMove;
            KeyDown += Window_KeyDown;
        }

        private void Window_KeyDown( object sender, KeyboardKeyEventArgs e )
        {
            if ( !Focused )
            {
                return;
            }

            switch ( e.Key )
            {
                case Key.W:
                    TryToMoveCamera( position + front * 0.01f );
                    break;
                case Key.S:
                    TryToMoveCamera( position - front * 0.01f );
                    break;
                case Key.A:
                    TryToMoveCamera( position - Vector3.Normalize( Vector3.Cross( front, up ) ) * 0.02f );
                    break;
                case Key.D:
                    TryToMoveCamera( position + Vector3.Normalize( Vector3.Cross( front, up ) ) * 0.02f );
                    break;
                case Key.Space:
                    TryToMoveCamera( position + up * 0.01f );
                    break;
                case Key.ShiftLeft:
                    TryToMoveCamera( position - up * 0.01f );
                    break;
                default:
                    return;
            }
        }

        private void TryToMoveCamera( Vector3 point )
        {
            if ( !_cube.CheckCollision( point ) )
            {
                position = point;
            }
        }

        private float _yaw = -90;
        private float _pitch = 0;

        private void Window_MouseMove( object sender, MouseMoveEventArgs e )
        {
            if ( e.Mouse.LeftButton == ButtonState.Pressed )
            {
                //float rotateX = (float) e.YDelta * 180 / Height;
                //float rotateY = (float) e.XDelta * 180 / Width;

                //RotateCamera( rotateX, rotateY, e.XDelta, e.YDelta );

                if ( _pitch > 89.0f )
                {
                    _pitch = 89.0f;
                }
                else if ( _pitch < -89.0f )
                {
                    _pitch = -89.0f;
                }
                else
                {
                    _yaw += e.XDelta * 0.2f;
                    _pitch -= e.YDelta * 0.2f;
                }

                front.X = (float) Math.Cos( MathHelper.DegreesToRadians( _pitch ) ) * (float) Math.Cos( MathHelper.DegreesToRadians( _yaw ) );
                front.Y = (float) Math.Sin( MathHelper.DegreesToRadians( _pitch ) );
                front.Z = (float) Math.Cos( MathHelper.DegreesToRadians( _pitch ) ) * (float) Math.Sin( MathHelper.DegreesToRadians( _yaw ) );
                front.Normalize();
            }
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            base.OnUpdateFrame( e );
            Console.WriteLine( position.Z );
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
            var fov = Matrix4.CreatePerspectiveFieldOfView( FieldOfView, AspectRatio, 0.01f, ZFar );
            GL.LoadMatrix( ref fov );
            //GL.LoadIdentity();
            //GL.Frustum(
            //    -frustumWidth * 0.5f, frustumWidth * 0.5f,
            //    -frustumHeight * 0.5f, frustumHeight * 0.5f,
            //    FrustumSize * 0.5f, FrustumSize * 20 );
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

            //Matrix4 view = Matrix4.LookAt( position, position + front, up );

            Matrix4 view = Matrix4.LookAt( position, position + front, up );
            GL.LoadMatrix( ref view );

            _cube.Draw();

            SwapBuffers();
        }

        private void RotateCamera( float rotateX, float rotateY, float dx, float dy )
        {
            GL.GetFloat( GetPName.ModelviewMatrix, out Matrix4 modelView );

            Vector3 xAxis = new Vector3( modelView.M11, modelView.M21, modelView.M31 );
            Vector3 yAxis = new Vector3( modelView.M12, modelView.M22, modelView.M32 );

            front.X += dx / Width * 3;
            front.Y -= dy / Height * 3;
            //Matrix4 view = Matrix4.LookAt( position, position + front, up );
            //GL.LoadMatrix( ref view );

            //GL.Rotate( rotateX, xAxis.X, xAxis.Y, xAxis.Z );
            //GL.Rotate( rotateY, yAxis.X, yAxis.Y, yAxis.Z );

            //NormalizeModelViewMatrix();
        }

        private void NormalizeModelViewMatrix()
        {
            GL.GetFloat( GetPName.ModelviewMatrix, out Matrix4 modelView );

            Vector3 xAxis = new Vector3( modelView.M11, modelView.M21, modelView.M31 );
            xAxis.Normalize();
            Vector3 yAxis = new Vector3( modelView.M12, modelView.M22, modelView.M32 );
            yAxis.Normalize();

            Vector3 zAxis = Vector3.Cross( xAxis, yAxis );
            zAxis.Normalize();
            xAxis = Vector3.Cross( yAxis, zAxis );
            xAxis.Normalize();
            yAxis = Vector3.Cross( zAxis, xAxis );
            yAxis.Normalize();

            modelView.M11 = xAxis.X;
            modelView.M21 = xAxis.Y;
            modelView.M31 = xAxis.Z;
            modelView.M12 = yAxis.X;
            modelView.M22 = yAxis.Y;
            modelView.M23 = yAxis.Z;
            modelView.M13 = zAxis.X;
            modelView.M23 = zAxis.Y;
            modelView.M33 = zAxis.Z;

            //var view = Matrix4.LookAt( _camera.Row0, _camera.Row1, _camera.Row2 );
            //modelView = modelView * view;
            GL.LoadMatrix( ref modelView );
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
