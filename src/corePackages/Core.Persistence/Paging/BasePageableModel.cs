namespace Core.Persistence.Paging;

public class BasePageableModel
{
    public int Index { get; set; }  //Sayfanın indexi
    public int Size { get; set; } //Sayfada kaç data var
    public int Count { get; set; } //Toplamda kaç dada var
    public int Pages { get; set; } //Kaç sayfa var
    public bool HasPrevious { get; set; } //Öncesinde sayfa varmı
    public bool HasNext { get; set; } //Sonrasında sayfa varmı
}