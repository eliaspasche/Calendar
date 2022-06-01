# Calendar
___
School project to create a calendar application. The frontend is developed with WPF.

**Author**: Elias Pasche <br>
**Class**: EFI1A <br>
**Teacher**: Dirk Rust

**Deadline**: 2022-06-03 20:00


## Project structure 
### Assets
Assets like logos or icons can be found in `./Calendar/Assets`. The assets are included as resources in the application.

### Common
In the `./Calendar/Common` folder you can find all own data types and enums which are needed for the calendar.

### Components
The `./Calendar/Components` folder contains all components and windows from which the application is assembled. For each `xaml` file there is an additional `.cs` file which contains the logic of the respective component.

### Documentation
The `./Calendar/Documentation` folder includes three different Nassi-Shneiderman-Diagrams as `xml` file as well als picture.

### Services
In the `./Calendar/Services` folder you can find the the required services for this application. Currently there ist only the `CalendarService`. This Service contains helper methods for date calculations. 
