using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace MauiTesting.Pages;

public partial class SecondPage : ContentPage
{
    SecondPageViewModel viewModel;
    public SecondPage()
    {
        BindingContext = viewModel = new SecondPageViewModel(this);
        InitializeComponent();
    }
}
public partial class SecondPageViewModel : ObservableObject
{
    SecondPage page;
    [ObservableProperty]
    public partial string Text { get; set; }
    public SecondPageViewModel(SecondPage page)
    {
        this.page = page;
    }
    [RelayCommand]
    public async Task ButtonClicked()
    {
        await page.DisplayAlertAsync("标题", $"你输入的是: {Text}", "好");
    }
}