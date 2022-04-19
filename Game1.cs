using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DProject
{

    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // A collection of crates
        Tree[] trees;

        //Chicken
        private Model model;
        
        // The game camera
        Camera camera;

        //Terain texture for ground
        Texture2D groundTexture;

        //Chicken Collides
        bool isColliding = false;

        //Screen Specs
        public static int ScreenHeight;
        public static int ScreenWidth;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Chicken
            model = Content.Load<Model>("chicken_export");

            //Texture
            groundTexture = Content.Load<Texture2D>("grass");

            //test trees
            trees = new Tree[] {
              new Tree(this, Matrix.CreateTranslation(4, 0, 50)),
              };

            // Initialize the camera 
            camera = new Camera(this, new Vector3(5, -1, 0));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            // Update the camera
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Chicken mesh

            foreach (var mesh in model.Meshes)
            {
                foreach (var effect1 in mesh.Effects)
                {
                    var effect = (BasicEffect)effect1;
                    effect.EnableDefaultLighting();

                    //Camera
                    var cameraPosition = new Vector3(0 , 1, -4);
                    var cameraLookAtVector = Vector3.Forward;
                    var cameraUpVector = Vector3.UnitY;
                    effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);


                    float aspectRatio = _graphics.PreferredBackBufferWidth / (float)_graphics.PreferredBackBufferHeight;
                    float fieldOfView = MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;

                    effect.World = Matrix.CreateScale(.50f);
                    effect.World = Matrix.CreateBillboard(Vector3.Down, cameraPosition, cameraUpVector, Vector3.Forward);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }

                
                mesh.Draw();
            }


            // Draw some crates
            foreach (Tree tree in trees)
            {
                tree.Draw(camera);
            }

            base.Draw(gameTime);
        }
    }
}
