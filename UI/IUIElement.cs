using Crystal.Framework.Graphics;

namespace Crystal.Framework.UI
{
    public interface IUIElement
    {
        /// <summary>
        /// At which scene is this entity
        /// </summary>
        Scene Owner { get; set; }

        /// <summary>
        /// Space available to this entity
        /// </summary>
        Rectangle AvailableSpace { get; set; }

        /// <summary>
        /// Which point in space to use to calculate position
        /// </summary>
        AnchorPoint Anchor { get; set; }

        /// <summary>
        /// Gets the global position value
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// The Layout parenting this entity.
        /// Null if not parented
        /// </summary>
        Layout Parent { get; set; }

        /// <summary>
        /// The skin defining the looks of this element
        /// </summary>
        ISkin Skin { get; set; }

        /// <summary>
        /// Update this entity. Used to check for input
        /// or other interactions
        /// </summary>
        /// <param name="delta">How much time has passed since the last frame</param>
        void Update(float delta);

        /// <summary>
        /// Draw this entity
        /// </summary>
        /// <param name="drawer">What to use to draw</param>
        /// <param name="delta">How much time has passed since the last frame</param>
        void Draw(IDrawer drawer, float delta);
    }
}