namespace Crystal.ECS.Components.Graphical
{
    public class Camera : IComponent
    {
        private static Camera active = null;

        public bool Active
        {
            get => Camera.active == this;
            set
            {
                if (this.Active && !value)
                {
                    Camera.active = null;
                }
                else if (!this.Active && value)
                {
                    Camera.active = this;
                }
            }
        }

        public Camera()
        {
            if (Camera.active == null)
            {
                Camera.active = this;
            }
        }
    }
}