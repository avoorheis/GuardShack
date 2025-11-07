public class CarrierViewModel : BaseViewModel
{
    private readonly IRepository<Carrier> _repository;
    public ObservableCollection<Carrier> Carriers { get; set; } = new ObservableCollection<Carrier>();

    private Carrier _selectedCarrier;
    public Carrier SelectedCarrier
    {
        get => _selectedCarrier;
        set { _selectedCarrier = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public CarrierViewModel(IRepository<Carrier> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedCarrier != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedCarrier != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        Carriers.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            Carriers.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new Carrier();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedCarrier = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedCarrier != null)
        {
            await _repository.UpdateAsync(SelectedCarrier);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedCarrier != null)
        {
            await _repository.DeleteAsync(SelectedCarrier.CarrierID);
            await LoadAsync();
        }
    }
}