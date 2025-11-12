using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace MauiTesting.Pages;

public partial class SecoundPage : ContentPage
{
    SecoundPageViewModel viewModel;
    public SecoundPage()
    {
        BindingContext = viewModel = new SecoundPageViewModel(this);
        InitializeComponent();
    }
}
public partial class SecoundPageViewModel : ObservableObject
{
    SecoundPage page;
    [ObservableProperty]
    public partial string Text { get; set; }
    public SecoundPageViewModel(SecoundPage page)
    {
        this.page = page;
    }
    [RelayCommand]
    public async Task ButtonClicked()
    {
        await page.DisplayAlertAsync("标题", $"你输入的是: {Text}", "好");
    }
}