using System;
using System.Linq;
using Crystal.Framework.Components;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Renderers
{
    public class CameraSpriteRenderer : IRenderer
    {
        private SamplerState sampler;
        private Canvas canvas;
        
        public CameraSpriteRenderer(SamplerState sampler = SamplerState.LinearClamp)
        {
            this.sampler = sampler;
        }

        public void Initialize(Scene scene)
        {
            this.canvas = scene.Canvases.Create((Point)scene.Size);
        }
        
        public void Render(Scene s, float delta)
        {
            canvas.Clear();
            
            var camera = s.Entities
                .With<Camera>()
                .Many()
                .Where(e => e.Component.Active)
                .FirstOrDefault();

            if (camera == null)
            {
                return;
            }

            var cam = camera.Component;

            var entities = s.Entities
                .With<ISprite>()
                .With<Position>()
                .Many();

            // TODO: Implement resolution independence
            // Right now, if the size of this.canvas changes, the screen will look different
            s.Drawer.BeginDraw(
                this.canvas,
                transformMatrix: cam.TransformationMatrix,
                samplerState: sampler
            );

            foreach (var entity in entities)
            {
                var (pos, _) = entity;

                foreach (var sprite in entity.FindAll<ISprite>())
                {
                    sprite.Draw(pos.Vector, delta, s.Drawer);
                }
            }

            s.Drawer.EndDraw();
        }
    }
}