public class PlayerItem {
    Item item;
    int quantity;

    public PlayerItem(Item item, int qt){
        this.Item = item;
        this.quantity = qt;
    }

    public Item Item { get => item; private set => item = value; }
    public int Quantity { get => quantity; set => quantity = value; }

}