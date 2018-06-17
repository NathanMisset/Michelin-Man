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

        //Sphere sun;
        //Sphere earth;
        //Sphere mars;
        //Sphere jupiter;
        //Sphere saturnus;
        //Sphere uranus;
        //Sphere moon;
        //Sphere pluto;

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

        //float rEarth;
        //float rMars;
        //float rJupiter;
        //float rSaturnus;
        //float rUranus;
        //float rMoon;

        //Matrix moonMatrix;

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

            Window.Title = "HvA - Simulation & Physics - Opdracht 6 - Transformations - press <> to rotate camera";

            spheres = new List<Sphere>();

            spheres.Add(body1 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            spheres.Add(body2 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            body2.Transform.Translation = Vector3.Transform(body2.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 2, 0)));
            spheres.Add(body3 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            body3.Transform.Translation = Vector3.Transform(body3.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 4, 0)));
            spheres.Add(body4 = new Sphere(Matrix.CreateScale(new Vector3(4, 2, 2)), Color.White, 30));
            body4.Transform.Translation = Vector3.Transform(body4.Transform.Translation, Matrix.CreateTranslation(new Vector3(0, 6, 0)));



            //// Step 1: Study the way the Sphere class is used in Initialize()
            //// Step 2: Scale the sun uniformly (= the same factor in x, y and z directions) by a factor 2
            //spheres.Add(sun = new Sphere(Matrix.Identity, Color.Yellow, 30));
            //sun.Transform.M11 = 2;
            //sun.Transform.M22 = 2;
            //sun.Transform.M33 = 2;

            //// Step 3: Create an earth Sphere, with radius, distance and color as given in the assignment
            //spheres.Add(earth = new Sphere(Matrix.Identity, Color.Navy, 30));
            //earth.Transform.Translation = new Vector3(16,0,0);

            //// Step 4: Create 4 other planets: mars, jupiter, saturnus, uranus (radius, distance and color as given)
            //spheres.Add(mars = new Sphere(Matrix.Identity, Color.Red, 30));
            //mars.Transform.Translation = new Vector3(21,0,0);
            //mars.Transform.M11 = 0.6f;
            //mars.Transform.M22 = 0.6f;
            //mars.Transform.M33 = 0.6f;

            //spheres.Add(jupiter = new Sphere(Matrix.Identity, Color.Orange, 30));
            //jupiter.Transform.Translation = new Vector3(27, 0, 0);
            //jupiter.Transform.M11 = 1.7f;
            //jupiter.Transform.M22 = 1.7f;
            //jupiter.Transform.M33 = 1.7f;

            //spheres.Add(saturnus = new Sphere(Matrix.Identity, Color.Khaki, 30));
            //saturnus.Transform.Translation = new Vector3(36, 0, 0);
            //saturnus.Transform.M11 = 1.6f;
            //saturnus.Transform.M22 = 1.6f;
            //saturnus.Transform.M33 = 1.6f;

            //spheres.Add(uranus = new Sphere(Matrix.Identity, Color.Cyan, 30));
            //uranus.Transform.Translation = new Vector3(43, 0, 0);
            //uranus.Transform.M11 = 1.5f;
            //uranus.Transform.M22 = 1.5f;
            //uranus.Transform.M33 = 1.5f;

            //// Step 7: Create the moon (radius, distance and color as given)            
            //spheres.Add(moon = new Sphere(Matrix.Identity, Color.LightGray, 30));
            //moon.Transform.Translation = new Vector3(18, 0, 0);
            //moon.Transform.M11 = 0.5f;
            //moon.Transform.M22 = 0.5f;
            //moon.Transform.M33 = 0.5f;

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
                // Step 10: Make the camera position rotate around the origin depending on gameTime.ElapsedGameTime.TotalSeconds

                cameraMoveR = (float)1.5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                SetupCamera();


                cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY(cameraMoveR));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // Step 10: Make the camera position rotate around the origin depending on gameTime.ElapsedGameTime.TotalSeconds

                SetupCamera();
                cameraMoveL = (float)-1.5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY(cameraMoveL));
            }

            //// Step 6: Make the planets rotate, all with different speeds between 0.15 and 0.5 (radians) per second
            //rEarth = (float)0.15 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rMars = (float)0.50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rJupiter = (float)0.25 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rSaturnus = (float)0.35 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rUranus = (float)0.15 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rMoon = (float)1.5 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //earth.Transform.Translation = Vector3.Transform(earth.Transform.Translation, Matrix.CreateRotationY(rEarth));
            //mars.Transform.Translation = Vector3.Transform(mars.Transform.Translation, Matrix.CreateRotationY(rMars));
            //jupiter.Transform.Translation = Vector3.Transform(jupiter.Transform.Translation, Matrix.CreateRotationY(rJupiter));
            //saturnus.Transform.Translation = Vector3.Transform(saturnus.Transform.Translation, Matrix.CreateRotationY(rSaturnus));
            //uranus.Transform.Translation = Vector3.Transform(uranus.Transform.Translation, Matrix.CreateRotationY(rUranus));

            //// Step 7: Make the moon rotate around the earth, speed 1.5
            //moonMatrix = Matrix.CreateTranslation(-earth.Transform.Translation) * Matrix.CreateRotationY(rMoon) ;
            //moonMatrix = moonMatrix * Matrix.CreateRotationZ(rMoon);
            //moonMatrix = moonMatrix * Matrix.CreateTranslation(earth.Transform.Translation) ;
            //moonMatrix = moonMatrix * Matrix.CreateRotationY(rEarth);
            ////moonMatrix = moonMatrix * Matrix.CreateFromAxisAngle(new Vector3(0, 0, 0), 45); 
            //moon.Transform.Translation = Vector3.Transform(moon.Transform.Translation, moonMatrix);


            ////moonMatrix = moonMatrix * Matrix.CreateFromAxisAngle(new Vector3(0, 0, 0), 45);
            //// Step 8: Change the orbit of the moon such that it is rotated 45 degrees toward the sun/origin(see example!)

            base.Update(gameTime);
        }
    }
}
