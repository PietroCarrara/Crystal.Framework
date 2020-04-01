using System;
using System.Linq;
using System.Collections.Generic;
using Crystal.Framework.Graphics;
using Crystal.Framework.UI.UILayouts;

namespace Crystal.Framework.UI.Widgets
{
    public class HorizontalContainer : Container
    {
        private float[] weights = new float[0];
        private Margins margins = Margins.All(0);

        /// <summary>
        /// The weights of
        /// </summary>
        public float[] Weights
        {
            get => weights;
            set
            {
                weights = value;
                this.ChangeState();
            }
        }

        public Margins Margins
        {
            get => margins;
            set
            {
                this.margins = value;
                this.ChangeState();
            }
        }

        protected override void OnSetChildren(Widget[] children)
        {
            if (!weights.Any())
            {
                this.weights = children.Select(w => 1f).ToArray();
            }

            this.ChangeState();
        }

        protected override IUILayout Build()
        {
            var len = this.Children.Count;

            if (len <= 0)
            {
                return IUILayout.Empty;
            }

            if (weights.Length != len)
            {
                throw new Exception("Weight count incorrect!");
            }

            var width = this.AvailableArea.Width / weights.Sum();

            var i = 0;
            var totalWeights = 0f;
            foreach (var child in this.Children)
            {
                var area = new TextureSlice(
                    (int)(this.AvailableArea.TopLeft.X + width * totalWeights),
                    this.AvailableArea.TopLeft.Y,
                    (int)(width * weights[i]),
                    this.AvailableArea.Height
                );

                child.AvailableArea = margins.Apply(area);

                totalWeights += weights[i];
                i++;
            }

            return new UnorderedWidgetsLayout
            {
                Children = this.Children,
            };
        }
    }
}