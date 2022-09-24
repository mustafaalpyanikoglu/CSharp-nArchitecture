namespace Core.Persistence.Paging;

public interface IPaginate<T>
{
    int From { get; }
    int Index { get; } //kaçıncı index
    int Size { get; }  //kaç sayfa
    int Count { get; } //toplam kaç data
    int Pages { get; }
    IList<T> Items { get; } 
    bool HasPrevious { get; }
    bool HasNext { get; }
}