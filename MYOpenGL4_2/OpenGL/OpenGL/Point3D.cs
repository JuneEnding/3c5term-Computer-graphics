namespace OpenGL
{

    /// Класс описывающий точку в 3D пространстве
    public class Point3D
    {
        /// координаты
        public int x { set; get; }
        public int y { set; get; }
        public int z { set; get; }

        public Point3D ()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public Point3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public class Point3Df
    {
        /// координаты
        public float x { set; get; }
        public float y { set; get; }
        public float z { set; get; }

        public Point3Df()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public Point3Df(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
