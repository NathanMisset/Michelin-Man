#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Opdracht6_Transformations
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SimPhyGameWorld : Game
    {
        GraphicsDeviceManager graphDev;
        Color background = new Color(20, 0, 60);
        public static SimPhyGameWorld World;
        Vector3 cameraPosition = new Vector3(0, 30, 80);
        public Matrix View;
        public Matrix Projection;
        public static GraphicsDevice Graphics;

        List<Sphere> spheres;

        Sphere body1;
        Sphere body2;
        Sphere body3;
        Sphere body4;

        Sphere neck;
        Sphere head1;
        Sphere head2;

        Sphere arm11;
        Sphere arm12;
        Sphere arm13;
        Sphere hand1;

        Sphere arm21;
        Sphere arm22;
        Sphere arm23;
        Sphere hand2;

        Sphere leg11;
        Sphere leg12;
        Sphere leg13;
        Sphere foot1;

        Sphere leg21;
        Sphere leg22;
        Sphere leg23;
        Sphere foot2;


        float rarm;

        Matrix matrixArm11;
        Matrix matrixArm12;
        Matrix matrixArm13;
        Matrix matrixhand1;

        float cameraMoveR;
        float cameraMoveL;

        public SimPhyGameWorld()
            : base()
        {
            Content.RootDirectory = "Content";

            World = this;
            graphDev = new GraphicsDeviceManager(this);
        }
        protected override void Initialize()
        {
            Graphics = GraphicsDevice;

            graphDev.PreferredBackBufferWidth = 1280;
            graphDev.PreferredBackBufferHeight = 800;
            graphDev.IsFullScreen = false;
            graphDev.ApplyChanges();

            SetupCamera(true);

            Window.Title = "HvA - Simulation & Physics - Opdracht 6 - Michelin Man - press <> to rotate camera";

            spheres = new List<Sphere>();

            //Make Body Parts
            spheres.Add(body1 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            spheres.Add(body2 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            body2.Transform.Translation = Vector3.Transform(body2.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 2, 0)));
            spheres.Add(body3 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            body3.Transform.Translation = Vector3.Transform(body3.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 4, 0)));
            spheres.Add(body4 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            body4.Transform.Translation = Vector3.Transform(body4.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 6, 0)));

            //Make Neck and Head
            spheres.Add(neck = new Sphere(Matrix.CreateScale((float)1.5),Color.White, 30));
            neck.Transform.Translation = Vector3.Transform(neck.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 8, 0)));
            spheres.Add(head1 = new Sphere(Matrix.CreateScale(new Vector3(3, 2, 2)), Color.White, 30));
            head1.Transform.Translation = Vector3.Transform(head1.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 10, 0)));
            spheres.Add(head2 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            head2.Transform.Translation = Vector3.Transform(head2.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 12,0)));

            //Make Left Arm and Hand
            spheres.Add(arm11 = new Sphere(Matrix.CreateScale(new Vector3(3, 2, 2)), Color.White, 30));
            arm11.Transform.Translation = Vector3.Transform(arm11.Transform.Translation, Matrix.CreateTranslation(new Vector3(-4, 5, 0)));
            spheres.Add(arm12 = new Sphere(Matrix.CreateScale(new Vector3(3, 2, 2)), Color.White, 30));
            arm12.Transform.Translation = Vector3.Transform(arm12.Transform.Translation, Matrix.CreateTranslation(new Vector3(-6, 5, 0)));
            spheres.Add(arm13 = new Sphere(Matrix.CreateScale(new Vector3(3, 2, 2)), Color.White, 30));
            arm13.Transform.Translation = Vector3.Transform(arm13.Transform.Translation, Matrix.CreateTranslation(new Vector3(-8, 5, 0)));
            spheres.Add(hand1 = new Sphere(Matrix.CreateScale(new Vector3(3, 2, 2)), Color.White, 30));
            hand1.Transform.Translation = Vector3.Transform(hand1.Transform.Translation, Matrix.CreateTranslation(new Vector3(-11, 5, 0)));

            //Make Right Arm and Hand
            spheres.Add(arm21 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            arm21.Transform.Translation = Vector3.Transform(arm21.Transform.Translation, Matrix.CreateTranslation(new Vector3(4, 5, 0)));
            spheres.Add(arm22 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            arm22.Transform.Translation = Vector3.Transform(arm22.Transform.Translation, Matrix.CreateTranslation(new Vector3(6, 5, 0)));
            spheres.Add(arm23 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            arm23.Transform.Translation = Vector3.Transform(arm23.Transform.Translation, Matrix.CreateTranslation(new Vector3(8, 5, 0)));
            spheres.Add(hand2 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            hand2.Transform.Translation = Vector3.Transform(hand2.Transform.Translation, Matrix.CreateTranslation(new Vector3(11, 5, 0)));

            //Make Left leg and Foot
            spheres.Add(leg11 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            leg11.Transform.Translation = Vector3.Transform(leg11.Transform.Translation, Matrix.CreateTranslation(new Vector3(-2, -2, 0)));
            spheres.Add(leg12 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            leg12.Transform.Translation = Vector3.Transform(leg12.Transform.Translation, Matrix.CreateTranslation(new Vector3(-2, -4, 0)));
            spheres.Add(leg13 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            leg13.Transform.Translation = Vector3.Transform(leg13.Transform.Translation, Matrix.CreateTranslation(new Vector3(-2, -6, 0)));
            spheres.Add(foot1 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            foot1.Transform.Translation = Vector3.Transform(foot1.Transform.Translation, Matrix.CreateTranslation(new Vector3(-2, -9, 0)));

            //Make Right Leg and Foot
            spheres.Add(leg21 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            leg21.Transform.Translation = Vector3.Transform(leg21.Transform.Translation, Matrix.CreateTranslation(new Vector3(2, -2, 0)));
            spheres.Add(leg22 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            leg22.Transform.Translation = Vector3.Transform(leg22.Transform.Translation, Matrix.CreateTranslation(new Vector3(2, -4, 0)));
            spheres.Add(leg23 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            leg23.Transform.Translation = Vector3.Transform(leg23.Transform.Translation, Matrix.CreateTranslation(new Vector3(2, -6, 0)));
            spheres.Add(foot2 = new Sphere(Matrix.CreateScale(2), Color.White, 30));
            foot2.Transform.Translation = Vector3.Transform(foot2.Transform.Translation, Matrix.CreateTranslation(new Vector3(2, -9, 0)));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            IsMouseVisible = true;
        }

        private void SetupCamera(bool initialize = false)
        {
            View = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            if (initialize) Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, SimPhyGameWorld.World.GraphicsDevice.Viewport.AspectRatio, 1.0f, 300.0f);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);

            foreach (Sphere sphere in spheres)
            {
                sphere.Draw();
            }

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                cameraMoveR = (float)1.5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                SetupCamera();
                cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY(cameraMoveR));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                SetupCamera();
                cameraMoveL = (float)-1.5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY(cameraMoveL));
            }
            rarm += (float)Math.PI * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Matrix totalMatrix = Matrix.CreateTranslation(new Vector3(0, (float)Math.Sin(rarm)/70, 0));


            matrixArm11 = Matrix.CreateTranslation(-body4.Transform.Translation) * totalMatrix;
            matrixArm11 = matrixArm11 * Matrix.CreateTranslation(body4.Transform.Translation);
            arm11.Transform.Translation = Vector3.Transform(arm11.Transform.Translation, matrixArm11);

            totalMatrix *= Matrix.CreateTranslation(new Vector3((float)Math.Sin(rarm) / 40, (float)Math.Sin(rarm) / 40, 0));
            matrixArm12 = Matrix.CreateTranslation(-arm11.Transform.Translation) * totalMatrix;
            matrixArm12 = matrixArm12 * Matrix.CreateTranslation(arm11.Transform.Translation);
            arm12.Transform.Translation = Vector3.Transform(arm12.Transform.Translation, matrixArm12);

            totalMatrix *= Matrix.CreateTranslation(new Vector3((float)Math.Sin(rarm) / 40, (float)Math.Sin(rarm) / 40, 0));
            matrixArm13 = Matrix.CreateTranslation(-arm12.Transform.Translation) * totalMatrix;
            matrixArm13 = matrixArm13 * Matrix.CreateTranslation(arm12.Transform.Translation);
            arm13.Transform.Translation = Vector3.Transform(arm13.Transform.Translation, matrixArm13);

            totalMatrix *= Matrix.CreateTranslation(new Vector3((float)Math.Sin(rarm) / 40, (float)Math.Sin(rarm) / 40, 0));
            matrixhand1 = Matrix.CreateTranslation(-arm13.Transform.Translation) * totalMatrix;
            matrixhand1 = matrixhand1 * Matrix.CreateTranslation(arm13.Transform.Translation);
            hand1.Transform.Translation = Vector3.Transform(hand1.Transform.Translation, matrixhand1);

            base.Update(gameTime);
        }
    }
}
