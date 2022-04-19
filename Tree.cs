using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace _3DProject
{
    /// <summary>
    /// A class representing a crate
    /// </summary>
    public class Tree
    {
        // The game this crate belongs to
        Game game;

        // The effect to render the crate with
        BasicEffect effect;

        public Model model;

        BoundingBox boundingbox;

        Vector3 position;


        public Tree(Game game, Matrix world)
        {
            this.game = game;
            this.model = game.Content.Load<Model>("shrub_EXPORT");
            InitializeEffect();
            effect.World = world;
            position = world.Translation;


        }

        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.CreateScale(2.0f);
            effect.View = Matrix.CreateLookAt(
                new Vector3(8, 9, 12), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up            // The camera up vector
            );
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            effect.TextureEnabled = true;
            effect.LightingEnabled = true;
            effect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);
        }

        public void Draw(ICamera camera)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (var effect1 in mesh.Effects)
                {
                    var effect = (BasicEffect)effect1;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.CurrentTechnique.Passes[0].Apply();
                }
                mesh.Draw();
            }


        }
    }
}
