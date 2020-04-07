using System.Numerics;
using System;
using System.Linq;
using Crystal.Framework.LowLevel;
using Crystal.Framework.Components;
using Crystal.Framework.Graphics;

namespace Crystal.Framework.Renderers
{
    public class CameraSpriteRenderer : IRenderer
    {
        private SamplerState sampler;
        private Matrix4x4 scale;

        public CameraSpriteRenderer(SamplerState sampler = SamplerState.LinearClamp)
        {
            this.sampler = sampler;
        }

        public void Initialize(Scene scene)
        {
            this.scale = Scaler.Instance.ScaleMatrix(
                new TextureSlice(Point.Zero, scene.Canvas.Size),
                new TextureSlice(Point.Zero, scene.Size)
            );

            scene.Canvas.SizeChanged += (c, size) =>
            {
                this.scale = Scaler.Instance.ScaleMatrix(
                    new TextureSlice(Point.Zero, size),
                    new TextureSlice(Point.Zero, scene.Size)
                );
            };
        }

        public void Render(Scene s, IDrawer drawer, float delta)
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

            drawer.BeginDraw(
                s.Canvas,
                transformMatrix: this.scale * cam.TransformationMatrix,
                samplerState: sampler
            );

            foreach (var entity in entities)
            {
                var (pos, _) = entity;

                foreach (var sprite in entity.FindAll<ISprite>())
                {
                    sprite.Draw(pos.Vector, delta, drawer);
                }
            }

            drawer.EndDraw();
        }
    }
}