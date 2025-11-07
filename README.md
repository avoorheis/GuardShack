# GuardShack

# WPF Guard Shack Management System

This project is a modern WPF application that replicates and enhances the functionality of the legacy Access-based Guard Shack system. It is designed for reliability, maintainability, and ease of use, leveraging a SQL Server backend and the MVVM architectural pattern.

## Features

*   **Modern WPF User Interface**: Clean, responsive, and user-friendly screens for all major workflows (shipping, receiving, reporting, etc.).
*   **Data Models**: Strongly-typed C# classes generated from the original Access database schema.
*   **MVVM Architecture**: Separation of concerns for easier maintenance and testability.
*   **Advanced Printing**: Supports FlowDocument and PDF export for professional, multi-page printouts.
*   **Reporting**: Built-in and extensible reporting capabilities.
*   **Extensible Data Access**: Uses Entity Framework for robust, scalable data operations.

## Project Structure

    /Models         // Data models (tables)
    /ViewModels     // Business logic and data binding
    /Views          // XAML UI screens
    /Data           // Database context and repositories
    /Services       // Dialogs, reporting, and business logic
    /Helpers        // Utility classes (e.g., RelayCommand)

## Printing & Reporting

*   **FlowDocument Printing**: For styled, paginated printouts directly from the application.
*   **PDF Export**: Generate professional PDF reports for sharing or archiving.
*   **Reporting Tools**: Easily integrate with third-party reporting solutions if needed.

## Getting Started

1.  Clone the repository.
2.  Update the connection string in `AppDbContext.cs` to point to your SQL Server instance.
3.  Build and run the solution in Visual Studio 2022 or later.
4.  Review and customize the Views and ViewModels as needed for your workflow.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is the property of Mennel Milling Company and all copyrighted materials are the sole property of said company.

***

