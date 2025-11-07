public class BakeryViewModel : BaseViewModel
{
    private readonly IRepository<Bakery> _repository;
    public ObservableCollection<Bakery> Bakeries { get; set; } = new ObservableCollection<Bakery>();

    private Bakery _selectedBakery;
    public Bakery SelectedBakery
    {
        get => _selectedBakery;
        set { _selectedBakery = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public BakeryViewModel(IRepository<Bakery> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedBakery != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedBakery != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        Bakeries.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            Bakeries.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new Bakery();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedBakery = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedBakery != null)
        {
            await _repository.UpdateAsync(SelectedBakery);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedBakery != null)
        {
            await _repository.DeleteAsync(SelectedBakery.BakeryID);
            await LoadAsync();
        }
    }
}