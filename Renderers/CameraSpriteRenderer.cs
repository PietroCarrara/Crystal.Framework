using System;
using System.Linq;
using Crystal.Framework.Components;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Renderers
{
    public class CameraSpriteRenderer : IRenderer
    {
        private SamplerState sampler;
        
        public CameraSpriteRenderer(SamplerState sampler = SamplerState.LinearClamp)
        {
            this.sampler = sampler;
        }
        
        public void Render(Scene s, float delta)
        {
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

            s.Drawer.BeginDraw(
                transformMatrix: cam.TransformationMatrix * s.Viewport.TransformMatrix,
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