public class SealsUsedViewModel : BaseViewModel
{
    private readonly IRepository<SealsUsed> _repository;
    public ObservableCollection<SealsUsed> Seals { get; set; } = new ObservableCollection<SealsUsed>();

    private SealsUsed _selectedSeal;
    public SealsUsed SelectedSeal
    {
        get => _selectedSeal;
        set { _selectedSeal = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public SealsUsedViewModel(IRepository<SealsUsed> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedSeal != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedSeal != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        Seals.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            Seals.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new SealsUsed();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedSeal = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedSeal != null)
        {
            await _repository.UpdateAsync(SelectedSeal);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedSeal != null)
        {
            await _repository.DeleteAsync(SelectedSeal.ID);
            await LoadAsync();
        }
    }
}