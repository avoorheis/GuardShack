public class OrderDetailViewModel : BaseViewModel
{
    private readonly IRepository<OrderDetail> _repository;
    public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new ObservableCollection<OrderDetail>();

    private OrderDetail _selectedOrderDetail;
    public OrderDetail SelectedOrderDetail
    {
        get => _selectedOrderDetail;
        set { _selectedOrderDetail = value; OnPropertyChanged(); }
    }

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public OrderDetailViewModel(IRepository<OrderDetail> repository)
    {
        _repository = repository;
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
        AddCommand = new RelayCommand(async () => await AddAsync());
        UpdateCommand = new RelayCommand(async () => await UpdateAsync(), () => SelectedOrderDetail != null);
        DeleteCommand = new RelayCommand(async () => await DeleteAsync(), () => SelectedOrderDetail != null);
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        OrderDetails.Clear();
        var items = await _repository.GetAllAsync();
        foreach (var item in items)
            OrderDetails.Add(item);
    }

    public async Task AddAsync()
    {
        var newItem = new OrderDetail();
        await _repository.AddAsync(newItem);
        await LoadAsync();
        SelectedOrderDetail = newItem;
    }

    public async Task UpdateAsync()
    {
        if (SelectedOrderDetail != null)
        {
            await _repository.UpdateAsync(SelectedOrderDetail);
            await LoadAsync();
        }
    }

    public async Task DeleteAsync()
    {
        if (SelectedOrderDetail != null)
        {
            await _repository.DeleteAsync(SelectedOrderDetail.OrderNumber);
            await LoadAsync();
        }
    }
}