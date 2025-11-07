public class ProductCodeViewModel : BaseViewModel
{
    private readonly IRepository<ProductCode> _repository;
    public ObservableCollection<ProductCode> ProductCodes { get; set; } = new ObservableCollection<ProductCode>();

    private ProductCode _selectedProductCode;
    public ProductCode SelectedProductCode
    {
        get => _selectedProductCode;
        set { _selectedProductCode = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public ProductCodeViewModel(IRepository<ProductCode> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedProductCode != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedProductCode != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        ProductCodes.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            ProductCodes.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new ProductCode();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedProductCode = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedProductCode != null)
        {
            await _repository.UpdateAsync(SelectedProductCode);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedProductCode != null)
        {
            await _repository.DeleteAsync(SelectedProductCode.LoadProductCode);
            await LoadAsync();
        }
    }
}
