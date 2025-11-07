public class TruckInspectionConfirmationViewModel : BaseViewModel
{
    private readonly IRepository<TruckInspectionConfirmation> _repository;
    public ObservableCollection<TruckInspectionConfirmation> Confirmations { get; set; } = new ObservableCollection<TruckInspectionConfirmation>();

    private TruckInspectionConfirmation _selectedConfirmation;
    public TruckInspectionConfirmation SelectedConfirmation
    {
        get => _selectedConfirmation;
        set { _selectedConfirmation = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public TruckInspectionConfirmationViewModel(IRepository<TruckInspectionConfirmation> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedConfirmation != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedConfirmation != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        Confirmations.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            Confirmations.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new TruckInspectionConfirmation();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedConfirmation = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedConfirmation != null)
        {
            await _repository.UpdateAsync(SelectedConfirmation);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedConfirmation != null)
        {
            await _repository.DeleteAsync(SelectedConfirmation.SignatureID);
            await LoadAsync();
        }
    }
}