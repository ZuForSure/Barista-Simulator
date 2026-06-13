public interface IDropZone 
{
    void PlaceItem(Item item);
    bool IsOccupied();
    PlaceableType AllowType { get; }
}
