using OpenTK;

namespace Ragnarok.Core
{
    class Random
    {
        private readonly System.Random rng;

        public Random() => rng = new System.Random();

        /// <summary>
        /// get a float
        /// </summary>
        /// <returns>a float in the range of 0.0f to 1.0f</returns>
        public float Float() => (float)rng.NextDouble();

        /// <summary>
        /// get a float
        /// </summary>
        /// <param name="min">the minimum value</param>
        /// <param name="max">the maximum value</param>
        /// <returns>a float in the range of min to max</returns>
        public float Float(float min, float max) => min + Float() * (max - min);
        public float Float((float Min, float Max) range) => Float(range.Min, range.Max);

        /// <summary>
        /// get a Vector2
        /// </summary>
        /// <returns>a Vector2 in the range of (-0.5,-0.5) to (0.5,0.5)</returns>
        public Vector2 Vector2() => new Vector2(Float() - 0.5f, Float() - 0.5f).Normalized();
    }
}
