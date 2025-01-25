namespace DefaultNamespace
{
    public class Order
    {
        public Order(int size, int topping, int sugar, int ice)
        {
            Size = size;
            Topping = topping;
            Sugar = sugar;
            Ice = ice;
        }
        
        public int Size { get; private set; }
        public int Topping { get; private set; }
        public int Sugar { get; private set; }
        public int Ice { get; private set; }
    }
}
