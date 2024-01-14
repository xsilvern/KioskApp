
using KioskAppServer.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public class Menu: INotifyPropertyChanged
{
    public string Name { get; set; }
    public string ImageId { get; set; }
    public decimal Price { get; set; }

    public BitmapImage MenuImage { get; set; }
    private int _quantity=0;
    public int Quantity { get => _quantity; set => SetProperty(ref _quantity, value); }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (!(obj is Menu other))
        {
            return false;
        }

        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
    public void LoadImageAsync(GoogleDriveDataService driveService)
    {
        if (!string.IsNullOrEmpty(ImageId))
        {
            using (Stream imageStream = driveService.DownloadImage(ImageId))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = imageStream;
                bitmap.EndInit();
                bitmap.Freeze(); // WPF의 UI 스레드 외부에서 생성된 이미지를 안정적으로 사용하기 위해
                MenuImage = bitmap;
            }
        }
    }
}