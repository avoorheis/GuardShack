public class LoadDetailViewModel : BaseViewModel
{
    private readonly ILoadDetailRepository _repository;
    public ObservableCollection<LoadDetail> LoadDetails { get; set; } = new ObservableCollection<LoadDetail>();

    private LoadDetail _selectedLoadDetail;
    public LoadDetail SelectedLoadDetail
    {
        get => _selectedLoadDetail;
        set { _selectedLoadDetail = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public LoadDetailViewModel(ILoadDetailRepository repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedLoadDetail != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedLoadDetail != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        LoadDetails.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            LoadDetails.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new LoadDetail();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedLoadDetail = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedLoadDetail != null)
        {
            await _repository.UpdateAsync(SelectedLoadDetail);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedLoadDetail != null)
        {
            await _repository.DeleteAsync(SelectedLoadDetail.ID);
            await LoadAsync();
        }
    }
}