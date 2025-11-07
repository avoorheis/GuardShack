public class LoadoutViewModel : BaseViewModel
{
    private readonly IRepository<Loadout> _repository;
    public ObservableCollection<Loadout> Loadouts { get; set; } = new ObservableCollection<Loadout>();

    private Loadout _selectedLoadout;
    public Loadout SelectedLoadout
    {
        get => _selectedLoadout;
        set { _selectedLoadout = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public LoadoutViewModel(IRepository<Loadout> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedLoadout != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedLoadout != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        Loadouts.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            Loadouts.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new Loadout();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedLoadout = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedLoadout != null)
        {
            await _repository.UpdateAsync(SelectedLoadout);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedLoadout != null)
        {
            await _repository.DeleteAsync(SelectedLoadout.Counter);
            await LoadAsync();
        }
    }
}
